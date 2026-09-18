import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { API_BASE_URL } from  "../api";

function MojProfil() {
    const navigate = useNavigate();
    const [user, setUser] = useState (null);
    const [formData, setFormData] = useState({ email: "", password: ""});
    const [message, setMessage] = useState("");
    const [isError, setIsError] = useState(false);

    useEffect(() => {
        const stored = localStorage.getItem("gozbaUser");
        if (!stored) {
            navigate("/login");
            return;
        }
        const parsedUser = JSON.parse(stored);
        setUser(parsedUser);
        setFormData({ email: parsedUser.email, password: ""});
    }, [navigate]);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData({ ...formData, [name]: value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        if (!formData.email.trim()) {
            setMessage("Email je obavezan");
            setIsError(true);
            return;
        }

        try {
            const response = await fetch(`${API_BASE_URL}/users/${user.id}`, {
                method: "PUT",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    email: formData.email,
                    password: formData.password || null,
                }),
            });

            if (!response.ok) {
                const errorText = await response.text();
                setMessage(errorText || "Izmena podataka nije uspela.");
                setIsError(true);
                return;
            }

            const data = await response.json();
            localStorage.setItem("gozbaUser", JSON.stringify(data));
            setUser(data);
            setFormData({email: data.email, password: "" });
            setMessage("Podatci su uspesno izmenjeni.");
            setIsError(false);
        } catch {
            setMessage("Greska prilikom povezivanja sa serverom.");
            setIsError(true);
        }
      };

      if (!user) {
        return null;
      }

      return (
        <div>
            <h2>Moj profil</h2>

            <form onSubmit={handleSubmit}>
                <input type="text" value={user.username} disabled />

                <input 
                    type="email"
                    name="email"
                    placeholder="Email"
                    value={formData.email}
                    onChange={handleChange}
                />

                <input
                    type="password"
                    name="password"
                    placeholder="Nova lozinka (ostavi prazno polje ako ne menjas)"
                    value={formData.password}
                    onChange={handleChange}
                />

                <button type="submit">Sacuvaj izmene</button>
            </form>

            {message && <p className={isError ? "error" : ""}>{message}</p>}
            </div>
         );
        }

        export default MojProfil;