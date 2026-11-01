import { useState } from "react";
import "./MenuItemCustomizationModal.scss";

const AVAILABLE_SIDES = [
  "Luk",
  "Paradajz",
  "Kupus",
  "Kečap",
  "Majonez",
  "Urnebes",
];

const MenuItemCustomizationModal = ({
  menuItem,
  onClose,
  onAddToCart,
}) => {
  const [selectedSides, setSelectedSides] = useState([]);
  const [note, setNote] = useState("");

  const toggleSide = (side) => {
    setSelectedSides((currentSides) =>
      currentSides.includes(side)
        ? currentSides.filter((item) => item !== side)
        : [...currentSides, side]
    );
  };

  const handleAdd = () => {
    onAddToCart({
      ...menuItem,
      selectedSides,
      note: note.trim(),
    });
  };

  return (
    <div className="customization-overlay">
      <div className="customization-modal">
        <button
          type="button"
          className="customization-close"
          onClick={onClose}
        >
          ×
        </button>

        <h2>{menuItem.name}</h2>
        <p className="customization-price">
          {menuItem.price} RSD
        </p>

        <div className="customization-section">
          <h3>Izaberi priloge</h3>

          <div className="sides-grid">
            {AVAILABLE_SIDES.map((side) => (
              <label className="side-option" key={side}>
                <input
                  type="checkbox"
                  checked={selectedSides.includes(side)}
                  onChange={() => toggleSide(side)}
                />
                <span>{side}</span>
              </label>
            ))}
          </div>
        </div>

        <div className="customization-section">
          <label className="note-label">
            Napomena za restoran
            <textarea
              value={note}
              onChange={(event) => setNote(event.target.value)}
              placeholder="Na primer: dobro pečeno, bez krastavca..."
              maxLength={200}
            />
          </label>
        </div>

        <button
          type="button"
          className="customization-add-button"
          onClick={handleAdd}
        >
          Dodaj u korpu – {menuItem.price} RSD
        </button>
      </div>
    </div>
  );
};

export default MenuItemCustomizationModal;