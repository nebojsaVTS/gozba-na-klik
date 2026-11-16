import "./WelcomePage.scss";
import { useNavigate } from "react-router-dom";
import Footer from "./Footer";

const WelcomePage = () => {
  const navigate = useNavigate();

  return (
    <div className="welcome-layout">
      <main className="welcome-page">
        <section className="welcome-card">
          <div className="welcome-content">
            <h1 className="welcome-title">
              <span className="title-main">Gozba</span>
              <span className="title-accent">na klik</span>
            </h1>

            <p className="subtitle">
              Dobrodošli u Gozbu na klik! <br />
              Registrujte se ili se prijavite
              <br /> kako biste nastavili!
            </p>

            <div className="welcome-actions">
              <button
                type="button"
                className="register-button"
                onClick={() => navigate("/register")}
              >
                Registracija korisnika
              </button>

              <button
                type="button"
                className="login-button"
                onClick={() => navigate("/login")}
              >
                Prijava
              </button>
            </div>
          </div>

          <div className="welcome-image"></div>
        </section>
      </main>

      <Footer />
    </div>
  );
};

export default WelcomePage;
