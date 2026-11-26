import "./RestaurantsOverview.scss";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { API_BASE_URL } from "../api";

function RestaurantsOverview() {
  const navigate = useNavigate();

  const [restaurants, setRestaurants] = useState([]);
  const [municipalities, setMunicipalities] = useState([]);
  const [settlements, setSettlements] = useState([]);

  const [selectedMunicipalityId, setSelectedMunicipalityId] = useState("");
  const [selectedSettlementId, setSelectedSettlementId] = useState("");

  const [loading, setLoading] = useState(true);
  const [loadError, setLoadError] = useState("");

  useEffect(() => {
    const loadInitialData = async () => {
      setLoading(true);

      try {
        const [restaurantsResponse, municipalitiesResponse] = await Promise.all(
          [
            fetch(`${API_BASE_URL}/restaurants/all`),
            fetch(`${API_BASE_URL}/Locations/municipalities`),
          ],
        );

        if (!restaurantsResponse.ok || !municipalitiesResponse.ok) {
          throw new Error();
        }

        const restaurantsData = await restaurantsResponse.json();
        const municipalitiesData = await municipalitiesResponse.json();

        setRestaurants(restaurantsData);
        setMunicipalities(municipalitiesData);
        setLoadError("");
      } catch {
        setLoadError("Greška pri učitavanju restorana.");
      } finally {
        setLoading(false);
      }
    };

    loadInitialData();
  }, []);

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
        setSelectedSettlementId("");
      }
    };

    loadSettlements();
  }, [selectedMunicipalityId]);

  const filteredRestaurants = restaurants.filter((restaurant) => {
    const matchesMunicipality =
      !selectedMunicipalityId ||
      restaurant.municipalityId === Number(selectedMunicipalityId);

    const matchesSettlement =
      !selectedSettlementId ||
      restaurant.settlementId === Number(selectedSettlementId);

    return matchesMunicipality && matchesSettlement;
  });

  const handleClearFilters = () => {
    setSelectedMunicipalityId("");
    setSelectedSettlementId("");
    setSettlements([]);
  };

  return (
    <div className="restaurants-overview-container">
      <div className="restaurants-overview-content">
        <div className="restaurants-overview-header">
          <div>
            <h1>Pregled restorana</h1>

            <p>Pregled i upravljanje registrovanim restoranima.</p>
          </div>

          <button
            type="button"
            className="create-restaurant-button"
            onClick={() => navigate("/admin/restaurants/new")}
          >
            + Novi restoran
          </button>
        </div>

        <div className="restaurants-filters">
          <div className="restaurant-filter-field">
            <label htmlFor="municipalityFilter">Opština</label>

            <select
              id="municipalityFilter"
              value={selectedMunicipalityId}
              onChange={(e) => setSelectedMunicipalityId(e.target.value)}
            >
              <option value="">Sve opštine</option>

              {municipalities.map((municipality) => (
                <option key={municipality.id} value={municipality.id}>
                  {municipality.name}
                </option>
              ))}
            </select>
          </div>

          <div className="restaurant-filter-field">
            <label htmlFor="settlementFilter">Naselje</label>

            <select
              id="settlementFilter"
              value={selectedSettlementId}
              onChange={(e) => setSelectedSettlementId(e.target.value)}
              disabled={!selectedMunicipalityId}
            >
              <option value="">
                {selectedMunicipalityId
                  ? "Sva naselja"
                  : "Prvo izaberite opštinu"}
              </option>

              {settlements.map((settlement) => (
                <option key={settlement.id} value={settlement.id}>
                  {settlement.name}
                </option>
              ))}
            </select>
          </div>

          {(selectedMunicipalityId || selectedSettlementId) && (
            <button
              type="button"
              className="clear-restaurant-filters"
              onClick={handleClearFilters}
            >
              Poništi filtere
            </button>
          )}
        </div>

        {loading && (
          <p className="restaurants-overview-message">
            Učitavanje restorana...
          </p>
        )}

        {loadError && <p className="restaurants-overview-error">{loadError}</p>}

        {!loading && !loadError && filteredRestaurants.length === 0 && (
          <div className="restaurants-overview-empty">
            <p>Nema restorana za izabranu lokaciju.</p>
          </div>
        )}

        {!loading && !loadError && filteredRestaurants.length > 0 && (
          <>
            <div className="restaurants-result-count">
              Prikazano restorana: <strong>{filteredRestaurants.length}</strong>
            </div>

            <div className="restaurants-table-wrapper">
              <table className="restaurants-table">
                <thead>
                  <tr>
                    <th>Naziv</th>
                    <th>Lokacija</th>
                    <th>Adresa</th>
                    <th>Telefon</th>
                    <th>Vlasnik</th>
                    <th>Način preuzimanja</th>
                    <th>Akcije</th>
                  </tr>
                </thead>

                <tbody>
                  {filteredRestaurants.map((restaurant) => (
                    <tr key={restaurant.id}>
                      <td className="restaurant-name-cell">
                        {restaurant.name}
                      </td>

                      <td>
                        <div className="restaurant-location">
                          <strong>
                            {restaurant.settlementName || "Nije podešeno"}
                          </strong>

                          {restaurant.municipalityName && (
                            <span>Opština {restaurant.municipalityName}</span>
                          )}
                        </div>
                      </td>

                      <td>{restaurant.address}</td>

                      <td>{restaurant.phoneNumber}</td>

                      <td>{restaurant.ownerUsername}</td>

                      <td>
                        <div className="restaurant-fulfillment">
                          {restaurant.supportsDelivery && (
                            <span className="fulfillment-badge delivery">
                              Dostava
                            </span>
                          )}

                          {restaurant.supportsPickup && (
                            <span className="fulfillment-badge pickup">
                              Lično preuzimanje
                            </span>
                          )}
                        </div>
                      </td>

                      <td>
                        <div className="restaurant-actions">
                          <button
                            type="button"
                            className="edit-restaurant-button"
                            onClick={() =>
                              navigate(
                                `/admin/restaurants/${restaurant.id}/edit`,
                              )
                            }
                          >
                            Izmeni
                          </button>

                          {restaurant.supportsDelivery ? (
                            <button
                              type="button"
                              className="delivery-zones-button"
                              onClick={() =>
                                navigate(
                                  `/admin/restaurants/${restaurant.id}/delivery-zones`,
                                )
                              }
                            >
                              Zone dostave
                            </button>
                          ) : (
                            <span className="pickup-only-label">
                              Samo preuzimanje
                            </span>
                          )}
                        </div>
                      </td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          </>
        )}
      </div>
    </div>
  );
}

export default RestaurantsOverview;
