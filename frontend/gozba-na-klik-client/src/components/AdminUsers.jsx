import "./AdminUsers.scss";

function AdminUsers() {
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

  return (
    <div className="admin-users-container">
      <h2>Pregled registrovanih korisnika</h2>

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