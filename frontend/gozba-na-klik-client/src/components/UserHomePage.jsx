import { useNavigate } from "react-router-dom";

const UserHomePage = ({ title, showRestaurants = false }) => {
  const navigate = useNavigate();

  const handleRestaurants = () => {
    navigate("/restaurants");
  };

  const handleLogout = () => {
    navigate("/login", { state: { logoutMessage: "Uspešno ste se odjavili" } });
  };

  return (
    <div>
      <h2>{title}</h2>

      {showRestaurants && (
        <button type="button" onClick={handleRestaurants}>
          Restorani
        </button>
      )}

      <button type="button" onClick={handleLogout}>
        Odjavi se
      </button>
    </div>
  );
};

export default UserHomePage;
