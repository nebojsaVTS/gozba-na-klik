import { useState } from "react";
import "./Register.scss";
import { useNavigate } from "react-router-dom";
import { API_BASE_URL } from "../api";

function Register() {
  const navigate = useNavigate();
  const [formData, setFormData] = useState({
    username: "",
    password: "",
    email: "",
  });

  const [message, setMessage] = useState("");
  const [isError, setIsError] = useState(false);

  function handleChange(event) {
    setFormData({
      ...formData,
      [event.target.name]: event.target.value,
    });
  }

  async function handleSubmit(event) {
    event.preventDefault();

    if (
      !formData.username.trim() ||
      !formData.password.trim() ||
      !formData.email.trim()
    ) {
      setMessage("Sva polja moraju biti popunjena.");
      setIsError(true);
      return;
    }

    try {
      const response = await fetch(`${API_BASE_URL}/auth/register`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(formData),
      });

      const data = await response.json();

      if (!response.ok) {
        const errorMessage =
          data.message ||
          Object.values(data.errors ?? {}).flat().join(" ") ||
          "Registracija nije uspela.";
        setMessage(errorMessage);
        setIsError(true);
        return;
      }

      setMessage(data.message || "Uspesna registracija!");
      setIsError(false);

      setFormData({
        username: "",
        password: "",
        email: "",
      });

      setTimeout(() => {
        navigate("/login");
      }, 2000);
    } catch {
      setMessage("Greška prilikom povezivanja sa serverom.");
      setIsError(true);
    }
  }

  return (
    <div className="register-container">
      <div className="register-card">
        <h2>Registracija</h2>

        <form className="register-form" onSubmit={handleSubmit}>
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

        {message && (
          <p className={isError ? "register-message error" : "success-message"}>
            {message}
          </p>
        )}
      </div>
    </div>
  );
}

export default Register;
