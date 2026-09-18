import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { API_BASE_URL } from "../api";

function CreateRestaurant() {
    const navigate = useNavigate();

    const [owners, setOwners] = useState([]);
    const [formData, setFormData] = useState({
        name: "",
        address: "",
        phoneNumber: "",
        ownerId: "",
    });

    const [message, setMessage] = useState("");
    const [isError, setIsError] = useState(false);


    useEffect(() => {
        fetch(`${API_BASE_URL}/admin/users`)
        .then((response) => response.json())
        .then((data) => {
            const restaurantOwners = data.filter((u) => u.role === "Vlasnik restorana");
            setOwners(restaurantOwners);
        })
        .catch(() => setMessage("Greska pri ucitavanju vlasnika!"));
    }, []);


    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData({...formData, [name]: value });
    };


    const handleSubmit = async (e) => {
        e.preventDefault();

        if (
            !formData.name.trim() ||
            !formData.address.trim() ||
            !formData.phoneNumber.trim()  ||
            !formData.ownerId
        )
        {
            setMessage("Sva polja su obavezna.");
            setIsError(true);
            return;

        }

        try {
            const response = await fetch(`${API_BASE_URL}/restaurants`, {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    name: formData.name,
                    address: formData.address,
                    phoneNumber: formData.phoneNumber,
                    ownerId: Number(formData.ownerId),

                }),
            });

            if (!response.ok) {
                const errorText = await response.text();
                setMessage(errorText || "Kreiranje restorana nije uspelo!");
                setIsError(true);
                return;
            }

            setMessage("Restoran je uspesno kreiran!");
            setIsError(false);

            setTimeout(() => {
                navigate("/admin/restaurants");
            }, 1500);
        } catch {
            setMessage("Greska prilikom povezivanja sa serverom.");
            setIsError(true);
        }
    };

    return (
        <div>
            <h2>Kreiranje restorana</h2>

            <form onSubmit={handleSubmit}>
                <input
                    type="text"
                    name="name"
                    placeholder="Naziv restorana"
                    value={formData.name}
                    onChange={handleChange}
                />
                <input
                    type="text"
                    name="address"
                    placeholder="Adresa"
                    value={formData.address}
                    onChange={handleChange}
                />
                <input
                    type="text"
                    name="phoneNumber"
                    placeholder="Telefon"
                    value={formData.phoneNumber}
                    onChange={handleChange}
                />

                <select name="ownerId" value={formData.ownerId} onChange={handleChange}>
                    <option value="">Izaberi vlasnika</option>
                    {owners.map((owner) => (
                        <option key={owner.id} value={owner.id}>
                            {owner.username}
                        </option>
                    ))}
                </select>

                <button type="submit">Kreiraj restoran</button>
            </form>

            {message && <p className={isError ? "error" : ""}>{message}</p>}
        </div>
    );
}

export default CreateRestaurant;
