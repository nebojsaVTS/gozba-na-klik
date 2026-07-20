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
            const reposnse = await fetch(`${API_BASE_URL}/restaurants`, {
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
                const errorText = await respons.text();
                setMessage(errorText || "Kreiranje restorana nije uspelo!");
                setIsError(true);
                return;
            }

            setMessage("Restoran je uspesno kreiran!");
            setIsError(false);
        }
    }
}