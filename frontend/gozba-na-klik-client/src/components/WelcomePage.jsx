import "./WelcomePage.scss";
import { useNavigate } from "react-router-dom"

const WelcomePage = () => {
  const navigate = useNavigate();

  return (
    <div className="welcome-container">
      <h1>GOZBA NA KLIK</h1>

      <p className="subtitle">
        Dobrodošli u Gozbu na klik!
        Registrujte se ili se prijavite kako biste nastavili!
      </p>

      <div className="buttons">
        <button onClick={() => navigate("/login")}>👤 Prijava</button>
        <button onClick={() => navigate("/register")}>🔐 Registracija korisnika</button>
      </div>
    </div>
  );
};

export default WelcomePage;