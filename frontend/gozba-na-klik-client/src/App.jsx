import { BrowserRouter, Routes , Route } from "react-router-dom";
import WelcomePage from "./components/WelcomePage";
import Register from "./components/Register";
import Login from "./components/Login";
import AdminUsers from "./components/AdminUsers";
import UserHomePage from "./components/UserHomePage";
import MojProfil from "./components/MojProfil";
import RestaurantsOverview from "./components/RestaurantsOverview";
import CreateRestaurant from "./components/CreateRestaurant";
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

        <Route path="/moj-profil" element={<MojProfil />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
