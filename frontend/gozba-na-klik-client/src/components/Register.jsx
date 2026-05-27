import { useState } from "react";
import "./Register.scss";
import { useNavigate } from "react-router-dom";

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

  function handleSubmit(event) {
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

    setMessage("Uspesna registracija!");
    setIsError(false);

    setFormData({
      username: "",
      password: "",
      email: "",
    });

    setTimeout(() => {
      navigate("/login");
    }, 2000);
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
