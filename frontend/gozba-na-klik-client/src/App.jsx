import { BrowserRouter, Routes, Route } from "react-router-dom";
import WelcomePage from "./components/WelcomePage";
import Register from "./components/Register";
import Login from "./components/Login";
import AdminUsers from "./components/AdminUsers";
import UserHomePage from "./components/UserHomePage";
import RestaurantsOverview from "./components/RestaurantsOverview";
import CreateRestaurant from "./components/CreateRestaurant";

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

        <Route 
          path="/admin/users" 
          element={<AdminUsers />}
           />

        <Route
          path="/admin/restaurants"
          element={<RestaurantsOverview />}
          />

        <Route
          path="/admin/restaurants/new"
          element={<CreateRestaurant />} />

      </Routes>
    </BrowserRouter>
  );
}

export default App;