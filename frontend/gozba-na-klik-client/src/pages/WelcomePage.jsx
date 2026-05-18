import "./WelcomePage.css";

const WelcomePage = () => {
  return (
    <div className="welcome-container">
      <h1>GOZBA NA KLIK</h1>

      <p className="subtitle">
        Dobrodošli u Gozbu na klik!
        Registrujte se ili se prijavite kako biste nastavili!
      </p>

      <div className="buttons">
        <button>👤 Prijava</button>
        <button>🔐 Registracija korisnika</button>
      </div>
    </div>
  );
};

export default WelcomePage;