import "./RestaurantDeliveryZones.scss";
import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import BackButton from "./BackButton";
import { API_BASE_URL } from "../api";

function RestaurantDeliveryZones() {
  const { restaurantId } = useParams();

  const [zones, setZones] = useState([]);
  const [municipalities, setMunicipalities] = useState([]);
  const [settlements, setSettlements] = useState([]);

  const [loading, setLoading] = useState(true);
  const [loadError, setLoadError] = useState("");

  const [showAddForm, setShowAddForm] = useState(false);

  const [selectedMunicipalityId, setSelectedMunicipalityId] = useState("");
  const [selectedSettlementId, setSelectedSettlementId] = useState("");

  const [deliveryFee, setDeliveryFee] = useState("");
  const [minimumOrderAmount, setMinimumOrderAmount] = useState("");

  const [formError, setFormError] = useState("");
  const [saving, setSaving] = useState(false);

  const [editZoneId, setEditZoneId] = useState(null);
  const [editDeliveryFee, setEditDeliveryFee] = useState("");
  const [editMinimumOrderAmount, setEditMinimumOrderAmount] = useState("");

  const [actionSavingId, setActionSavingId] = useState(null);
  const [actionError, setActionError] = useState("");

  const loadZones = async () => {
    try {
      const response = await fetch(
        `${API_BASE_URL}/restaurants/${restaurantId}/delivery-zones`,
      );

      if (!response.ok) {
        throw new Error();
      }

      const data = await response.json();

      setZones(data);
      setLoadError("");
    } catch {
      setLoadError("Greška pri učitavanju zona dostave.");
    }
  };

  useEffect(() => {
    const loadData = async () => {
      setLoading(true);

      try {
        const [zonesResponse, municipalitiesResponse] = await Promise.all([
          fetch(`${API_BASE_URL}/restaurants/${restaurantId}/delivery-zones`),
          fetch(`${API_BASE_URL}/Locations/municipalities`),
        ]);

        if (!zonesResponse.ok || !municipalitiesResponse.ok) {
          throw new Error();
        }

        const zonesData = await zonesResponse.json();
        const municipalitiesData = await municipalitiesResponse.json();

        setZones(zonesData);
        setMunicipalities(municipalitiesData);
        setLoadError("");
      } catch {
        setLoadError("Greška pri učitavanju podataka.");
      } finally {
        setLoading(false);
      }
    };

    loadData();
  }, [restaurantId]);

  useEffect(() => {
    if (!selectedMunicipalityId) {
      setSettlements([]);
      setSelectedSettlementId("");
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
        setSelectedSettlementId("");
      } catch {
        setSettlements([]);
        setFormError("Greška pri učitavanju naselja.");
      }
    };

    loadSettlements();
  }, [selectedMunicipalityId]);

  const resetForm = () => {
    setSelectedMunicipalityId("");
    setSelectedSettlementId("");
    setDeliveryFee("");
    setMinimumOrderAmount("");
    setSettlements([]);
    setFormError("");
  };

  const handleCancelAdd = () => {
    resetForm();
    setShowAddForm(false);
  };

  const handleAddZone = async (event) => {
    event.preventDefault();

    setFormError("");

    if (!selectedMunicipalityId) {
      setFormError("Izaberite opštinu.");
      return;
    }

    if (!selectedSettlementId) {
      setFormError("Izaberite naselje.");
      return;
    }

    if (deliveryFee === "") {
      setFormError("Unesite cenu dostave.");
      return;
    }

    const parsedDeliveryFee = Number(deliveryFee);

    if (Number.isNaN(parsedDeliveryFee) || parsedDeliveryFee < 0) {
      setFormError("Cena dostave ne može biti negativna.");
      return;
    }

    let parsedMinimumOrderAmount = null;

    if (minimumOrderAmount !== "") {
      parsedMinimumOrderAmount = Number(minimumOrderAmount);

      if (
        Number.isNaN(parsedMinimumOrderAmount) ||
        parsedMinimumOrderAmount < 0
      ) {
        setFormError("Minimalna porudžbina ne može biti negativna.");
        return;
      }
    }

    setSaving(true);

    try {
      const response = await fetch(
        `${API_BASE_URL}/restaurants/${restaurantId}/delivery-zones`,
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({
            settlementId: Number(selectedSettlementId),
            deliveryFee: parsedDeliveryFee,
            minimumOrderAmount: parsedMinimumOrderAmount,
          }),
        },
      );

      if (!response.ok) {
        const errorText = await response.text();

        if (
          errorText.includes("Delivery zone for this settlement already exists")
        ) {
          throw new Error("Ovo naselje je već dodato kao zona dostave.");
        }

        throw new Error("Greška pri dodavanju zone dostave.");
      }

      await loadZones();

      resetForm();
      setShowAddForm(false);
    } catch (error) {
      setFormError(error.message || "Greška pri dodavanju zone dostave.");
    } finally {
      setSaving(false);
    }
  };

  const handleEditStart = (zone) => {
    setActionError("");

    setEditZoneId(zone.id);
    setEditDeliveryFee(String(zone.deliveryFee));

    setEditMinimumOrderAmount(
      zone.minimumOrderAmount == null ? "" : String(zone.minimumOrderAmount),
    );
  };

  const handleEditCancel = () => {
    setEditZoneId(null);
    setEditDeliveryFee("");
    setEditMinimumOrderAmount("");
    setActionError("");
  };

  const handleEditSave = async (zone) => {
    setActionError("");

    if (editDeliveryFee === "") {
      setActionError("Cena dostave je obavezna.");
      return;
    }

    const parsedDeliveryFee = Number(editDeliveryFee);

    if (Number.isNaN(parsedDeliveryFee) || parsedDeliveryFee < 0) {
      setActionError("Cena dostave ne može biti negativna.");
      return;
    }

    let parsedMinimumOrderAmount = null;

    if (editMinimumOrderAmount !== "") {
      parsedMinimumOrderAmount = Number(editMinimumOrderAmount);

      if (
        Number.isNaN(parsedMinimumOrderAmount) ||
        parsedMinimumOrderAmount < 0
      ) {
        setActionError("Minimalna porudžbina ne može biti negativna.");
        return;
      }
    }

    setActionSavingId(zone.id);

    try {
      const response = await fetch(
        `${API_BASE_URL}/restaurants/${restaurantId}/delivery-zones/${zone.id}`,
        {
          method: "PUT",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({
            settlementId: zone.settlementId,
            deliveryFee: parsedDeliveryFee,
            minimumOrderAmount: parsedMinimumOrderAmount,
          }),
        },
      );

      if (!response.ok) {
        throw new Error();
      }

      await loadZones();

      setEditZoneId(null);
      setEditDeliveryFee("");
      setEditMinimumOrderAmount("");
    } catch {
      setActionError("Greška pri izmeni zone dostave.");
    } finally {
      setActionSavingId(null);
    }
  };

  const handleDeleteZone = async (zone) => {
    const confirmed = window.confirm(
      `Da li ste sigurni da želite da obrišete dostavu za naselje ${zone.settlementName}?`,
    );

    if (!confirmed) {
      return;
    }

    setActionError("");
    setActionSavingId(zone.id);

    try {
      const response = await fetch(
        `${API_BASE_URL}/restaurants/${restaurantId}/delivery-zones/${zone.id}`,
        {
          method: "DELETE",
        },
      );

      if (!response.ok) {
        throw new Error();
      }

      await loadZones();

      if (editZoneId === zone.id) {
        handleEditCancel();
      }
    } catch {
      setActionError("Greška pri brisanju zone dostave.");
    } finally {
      setActionSavingId(null);
    }
  };

  const availableSettlements = settlements.filter(
    (settlement) => !zones.some((zone) => zone.settlementId === settlement.id),
  );

  return (
    <div className="delivery-zones-page">
      <div className="delivery-zones-content">
        <div className="delivery-zones-header">
          <BackButton to="/admin/restaurants" />

          <h1>Zone dostave</h1>

          <div className="delivery-zones-header-placeholder" />
        </div>

        {!loading && !loadError && (
          <div className="delivery-zones-toolbar">
            <button
              type="button"
              className="add-delivery-zone-button"
              onClick={() => {
                if (showAddForm) {
                  resetForm();
                }

                setShowAddForm((current) => !current);
              }}
            >
              {showAddForm ? "Zatvori" : "+ Dodaj zonu"}
            </button>
          </div>
        )}

        {showAddForm && (
          <form className="delivery-zone-form" onSubmit={handleAddZone}>
            <h2>Nova zona dostave</h2>

            <div className="delivery-zone-form-grid">
              <div className="delivery-zone-form-field">
                <label htmlFor="municipality">Opština</label>

                <select
                  id="municipality"
                  value={selectedMunicipalityId}
                  onChange={(event) => {
                    setSelectedMunicipalityId(event.target.value);
                    setFormError("");
                  }}
                >
                  <option value="">Izaberite opštinu</option>

                  {municipalities.map((municipality) => (
                    <option key={municipality.id} value={municipality.id}>
                      {municipality.name}
                    </option>
                  ))}
                </select>
              </div>

              <div className="delivery-zone-form-field">
                <label htmlFor="settlement">Naselje</label>

                <select
                  id="settlement"
                  value={selectedSettlementId}
                  onChange={(event) => {
                    setSelectedSettlementId(event.target.value);
                    setFormError("");
                  }}
                  disabled={!selectedMunicipalityId}
                >
                  <option value="">
                    {selectedMunicipalityId
                      ? "Izaberite naselje"
                      : "Prvo izaberite opštinu"}
                  </option>

                  {availableSettlements.map((settlement) => (
                    <option key={settlement.id} value={settlement.id}>
                      {settlement.name}
                    </option>
                  ))}
                </select>

                {selectedMunicipalityId &&
                  settlements.length > 0 &&
                  availableSettlements.length === 0 && (
                    <small>Sva naselja iz ove opštine su već dodata.</small>
                  )}
              </div>

              <div className="delivery-zone-form-field">
                <label htmlFor="deliveryFee">Cena dostave (RSD)</label>

                <input
                  id="deliveryFee"
                  type="number"
                  min="0"
                  step="1"
                  value={deliveryFee}
                  onChange={(event) => {
                    setDeliveryFee(event.target.value);
                    setFormError("");
                  }}
                  placeholder="npr. 200"
                />

                <small>Unesite 0 za besplatnu dostavu.</small>
              </div>

              <div className="delivery-zone-form-field">
                <label htmlFor="minimumOrderAmount">
                  Minimalna porudžbina (RSD)
                </label>

                <input
                  id="minimumOrderAmount"
                  type="number"
                  min="0"
                  step="1"
                  value={minimumOrderAmount}
                  onChange={(event) => {
                    setMinimumOrderAmount(event.target.value);
                    setFormError("");
                  }}
                  placeholder="npr. 1000"
                />

                <small>Ostavite prazno ako nema minimuma.</small>
              </div>
            </div>

            {formError && (
              <p className="delivery-zone-form-error">{formError}</p>
            )}

            <div className="delivery-zone-form-actions">
              <button
                type="submit"
                className="save-delivery-zone-button"
                disabled={saving}
              >
                {saving ? "Čuvanje..." : "Dodaj zonu"}
              </button>

              <button
                type="button"
                className="cancel-delivery-zone-button"
                onClick={handleCancelAdd}
                disabled={saving}
              >
                Odustani
              </button>
            </div>
          </form>
        )}

        {actionError && (
          <p className="delivery-zone-action-error">{actionError}</p>
        )}

        {loading && (
          <p className="delivery-zones-message">Učitavanje zona dostave...</p>
        )}

        {loadError && <p className="delivery-zones-error">{loadError}</p>}

        {!loading && !loadError && zones.length === 0 && (
          <div className="delivery-zones-empty">
            <p>Ovaj restoran trenutno nema podešene zone dostave.</p>
          </div>
        )}

        {!loading && !loadError && zones.length > 0 && (
          <div className="delivery-zones-list">
            {zones.map((zone) => {
              const isEditing = editZoneId === zone.id;

              return (
                <div className="delivery-zone-card" key={zone.id}>
                  <div className="delivery-zone-main">
                    <h2>{zone.settlementName}</h2>
                    <p>{zone.municipalityName}</p>
                  </div>

                  {isEditing ? (
                    <div className="delivery-zone-edit">
                      <div className="delivery-zone-edit-field">
                        <label>Cena dostave</label>

                        <input
                          type="number"
                          min="0"
                          step="1"
                          value={editDeliveryFee}
                          onChange={(event) =>
                            setEditDeliveryFee(event.target.value)
                          }
                        />
                      </div>

                      <div className="delivery-zone-edit-field">
                        <label>Minimalna porudžbina</label>

                        <input
                          type="number"
                          min="0"
                          step="1"
                          value={editMinimumOrderAmount}
                          onChange={(event) =>
                            setEditMinimumOrderAmount(event.target.value)
                          }
                          placeholder="Bez minimuma"
                        />
                      </div>

                      <div className="delivery-zone-edit-actions">
                        <button
                          type="button"
                          className="delivery-zone-save-button"
                          onClick={() => handleEditSave(zone)}
                          disabled={actionSavingId === zone.id}
                        >
                          {actionSavingId === zone.id
                            ? "Čuvanje..."
                            : "Sačuvaj"}
                        </button>

                        <button
                          type="button"
                          className="delivery-zone-cancel-button"
                          onClick={handleEditCancel}
                          disabled={actionSavingId === zone.id}
                        >
                          Odustani
                        </button>
                      </div>
                    </div>
                  ) : (
                    <>
                      <div className="delivery-zone-details">
                        <div className="delivery-zone-detail">
                          <span>Cena dostave</span>

                          <strong>
                            {zone.deliveryFee === 0
                              ? "Besplatna"
                              : `${zone.deliveryFee} RSD`}
                          </strong>
                        </div>

                        <div className="delivery-zone-detail">
                          <span>Minimalna porudžbina</span>

                          <strong>
                            {zone.minimumOrderAmount == null
                              ? "Nema minimuma"
                              : `${zone.minimumOrderAmount} RSD`}
                          </strong>
                        </div>
                      </div>

                      <div className="delivery-zone-actions">
                        <button
                          type="button"
                          className="delivery-zone-edit-button"
                          onClick={() => handleEditStart(zone)}
                        >
                          Izmeni
                        </button>

                        <button
                          type="button"
                          className="delivery-zone-delete-button"
                          onClick={() => handleDeleteZone(zone)}
                          disabled={actionSavingId === zone.id}
                        >
                          {actionSavingId === zone.id
                            ? "Brisanje..."
                            : "Obriši"}
                        </button>
                      </div>
                    </>
                  )}
                </div>
              );
            })}
          </div>
        )}
      </div>
    </div>
  );
}

export default RestaurantDeliveryZones;
