import "./MojeAdrese.scss";
import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { API_BASE_URL } from "../api";

function MojeAdrese() {
    const navigate = useNavigate();
    const [user, setUser] = useState(null);
    const [addresses, setAddresses] = useState([]);
    const [loadError, setLoadError] = useState("");

    
    const [formData, setFormData] = useState({ street: "", city: "" });
    const [formError, setFormError] = useState("");

    const [editingId, setEditingId] = useState(null);
    const [editFormData, setEditFormData] = useState({ street: "", city: "" });
    const [editError, setEditError] = useState("");

  useEffect(() => {
    const stored = localStorage.getItem("gozbaUser");
    if (!stored) {
      navigate("/login");
      return;
    }
    setUser(JSON.parse(stored));
  }, [navigate]);

  const loadAddresses = (userId) => {
    fetch(`${API_BASE_URL}/addresses?userId=${userId}`)
      .then((response) => {
        if (!response.ok) {
          throw new Error("Greška pri učitavanju adresa.");
        }
        return response.json();
      })
      .then((data) => {
        setAddresses(data);
        setLoadError("");
      })
      .catch(() => setLoadError("Greška pri učitavanju adresa."));
  };

  useEffect(() => {
    if (user) {
      loadAddresses(user.id);
    }
  }, [user]);

  const handleAddChange = (e) => {
    const { name, value } = e.target;
    setFormData({ ...formData, [name]: value });
  };

  const handleAddSubmit = async (e) => {
    e.preventDefault();

    if (!formData.street.trim() || !formData.city.trim()) {
      setFormError("Sva polja su obavezna.");
      return;
    }

    try {
      const response = await fetch(`${API_BASE_URL}/addresses`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          street: formData.street,
          city: formData.city,
          userId: user.id,
        }),
      });

      if (!response.ok) {
        const errorText = await response.text();
        setFormError(errorText || "Dodavanje adrese nije uspelo.");
        return;
      }

      setFormError("");
      setFormData({ street: "", city: "" });
      loadAddresses(user.id);
    } catch {
      setFormError("Greška prilikom povezivanja sa serverom.");
    }
  };

  const startEditing = (address) => {
    setEditingId(address.id);
    setEditFormData({ street: address.street, city: address.city });
    setEditError("");
  };

  const cancelEditing = () => {
    setEditingId(null);
    setEditError("");
  };

  const handleEditChange = (e) => {
    const { name, value } = e.target;
    setEditFormData({ ...editFormData, [name]: value });
  };

  const handleEditSubmit = async (e, id) => {
    e.preventDefault();

    if (!editFormData.street.trim() || !editFormData.city.trim()) {
      setEditError("Sva polja su obavezna.");
      return;
    }

    try {
      const response = await fetch(`${API_BASE_URL}/addresses/${id}`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          street: editFormData.street,
          city: editFormData.city,
        }),
      });

      if (!response.ok) {
        const errorText = await response.text();
        setEditError(errorText || "Izmena adrese nije uspela.");
        return;
      }

      setEditingId(null);
      loadAddresses(user.id);
    } catch {
      setEditError("Greška prilikom povezivanja sa serverom.");
    }
  };

  const handleDelete = async (id) => {
    const confirmed = window.confirm("Da li sigurno želiš da obrišeš ovu adresu?");
    if (!confirmed) {
      return;
    }

    try {
      const response = await fetch(`${API_BASE_URL}/addresses/${id}`, {
        method: "DELETE",
      });

      if (!response.ok) {
        return;
      }

      loadAddresses(user.id);
    } catch {
      setLoadError("Greška prilikom brisanja adrese.");
    }
  };

  if (!user) {
    return null;
  }


   return (
    <div className="adrese-container">
      <div className="adrese-content">
        <div className="adrese-header">
          <button type="button" className="adrese-back-button" onClick={() => navigate("/kupac")}>
            ← Nazad
          </button>
          <h1>Moje adrese</h1>
          <p className="adrese-subtitle">Upravljaj svojim lokacijama za dostavu</p>
        </div>

        {loadError && <p className="adrese-error">{loadError}</p>}

        {addresses.length === 0 && !loadError && (
          <p className="adrese-empty">Još uvek nemaš sačuvanih adresa.</p>
        )}

        <div className="adrese-list">
          {addresses.map((address) => (
            <div className="adresa-card" key={address.id}>
              {editingId === address.id ? (
                <form className="adresa-edit-form" onSubmit={(e) => handleEditSubmit(e, address.id)}>
                  <input
                    type="text"
                    name="street"
                    placeholder="Ulica i broj"
                    value={editFormData.street}
                    onChange={handleEditChange}
                  />
                  <input
                    type="text"
                    name="city"
                    placeholder="Grad"
                    value={editFormData.city}
                    onChange={handleEditChange}
                  />
                  <div className="adresa-actions">
                    <button type="submit" className="adresa-save-button">Sačuvaj</button>
                    <button type="button" className="adresa-cancel-button" onClick={cancelEditing}>
                      Otkaži
                    </button>
                  </div>
                  {editError && <p className="adrese-error">{editError}</p>}
                </form>
              ) : (
                <>
                  <div className="adresa-info">
                    <span className="adresa-street">{address.street}</span>
                    <span className="adresa-city">{address.city}</span>
                  </div>
                  <div className="adresa-actions">
                    <button type="button" className="adresa-edit-button" onClick={() => startEditing(address)}>
                      Izmeni
                    </button>
                    <button type="button" className="adresa-delete-button" onClick={() => handleDelete(address.id)}>
                      Obriši
                    </button>
                  </div>
                </>
              )}
            </div>
          ))}
        </div>

        <div className="adresa-card adresa-new">
          <h2>Dodaj novu adresu</h2>
          <form onSubmit={handleAddSubmit}>
            <input
              type="text"
              name="street"
              placeholder="Ulica i broj"
              value={formData.street}
              onChange={handleAddChange}
            />
            <input
              type="text"
              name="city"
              placeholder="Grad"
              value={formData.city}
              onChange={handleAddChange}
            />
            <button type="submit" className="adresa-add-button">Dodaj adresu</button>
            {formError && <p className="adrese-error">{formError}</p>}
          </form>
        </div>
      </div>
    </div>
  );
};


export default MojeAdrese;