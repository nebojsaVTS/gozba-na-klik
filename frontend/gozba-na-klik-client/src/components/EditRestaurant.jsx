import "./EditRestaurant.scss";
import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { API_BASE_URL } from "../api";
import BackButton from "./BackButton";

function EditRestaurant() {
  const { restaurantId } = useParams();
  const navigate = useNavigate();

  const [municipalities, setMunicipalities] = useState([]);
  const [settlements, setSettlements] = useState([]);

  const [selectedMunicipalityId, setSelectedMunicipalityId] = useState("");

  const [formData, setFormData] = useState({
    name: "",
    address: "",
    phoneNumber: "",
    settlementId: "",
    supportsDelivery: false,
    supportsPickup: false,
  });

  const [loading, setLoading] = useState(true);
  const [saving, setSaving] = useState(false);

  const [message, setMessage] = useState("");
  const [isError, setIsError] = useState(false);

  useEffect(() => {
    const loadInitialData = async () => {
      try {
        const [restaurantResponse, municipalitiesResponse] = await Promise.all([
          fetch(`${API_BASE_URL}/restaurants/${restaurantId}`),
          fetch(`${API_BASE_URL}/Locations/municipalities`),
        ]);

        if (!restaurantResponse.ok || !municipalitiesResponse.ok) {
          throw new Error();
        }

        const restaurantData = await restaurantResponse.json();
        const municipalitiesData = await municipalitiesResponse.json();

        setMunicipalities(municipalitiesData);

        setSelectedMunicipalityId(
          restaurantData.municipalityId
            ? String(restaurantData.municipalityId)
            : "",
        );

        setFormData({
          name: restaurantData.name ?? "",
          address: restaurantData.address ?? "",
          phoneNumber: restaurantData.phoneNumber ?? "",
          settlementId: restaurantData.settlementId
            ? String(restaurantData.settlementId)
            : "",
          supportsDelivery: restaurantData.supportsDelivery,
          supportsPickup: restaurantData.supportsPickup,
        });
      } catch {
        setMessage("Greška pri učitavanju restorana.");
        setIsError(true);
      } finally {
        setLoading(false);
      }
    };

    loadInitialData();
  }, [restaurantId]);

  useEffect(() => {
    if (!selectedMunicipalityId) {
      setSettlements([]);
      return;
    }

    const loadSettlements = async () => {
      try {
        const response = await fetch(
          `${API_BASE_URL}/Locations/municipalities/${selectedMunicipalityId}/settlements`,
        );

        if (!response.ok) {
          throw new Error();
        }

        const data = await response.json();

        setSettlements(data);
      } catch {
        setMessage("Greška pri učitavanju naselja.");
        setIsError(true);
      }
    };

    loadSettlements();
  }, [selectedMunicipalityId]);

  const handleChange = (e) => {
    const { name, value, type, checked } = e.target;

    setFormData((current) => ({
      ...current,
      [name]: type === "checkbox" ? checked : value,
    }));

    setMessage("");
  };

  const handleMunicipalityChange = (e) => {
    const municipalityId = e.target.value;

    setSelectedMunicipalityId(municipalityId);

    setFormData((current) => ({
      ...current,
      settlementId: "",
    }));

    setMessage("");
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    setMessage("");

    if (
      !formData.name.trim() ||
      !formData.address.trim() ||
      !formData.phoneNumber.trim() ||
      !selectedMunicipalityId ||
      !formData.settlementId
    ) {
      setMessage("Sva polja su obavezna.");
      setIsError(true);
      return;
    }

    if (!formData.supportsDelivery && !formData.supportsPickup) {
      setMessage(
        "Restoran mora podržavati dostavu, lično preuzimanje ili obe opcije.",
      );
      setIsError(true);
      return;
    }

    setSaving(true);

    try {
      const response = await fetch(
        `${API_BASE_URL}/restaurants/${restaurantId}`,
        {
          method: "PUT",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({
            name: formData.name,
            address: formData.address,
            phoneNumber: formData.phoneNumber,
            settlementId: Number(formData.settlementId),
            supportsDelivery: formData.supportsDelivery,
            supportsPickup: formData.supportsPickup,
          }),
        },
      );

      if (!response.ok) {
        const errorText = await response.text();

        setMessage(errorText || "Izmena restorana nije uspela.");
        setIsError(true);
        return;
      }

      setMessage("Restoran je uspešno izmenjen.");
      setIsError(false);

      setTimeout(() => {
        navigate("/admin/restaurants");
      }, 1000);
    } catch {
      setMessage("Greška prilikom povezivanja sa serverom.");
      setIsError(true);
    } finally {
      setSaving(false);
    }
  };

  if (loading) {
    return (
      <div className="edit-restaurant-page">
        <p>Učitavanje restorana...</p>
      </div>
    );
  }

  return (
    <div className="edit-restaurant-page">
      <div className="edit-restaurant-content">
        <div className="edit-restaurant-header">
          <BackButton to="/admin/restaurants" />

          <h1>Izmena restorana</h1>

          <div className="edit-restaurant-header-placeholder" />
        </div>

        <form className="edit-restaurant-form" onSubmit={handleSubmit}>
          <div className="edit-restaurant-field">
            <label htmlFor="name">Naziv restorana</label>

            <input
              id="name"
              type="text"
              name="name"
              value={formData.name}
              onChange={handleChange}
            />
          </div>

          <div className="edit-restaurant-grid">
            <div className="edit-restaurant-field">
              <label htmlFor="municipality">Opština</label>

              <select
                id="municipality"
                value={selectedMunicipalityId}
                onChange={handleMunicipalityChange}
              >
                <option value="">Izaberite opštinu</option>

                {municipalities.map((municipality) => (
                  <option key={municipality.id} value={municipality.id}>
                    {municipality.name}
                  </option>
                ))}
              </select>
            </div>

            <div className="edit-restaurant-field">
              <label htmlFor="settlementId">Naselje</label>

              <select
                id="settlementId"
                name="settlementId"
                value={formData.settlementId}
                onChange={handleChange}
                disabled={!selectedMunicipalityId}
              >
                <option value="">
                  {selectedMunicipalityId
                    ? "Izaberite naselje"
                    : "Prvo izaberite opštinu"}
                </option>

                {settlements.map((settlement) => (
                  <option key={settlement.id} value={settlement.id}>
                    {settlement.name}
                  </option>
                ))}
              </select>
            </div>
          </div>

          <div className="edit-restaurant-field">
            <label htmlFor="address">Adresa</label>

            <input
              id="address"
              type="text"
              name="address"
              value={formData.address}
              onChange={handleChange}
            />
          </div>

          <div className="edit-restaurant-field">
            <label htmlFor="phoneNumber">Telefon</label>

            <input
              id="phoneNumber"
              type="text"
              name="phoneNumber"
              value={formData.phoneNumber}
              onChange={handleChange}
            />
          </div>

          <div className="edit-restaurant-options">
            <p>Način preuzimanja</p>

            <label>
              <input
                type="checkbox"
                name="supportsDelivery"
                checked={formData.supportsDelivery}
                onChange={handleChange}
              />
              Dostava
            </label>

            <label>
              <input
                type="checkbox"
                name="supportsPickup"
                checked={formData.supportsPickup}
                onChange={handleChange}
              />
              Lično preuzimanje
            </label>
          </div>

          {message && (
            <p
              className={
                isError
                  ? "edit-restaurant-message error"
                  : "edit-restaurant-message success"
              }
            >
              {message}
            </p>
          )}

          <div className="edit-restaurant-actions">
            <button
              type="submit"
              className="edit-restaurant-save"
              disabled={saving}
            >
              {saving ? "Čuvanje..." : "Sačuvaj izmene"}
            </button>

            <button
              type="button"
              className="edit-restaurant-cancel"
              onClick={() => navigate("/admin/restaurants")}
              disabled={saving}
            >
              Odustani
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}

export default EditRestaurant;
