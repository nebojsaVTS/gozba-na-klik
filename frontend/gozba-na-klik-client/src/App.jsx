import { BrowserRouter, Routes, Route } from "react-router-dom";
import WelcomePage from "./components/WelcomePage";
import Register from "./components/Register";
import Login from "./components/Login";
import AdminUsers from "./components/AdminUsers";
import UserHomePage from "./components/UserHomePage";
import CustomerRestaurants from "./components/CustomerRestaurants";
import RestaurantMenu from "./components/RestaurantMenu";
import CustomerHomePage from "./components/CustomerHomePage";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<WelcomePage />} />
        <Route path="/register" element={<Register />} />
        <Route path="/login" element={<Login />} />

        <Route path="/kupac" element={<CustomerHomePage />} />
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
        <Route path="/restaurants" element={<CustomerRestaurants />} />
        <Route path="/restaurants/:restaurantId" element={<RestaurantMenu />} />

        <Route path="/admin/users" element={<AdminUsers />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
