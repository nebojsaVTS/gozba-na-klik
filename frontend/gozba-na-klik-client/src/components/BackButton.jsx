import { useNavigate } from "react-router-dom";
import "./BackButton.scss";

const BackButton = ({ to }) => {
  const navigate = useNavigate();

  const handleBack = () => {
    if (to) {
      navigate(to);
    } else {
      navigate(-1);
    }
  };

  return (
    <button
      type="button"
      className="back-button"
      onClick={handleBack}
      aria-label="Nazad"
      title="Nazad"
    >
      <span className="back-button-arrow">←</span>
      <span className="back-button-text">Nazad</span>
    </button>
  );
};

export default BackButton;
