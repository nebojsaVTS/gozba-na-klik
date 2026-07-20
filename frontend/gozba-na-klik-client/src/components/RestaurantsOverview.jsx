import "./RestaurantsOverview.scss";
import { useState, useEffect } from "react";
import { API_BASE_URL } from "../api";

function RestaurantsOverview() {
    const [restaurants, setRestaurants] = useState([]);
    const [loadError, setLoadError] = useState("");

    useEffect(() => {
        fetch(`${API_BASE_URL}/restaurants/all`)
            .then((response) => {
                if(!response.ok) {
                    throw new Error("Greska pri ucitavanju restorana!");
                }
                return response.json();
            })
            .then((data) =>setRestaurants(data))
            .catch(() => setLoadError("Greska pri ucitavanju restorana!"));
    }, []);

    return (
        <div className="restaurants-overview-container">
            <h2>Pregled restorana</h2>

            {loadError && <p className="error">{loadError}</p>}

            {!loadError && restaurants.length === 0 && <p>Nema restorana.</p>}

            {restaurants.length > 0 && (
                <table className="restaurants-table">
                    <thead>
                        <tr>
                            <th>Naziv</th>
                            <th>Adresa</th>
                            <th>Telefon</th>
                            <th>Vlasnik</th>
                        </tr>
                    </thead>
                    <tbody>
                        {restaurants.map((r) => (
                            <tr key={r.id}>
                                <td>{r.name}</td>
                                <td>{r.address}</td>
                                <td>{r.phoneNumber}</td>
                                <td>{r.ownerUsername}</td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            )}

        </div>
    );
}

export default RestaurantsOverview;