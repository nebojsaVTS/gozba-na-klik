import { BrowserRouter, Routes, Route } from "react-router-dom";
import WelcomePage from "./components/WelcomePage";
import Register from "./components/Register";
import Login from "./components/Login";
import AdminUsers from "./components/AdminUsers";
import UserHomePage from "./components/UserHomePage";
import MojProfil from "./components/MojProfil";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<WelcomePage />} />
        <Route path="/register" element={<Register />} />
        <Route path="/login" element={<Login />} />

        <Route
          path="/kupac"
          element={<UserHomePage title="Početna stranica kupca" />}
        />
        <Route
          path="/vlasnik"
          element={<UserHomePage title="Pocetna stranica za vlasnika" />}
        />
        <Route
          path="/administrator"
          element={<UserHomePage title="Pocetna stranica za administratora" />}
        />
        <Route
          path="/kurir"
          element={<UserHomePage title="Pocetna stranica za kurira" />}
        />

        <Route path="/admin/users" element={<AdminUsers />} />
        <Route path="/moj-profil" element={<MojProfil />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;