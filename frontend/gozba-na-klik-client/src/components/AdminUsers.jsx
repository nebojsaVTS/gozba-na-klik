import "./AdminUsers.scss";
import { useState, useEffect } from "react";
import { API_BASE_URL } from "../api";

function AdminUsers() {

  const [showRegisterForm, setShowRegisterForm] = useState(false);
  const [users, setUsers] = useState([]);
  const [loadError, setLoadError] = useState("");
  const [formError, setFormError] = useState("");

   const [formData, setFormData] = useState({
    username: "",
    password: "",
    email: "",
    role: ""
  });

  const loadUsers = () => {
    fetch(`${API_BASE_URL}/admin/users`)
      .then((response) => {
        if (!response.ok) {
          throw new Error("Greška pri učitavanju korisnika.");
        }
        return response.json();
      })
      .then((data) => {
        setUsers(data);
        setLoadError("");
      })
      .catch(() => setLoadError("Greška pri učitavanju korisnika."));
  };

  useEffect(() => {
    loadUsers();
  }, []);

  const handleChange = (e) => {
    const { name, value } = e.target;

    setFormData({
      ...formData,
      [name]: value
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (
      !formData.username ||
      !formData.password ||
      !formData.email ||
      !formData.role
    ) {
      setFormError("Sva polja su obavezna");
      return;
    }

    try {
      const response = await fetch(`${API_BASE_URL}/admin/users`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(formData),
      });

      const data = await response.json();

      if (!response.ok) {
        const errorMessage =
          data.message ||
          Object.values(data.errors ?? {}).flat().join(" ") ||
          "Registracija korisnika nije uspela.";
        setFormError(errorMessage);
        return;
      }

      setFormError("");
      setFormData({
        username: "",
        password: "",
        email: "",
        role: ""
      });

      setShowRegisterForm(false);
      loadUsers();
    } catch {
      setFormError("Greška prilikom povezivanja sa serverom.");
    }
  };

  return (
    <div className="admin-users-container">
      <h2>Pregled registrovanih korisnika</h2>

      <button onClick={() => setShowRegisterForm(true)}>
          Registruj novog korisnika
      </button>

      {showRegisterForm && (
  <form onSubmit={handleSubmit}>
    <input
      type="text"
      name="username"
      placeholder="Korisničko ime"
      value={formData.username}
      onChange={handleChange}
    />
    <input
      type="password"
      name="password"
      placeholder="Password"
      value={formData.password}
      onChange={handleChange}
    />
    <input
      type="email"
      name="email"
      placeholder="Email"
      value={formData.email}
      onChange={handleChange}
    />

    <select name="role" value={formData.role} onChange={handleChange}>
      <option value="">Izaberi ulogu</option>
      <option value="Kurir">Kurir</option>
      <option value="Vlasnik restorana">Vlasnik restorana</option>
    </select>

    <button type="submit">Registruj</button>

    {formError && <p className="error">{formError}</p>}
  </form>
)}

      {loadError && <p className="error">{loadError}</p>}

      <table className="users-table">
        <thead>
          <tr>
            <th>Username</th>
            <th>Email</th>
            <th>Role</th>
          </tr>
        </thead>

        <tbody>
          {users.map((user) => (
            <tr key={user.id}>
              <td>{user.username}</td>
              <td>{user.email}</td>
              <td>{user.role}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default AdminUsers;