import "./Login.scss";
import { useState, useEffect } from "react";
import { useLocation, useNavigate } from "react-router-dom";

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

  const handleSubmit = (e) => {
    e.preventDefault();

    if (username.trim() === "" || password.trim() === "") {
      setMessage("Sva polja moraju biti popunjena.");
      return;
    }

    setMessage("");

    // TODO: Trenutno nema backend logike koja vraća ulogu korisnika.
    // Privremeno koristimo fiksnu vrednost za testiranje redirect-a.
    // Ovo će biti zamenjeno kada se implementira backend login.

    const uloga = "KUPAC";

    if (uloga === "KUPAC") navigate("/kupac");
    else if (uloga === "VLASNIK RESTORANA") navigate("/vlasnik");
    else if (uloga === "ADMINISTRATOR") navigate("/administrator");
    else if (uloga === "KURIR") navigate("/kurir");
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
