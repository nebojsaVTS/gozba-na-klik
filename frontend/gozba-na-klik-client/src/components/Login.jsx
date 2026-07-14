import "./Login.scss";
import { useState, useEffect } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { API_BASE_URL } from "../api";

const Login = () => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [message, setMessage] = useState("");
  const [logoutMessage, setLogoutMessage] = useState();
  const navigate = useNavigate();
  const location = useLocation();

  useEffect(() => {
    if (location.state?.logoutMessage) {
      setLogoutMessage(location.state.logoutMessage);

      const timer = setTimeout(() => {
        setLogoutMessage("");
      }, 5000);
      return () => clearTimeout(timer);
    }
  }, [location.state]);

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (username.trim() === "" || password.trim() === "") {
      setMessage("Sva polja moraju biti popunjena.");
      return;
    }

    setMessage("");

    try {
      const response = await fetch(`${API_BASE_URL}/auth/login`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ username, password }),
      });

      if (!response.ok) {
        const errorText = await response.text();
        setMessage(errorText || "Neispravno korisničko ime ili lozinka.");
        return;
      }

      const user = await response.json();

      if (user.role === "Kupac") navigate("/kupac");
      else if (user.role === "Vlasnik restorana") navigate("/vlasnik");
      else if (user.role === "Administrator") navigate("/administrator");
      else if (user.role === "Kurir") navigate("/kurir");
    } catch {
      setMessage("Greška prilikom povezivanja sa serverom.");
    }
  };

  return (
    <div className="login-container">
      {logoutMessage && <p className="logout-toast">{logoutMessage}</p>}
      <div className="login-card">
        <h2>Prijava</h2>

        <form className="login-form" onSubmit={handleSubmit}>
          <input
            type="text"
            placeholder="Username"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
          />

          <input
            type="password"
            placeholder="Password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />

          <button type="submit">Prijavi se</button>

          {message && <p className="login-message error">{message}</p>}
        </form>
      </div>
    </div>
  );
};

export default Login;
