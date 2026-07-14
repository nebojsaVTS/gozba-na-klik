import "./AdminUsers.scss";
import { useState } from "react";

function AdminUsers() {

  const [showRegisterForm, setShowRegisterForm] = useState(false);

   const [formData, setFormData] = useState({
    username: "",
    password: "",
    email: "",
    role: ""
  });

  const users = [
    {
      username: "admin1",
      email: "admin@test.com",
      role: "Administrator"
    },
    {
      username: "marko",
      email: "marko@test.com",
      role: "Kupac"
    },
    {
      username: "kurir1",
      email: "kurir@test.com",
      role: "Kurir"
    },
    {
      username: "vlasnik1",
      email: "vlasnik@test.com",
      role: "Vlasnik restorana"
    }
  ];
  const handleChange = (e) => {
    const { name, value } = e.target;

    setFormData({
      ...formData,
      [name]: value
    });
  };

  const handleSubmit = (e) => {
    e.preventDefault();

    if (
      !formData.username ||
      !formData.password ||
      !formData.email ||
      !formData.role
    ) {
      alert("Sva polja su obavezna");
      return;
    }

    console.log("Novi korisnik:", formData);

    // ovde kasnije ide API call

    setFormData({
      username: "",
      password: "",
      email: "",
      role: ""
    });

    setShowRegisterForm(false);
  };
  
  return (
    <div className="admin-users-container">
      <h2>Pregled registrovanih korisnika</h2>

      <button onClick={() => setShowRegisterForm(true)}>
          Registruj novog korisnika
      </button>

      {showRegisterForm && (
  <form>
    <input type="text" placeholder="Korisničko ime" />
    <input type="password" placeholder="Password" />
    <input type="email" placeholder="Email" />

    <select>
      <option value="">Izaberi ulogu</option>
      <option value="Courier">Kurir</option>
      <option value="RestaurantOwner">Vlasnik restorana</option>
    </select>

    <button type="submit">Registruj</button>
  </form>
)}

      <table className="users-table">
        <thead>
          <tr>
            <th>Username</th>
            <th>Email</th>
            <th>Role</th>
          </tr>
        </thead>

        <tbody>
          {users.map((user, index) => (
            <tr key={index}>
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