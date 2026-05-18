import { useState } from "react";
import "./Register.css";

function Register() {
    const [formData, setFormData] = useState({
        username: "",
        password: "",
        email: "",
    });

    const [message, setMessage] = useState("");

    function handleChange(event) {
        setFormData ({
            ...formData,
            [event.target.name]: event.target.value,
        });
    }

    function handleSubmit(event) {
        event.preventDefault();

        if(
            !formData.username ||
            !formData.password ||
            !formData.email
        ){
            setMessage("Sva polja moraju biti popunjena.");
            return;
        }

        setMessage("Uspesna registracija!")

        setFormData({
            username: "",
            password: "",
            email: "",
        });
    }


    return (
        <div className="register-container">
            <h2>Registracija</h2>

            <form onSubmit={handleSubmit}>
                <input
                    type="text"
                    name="username"
                    placeholder="Username"
                    value={formData.username}
                    onChange={handleChange}
                    autoComplete="off"
                />

                <input 
                    type="password"
                    name="password"
                    placeholder="Password"
                    value={formData.password}
                    onChange={handleChange}
                    autoComplete="new-password"
                />

                <input 
                    type="email"
                    name="email"
                    placeholder="Email"
                    value={formData.email}
                    onChange={handleChange}
                    autoComplete="off"
                />
            



                <button type="submit">Registruj se</button>
            </form>

            {message && <p>{message}</p>}
        </div>
    );
}

export default Register;