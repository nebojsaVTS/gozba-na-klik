import "./MojeAdrese.scss";
import { useState, useEffect } from "react";
import { useNavigate, useLocation } from "react-router-dom";
import { API_BASE_URL } from "../api";
import BackButton from "./BackButton";

function MojeAdrese() {
  const navigate = useNavigate();
  const location = useLocation();
  const returnTo = location.state?.returnTo;

  const [user, setUser] = useState(null);
  const [addresses, setAddresses] = useState([]);
  const [loadError, setLoadError] = useState("");

  const [municipalities, setMunicipalities] = useState([]);
  const [settlements, setSettlements] = useState([]);
  const [streets, setStreets] = useState([]);

  const [editSettlements, setEditSettlements] = useState([]);
  const [editStreets, setEditStreets] = useState([]);

  const [formData, setFormData] = useState({
    municipalityId: "",
    settlementId: "",
    streetId: "",
    houseNumber: "",
    phoneNumber: "",
  });

  const [formError, setFormError] = useState("");

  const [editingId, setEditingId] = useState(null);

  const [editFormData, setEditFormData] = useState({
    municipalityId: "",
    settlementId: "",
    streetId: "",
    houseNumber: "",
    phoneNumber: "",
  });

  const [editError, setEditError] = useState("");

  useEffect(() => {
    const stored = localStorage.getItem("gozbaUser");

    if (!stored) {
      navigate("/login");
      return;
    }

    setUser(JSON.parse(stored));
  }, [navigate]);

  const getSettlements = async (municipalityId) => {
    const response = await fetch(
      `${API_BASE_URL}/Locations/municipalities/${municipalityId}/settlements`,
    );

    if (!response.ok) {
      throw new Error("Greška pri učitavanju naselja.");
    }

    return await response.json();
  };

  const getStreets = async (settlementId) => {
    const response = await fetch(
      `${API_BASE_URL}/Locations/settlements/${settlementId}/streets`,
    );

    if (!response.ok) {
      throw new Error("Greška pri učitavanju ulica.");
    }

    return await response.json();
  };

  useEffect(() => {
    const loadMunicipalities = async () => {
      try {
        const response = await fetch(
          `${API_BASE_URL}/Locations/municipalities`,
        );

        if (!response.ok) {
          throw new Error();
        }

        const data = await response.json();

        setMunicipalities(data);

        // Dok imamo samo jednu opstinu, automatski je izaberi.
        if (data.length === 1) {
          const municipalityId = String(data[0].id);

          setFormData((previous) => ({
            ...previous,
            municipalityId,
          }));

          const settlementData = await getSettlements(data[0].id);
          setSettlements(settlementData);
        }
      } catch {
        setLoadError("Greška pri učitavanju podataka o lokacijama.");
      }
    };

    loadMunicipalities();
  }, []);

  const loadAddresses = (userId) => {
    fetch(`${API_BASE_URL}/addresses?userId=${userId}`)
      .then((response) => {
        if (!response.ok) {
          throw new Error("Greška pri učitavanju adresa.");
        }

        return response.json();
      })
      .then((data) => {
        setAddresses(data);
        setLoadError("");
      })
      .catch(() => {
        setLoadError("Greška pri učitavanju adresa.");
      });
  };

  useEffect(() => {
    if (user) {
      loadAddresses(user.id);
    }
  }, [user]);

  const handleAddMunicipalityChange = async (e) => {
    const municipalityId = e.target.value;

    setFormData((previous) => ({
      ...previous,
      municipalityId,
      settlementId: "",
      streetId: "",
    }));

    setSettlements([]);
    setStreets([]);
    setFormError("");

    if (!municipalityId) {
      return;
    }

    try {
      const data = await getSettlements(municipalityId);
      setSettlements(data);
    } catch {
      setFormError("Greška pri učitavanju naselja.");
    }
  };

  const handleAddSettlementChange = async (e) => {
    const settlementId = e.target.value;

    setFormData((previous) => ({
      ...previous,
      settlementId,
      streetId: "",
    }));

    setStreets([]);
    setFormError("");

    if (!settlementId) {
      return;
    }

    try {
      const data = await getStreets(settlementId);
      setStreets(data);
    } catch {
      setFormError("Greška pri učitavanju ulica.");
    }
  };

  const handleAddStreetChange = (e) => {
    setFormData((previous) => ({
      ...previous,
      streetId: e.target.value,
    }));

    setFormError("");
  };

  const handleAddHouseNumberChange = (e) => {
    setFormData((previous) => ({
      ...previous,
      houseNumber: e.target.value,
    }));
  };

  const handleAddPhoneChange = (e) => {
    const value = e.target.value.replace(/\D/g, "").slice(0, 9);

    setFormData((previous) => ({
      ...previous,
      phoneNumber: value,
    }));
  };

  const handleAddSubmit = async (e) => {
    e.preventDefault();

    if (
      !formData.municipalityId ||
      !formData.settlementId ||
      !formData.streetId
    ) {
      setFormError("Opština, naselje i ulica su obavezni.");
      return;
    }

    if (!formData.houseNumber.trim()) {
      setFormError("Broj kuće ili zgrade je obavezan.");
      return;
    }

    if (formData.phoneNumber.length < 8 || formData.phoneNumber.length > 9) {
      setFormError("Broj telefona mora sadržati 8 ili 9 cifara nakon +381.");
      return;
    }

    try {
      const response = await fetch(`${API_BASE_URL}/addresses`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          streetId: Number(formData.streetId),
          houseNumber: formData.houseNumber.trim(),
          phoneNumber: `+381${formData.phoneNumber}`,
          userId: user.id,
        }),
      });

      if (!response.ok) {
        const errorText = await response.text();
        setFormError(errorText || "Dodavanje adrese nije uspelo.");
        return;
      }

      setFormError("");
      setStreets([]);

      setFormData({
        municipalityId:
          municipalities.length === 1 ? String(municipalities[0].id) : "",
        settlementId: "",
        streetId: "",
        houseNumber: "",
        phoneNumber: "",
      });

      if (returnTo) {
        navigate(returnTo);
        return;
      }

      loadAddresses(user.id);
    } catch {
      setFormError("Greška prilikom povezivanja sa serverom.");
    }
  };

  const startEditing = async (address) => {
    let phoneNumber = address.phoneNumber || "";

    if (phoneNumber.startsWith("+381")) {
      phoneNumber = phoneNumber.slice(4);
    }

    setEditingId(address.id);
    setEditError("");
    setEditSettlements([]);
    setEditStreets([]);

    try {
      let municipalityList = municipalities;

      if (municipalityList.length === 0) {
        const response = await fetch(
          `${API_BASE_URL}/Locations/municipalities`,
        );

        municipalityList = await response.json();
        setMunicipalities(municipalityList);
      }

      let municipality = municipalityList.find(
        (item) => item.name === address.municipalityName,
      );

      // Stare adrese nemaju municipalityName.
      // Ako postoji samo jedna opstina, koristimo nju.
      if (!municipality && municipalityList.length === 1) {
        municipality = municipalityList[0];
      }

      if (!municipality) {
        setEditFormData({
          municipalityId: "",
          settlementId: "",
          streetId: "",
          houseNumber: address.houseNumber || "",
          phoneNumber,
        });

        return;
      }

      const settlementData = await getSettlements(municipality.id);
      setEditSettlements(settlementData);

      const settlement = settlementData.find(
        (item) =>
          item.name === address.settlementName || item.name === address.city,
      );

      if (!settlement) {
        setEditFormData({
          municipalityId: String(municipality.id),
          settlementId: "",
          streetId: "",
          houseNumber: address.houseNumber || "",
          phoneNumber,
        });

        return;
      }

      const streetData = await getStreets(settlement.id);
      setEditStreets(streetData);

      let streetId = address.streetId ? String(address.streetId) : "";

      // Ako je stara adresa, pokusaj da pronadjes ulicu po nazivu.
      if (!streetId) {
        const matchingStreet = streetData.find(
          (item) =>
            item.name === address.streetName || item.name === address.street,
        );

        if (matchingStreet) {
          streetId = String(matchingStreet.id);
        }
      }

      setEditFormData({
        municipalityId: String(municipality.id),
        settlementId: String(settlement.id),
        streetId,
        houseNumber: address.houseNumber || "",
        phoneNumber,
      });
    } catch {
      setEditError("Greška pri učitavanju podataka za adresu.");
    }
  };

  const cancelEditing = () => {
    setEditingId(null);
    setEditError("");
    setEditSettlements([]);
    setEditStreets([]);
  };

  const handleEditMunicipalityChange = async (e) => {
    const municipalityId = e.target.value;

    setEditFormData((previous) => ({
      ...previous,
      municipalityId,
      settlementId: "",
      streetId: "",
    }));

    setEditSettlements([]);
    setEditStreets([]);
    setEditError("");

    if (!municipalityId) {
      return;
    }

    try {
      const data = await getSettlements(municipalityId);
      setEditSettlements(data);
    } catch {
      setEditError("Greška pri učitavanju naselja.");
    }
  };

  const handleEditSettlementChange = async (e) => {
    const settlementId = e.target.value;

    setEditFormData((previous) => ({
      ...previous,
      settlementId,
      streetId: "",
    }));

    setEditStreets([]);
    setEditError("");

    if (!settlementId) {
      return;
    }

    try {
      const data = await getStreets(settlementId);
      setEditStreets(data);
    } catch {
      setEditError("Greška pri učitavanju ulica.");
    }
  };

  const handleEditStreetChange = (e) => {
    setEditFormData((previous) => ({
      ...previous,
      streetId: e.target.value,
    }));
  };

  const handleEditHouseNumberChange = (e) => {
    setEditFormData((previous) => ({
      ...previous,
      houseNumber: e.target.value,
    }));
  };

  const handleEditPhoneChange = (e) => {
    const value = e.target.value.replace(/\D/g, "").slice(0, 9);

    setEditFormData((previous) => ({
      ...previous,
      phoneNumber: value,
    }));
  };

  const handleEditSubmit = async (e, id) => {
    e.preventDefault();

    if (
      !editFormData.municipalityId ||
      !editFormData.settlementId ||
      !editFormData.streetId
    ) {
      setEditError("Opština, naselje i ulica su obavezni.");
      return;
    }

    if (!editFormData.houseNumber.trim()) {
      setEditError("Broj kuće ili zgrade je obavezan.");
      return;
    }

    if (
      editFormData.phoneNumber.length < 8 ||
      editFormData.phoneNumber.length > 9
    ) {
      setEditError("Broj telefona mora sadržati 8 ili 9 cifara nakon +381.");
      return;
    }

    try {
      const response = await fetch(`${API_BASE_URL}/addresses/${id}`, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          streetId: Number(editFormData.streetId),
          houseNumber: editFormData.houseNumber.trim(),
          phoneNumber: `+381${editFormData.phoneNumber}`,
        }),
      });

      if (!response.ok) {
        const errorText = await response.text();
        setEditError(errorText || "Izmena adrese nije uspela.");
        return;
      }

      setEditingId(null);
      setEditError("");
      setEditSettlements([]);
      setEditStreets([]);

      loadAddresses(user.id);
    } catch {
      setEditError("Greška prilikom povezivanja sa serverom.");
    }
  };

  const handleDelete = async (id) => {
    const confirmed = window.confirm(
      "Da li sigurno želiš da obrišeš ovu adresu?",
    );

    if (!confirmed) {
      return;
    }

    try {
      const response = await fetch(`${API_BASE_URL}/addresses/${id}`, {
        method: "DELETE",
      });

      if (!response.ok) {
        setLoadError("Brisanje adrese nije uspelo.");
        return;
      }

      loadAddresses(user.id);
    } catch {
      setLoadError("Greška prilikom brisanja adrese.");
    }
  };

  if (!user) {
    return null;
  }

  return (
    <div className="adrese-container">
      <div className="adrese-content">
        <div className="adrese-header">
          <BackButton to={returnTo || "/kupac"} />

          <h1>Moje adrese</h1>

          <p className="adrese-subtitle">
            Upravljaj svojim lokacijama za dostavu
          </p>
        </div>

        {loadError && <p className="adrese-error">{loadError}</p>}

        {addresses.length === 0 && !loadError && (
          <p className="adrese-empty">Još uvek nemaš sačuvanih adresa.</p>
        )}

        <div className="adrese-list">
          {addresses.map((address) => (
            <div className="adresa-card" key={address.id}>
              {editingId === address.id ? (
                <form
                  className="adresa-edit-form"
                  onSubmit={(e) => handleEditSubmit(e, address.id)}
                >
                  <select
                    value={editFormData.municipalityId}
                    onChange={handleEditMunicipalityChange}
                  >
                    <option value="">Izaberi opštinu</option>

                    {municipalities.map((municipality) => (
                      <option key={municipality.id} value={municipality.id}>
                        {municipality.name}
                      </option>
                    ))}
                  </select>

                  <select
                    value={editFormData.settlementId}
                    onChange={handleEditSettlementChange}
                    disabled={!editFormData.municipalityId}
                  >
                    <option value="">Izaberi naselje</option>

                    {editSettlements.map((settlement) => (
                      <option key={settlement.id} value={settlement.id}>
                        {settlement.name}
                      </option>
                    ))}
                  </select>

                  <select
                    value={editFormData.streetId}
                    onChange={handleEditStreetChange}
                    disabled={!editFormData.settlementId}
                  >
                    <option value="">Izaberi ulicu</option>

                    {editStreets.map((street) => (
                      <option key={street.id} value={street.id}>
                        {street.name}
                      </option>
                    ))}
                  </select>

                  <input
                    type="text"
                    placeholder="Broj kuće / zgrade"
                    value={editFormData.houseNumber}
                    onChange={handleEditHouseNumberChange}
                    maxLength={10}
                  />

                  <div className="phone-input-wrapper">
                    <span className="phone-prefix">+381</span>

                    <input
                      type="tel"
                      placeholder="615648484"
                      value={editFormData.phoneNumber}
                      onChange={handleEditPhoneChange}
                      maxLength={9}
                      inputMode="numeric"
                    />
                  </div>

                  <div className="adresa-actions">
                    <button type="submit" className="adresa-save-button">
                      Sačuvaj
                    </button>

                    <button
                      type="button"
                      className="adresa-cancel-button"
                      onClick={cancelEditing}
                    >
                      Otkaži
                    </button>
                  </div>

                  {editError && <p className="adrese-error">{editError}</p>}
                </form>
              ) : (
                <>
                  <div className="adresa-info">
                    <span className="adresa-street">
                      {address.streetName || address.street}
                      {address.houseNumber ? ` ${address.houseNumber}` : ""}
                    </span>

                    <span className="adresa-city">
                      {address.settlementName || address.city}
                      {address.municipalityName &&
                      address.municipalityName !==
                        (address.settlementName || address.city)
                        ? `, opština ${address.municipalityName}`
                        : ""}
                    </span>

                    {address.phoneNumber ? (
                      <span className="adresa-phone">
                        {address.phoneNumber}
                      </span>
                    ) : (
                      <span className="adresa-phone missing">
                        Broj telefona nije unet
                      </span>
                    )}
                  </div>

                  <div className="adresa-actions">
                    <button
                      type="button"
                      className="adresa-edit-button"
                      onClick={() => startEditing(address)}
                    >
                      Izmeni
                    </button>

                    <button
                      type="button"
                      className="adresa-delete-button"
                      onClick={() => handleDelete(address.id)}
                    >
                      Obriši
                    </button>
                  </div>
                </>
              )}
            </div>
          ))}
        </div>

        <div className="adresa-card adresa-new">
          <h2>Dodaj novu adresu</h2>

          <form onSubmit={handleAddSubmit}>
            <select
              value={formData.municipalityId}
              onChange={handleAddMunicipalityChange}
            >
              <option value="">Izaberi opštinu</option>

              {municipalities.map((municipality) => (
                <option key={municipality.id} value={municipality.id}>
                  {municipality.name}
                </option>
              ))}
            </select>

            <select
              value={formData.settlementId}
              onChange={handleAddSettlementChange}
              disabled={!formData.municipalityId}
            >
              <option value="">Izaberi naselje</option>

              {settlements.map((settlement) => (
                <option key={settlement.id} value={settlement.id}>
                  {settlement.name}
                </option>
              ))}
            </select>

            <select
              value={formData.streetId}
              onChange={handleAddStreetChange}
              disabled={!formData.settlementId}
            >
              <option value="">Izaberi ulicu</option>

              {streets.map((street) => (
                <option key={street.id} value={street.id}>
                  {street.name}
                </option>
              ))}
            </select>

            <input
              type="text"
              placeholder="Broj kuće / zgrade"
              value={formData.houseNumber}
              onChange={handleAddHouseNumberChange}
              maxLength={10}
            />

            <div className="phone-input-wrapper">
              <span className="phone-prefix">+381</span>

              <input
                type="tel"
                placeholder="615648484"
                value={formData.phoneNumber}
                onChange={handleAddPhoneChange}
                maxLength={9}
                inputMode="numeric"
              />
            </div>

            <button type="submit" className="adresa-add-button">
              Dodaj adresu
            </button>

            {formError && <p className="adrese-error">{formError}</p>}
          </form>
        </div>
      </div>
    </div>
  );
}

export default MojeAdrese;
