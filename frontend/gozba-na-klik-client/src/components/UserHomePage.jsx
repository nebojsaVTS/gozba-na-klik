import { useNavigate } from "react-router-dom";

const UserHomePage = ({ title }) => {
  const navigate = useNavigate();

  const handleLogout = () => {
    navigate("/login", { state: { logoutMessage: "Uspešno ste se odjavili" } });
  };

  return (
    <div>
      <h2>{title}</h2>
      <button type="button" onClick={handleLogout}>
        Odjavi se
      </button>
    </div>
  );
};

export default UserHomePage;
