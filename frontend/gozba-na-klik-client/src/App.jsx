import { BrowserRouter, Routes, Route } from "react-router-dom";

import WelcomePage from "./components/WelcomePage";
import Register from "./components/Register";
import Login from "./components/Login";
import AdminUsers from "./components/AdminUsers";
import UserHomePage from "./components/UserHomePage";
import MojeAdrese from "./components/MojeAdrese";
import MojProfil from "./components/MojProfil";
import RestaurantsOverview from "./components/RestaurantsOverview";
import CreateRestaurant from "./components/CreateRestaurant";
import CustomerRestaurants from "./components/CustomerRestaurants";
import RestaurantMenu from "./components/RestaurantMenu";
import CustomerHomePage from "./components/CustomerHomePage";
import RestaurantOperatorHomePage from "./components/RestaurantOperatorHomePage";
import RestaurantOrders from "./components/RestaurantOrders";
import CartPage from "./components/CartPage";
import CheckoutPage from "./components/CheckoutPage";
import CustomerOrders from "./components/CustomerOrders";
import RoleRoute from "./components/RoleRoute";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        {/* Javne rute */}
        <Route path="/" element={<WelcomePage />} />
        <Route path="/register" element={<Register />} />
        <Route path="/login" element={<Login />} />

        {/* KUPAC */}
        <Route
          path="/kupac"
          element={
            <RoleRoute allowedRoles={["Kupac"]}>
              <CustomerHomePage />
            </RoleRoute>
          }
        />

        <Route
          path="/restaurants"
          element={
            <RoleRoute allowedRoles={["Kupac"]}>
              <CustomerRestaurants />
            </RoleRoute>
          }
        />

        <Route
          path="/restaurants/:restaurantId"
          element={
            <RoleRoute allowedRoles={["Kupac"]}>
              <RestaurantMenu />
            </RoleRoute>
          }
        />

        <Route
          path="/cart"
          element={
            <RoleRoute allowedRoles={["Kupac"]}>
              <CartPage />
            </RoleRoute>
          }
        />

        <Route
          path="/checkout"
          element={
            <RoleRoute allowedRoles={["Kupac"]}>
              <CheckoutPage />
            </RoleRoute>
          }
        />

        <Route
          path="/moje-porudzbine"
          element={
            <RoleRoute allowedRoles={["Kupac"]}>
              <CustomerOrders />
            </RoleRoute>
          }
        />

        <Route
          path="/moje-adrese"
          element={
            <RoleRoute allowedRoles={["Kupac"]}>
              <MojeAdrese />
            </RoleRoute>
          }
        />

        {/* VLASNIK RESTORANA */}
        <Route
          path="/vlasnik"
          element={
            <RoleRoute allowedRoles={["Vlasnik restorana"]}>
              <UserHomePage title="Pocetna stranica za vlasnika" />
            </RoleRoute>
          }
        />

        {/* ADMINISTRATOR */}
        <Route
          path="/administrator"
          element={
            <RoleRoute allowedRoles={["Administrator"]}>
              <UserHomePage title="Pocetna stranica za administratora" />
            </RoleRoute>
          }
        />

        <Route
          path="/admin/users"
          element={
            <RoleRoute allowedRoles={["Administrator"]}>
              <AdminUsers />
            </RoleRoute>
          }
        />

        <Route
          path="/admin/restaurants"
          element={
            <RoleRoute allowedRoles={["Administrator"]}>
              <RestaurantsOverview />
            </RoleRoute>
          }
        />

        <Route
          path="/admin/restaurants/new"
          element={
            <RoleRoute allowedRoles={["Administrator"]}>
              <CreateRestaurant />
            </RoleRoute>
          }
        />

        {/* KURIR */}
        <Route
          path="/kurir"
          element={
            <RoleRoute allowedRoles={["Kurir"]}>
              <UserHomePage title="Pocetna stranica za kurira" />
            </RoleRoute>
          }
        />

        {/* RESTAURANT OPERATOR */}
        <Route
          path="/restaurant-operator"
          element={
            <RoleRoute allowedRoles={["RestaurantOperator"]}>
              <RestaurantOperatorHomePage />
            </RoleRoute>
          }
        />

        <Route
          path="/restaurant-operator/orders"
          element={
            <RoleRoute allowedRoles={["RestaurantOperator"]}>
              <RestaurantOrders />
            </RoleRoute>
          }
        />

        {/* PROFIL - svi prijavljeni korisnici */}
        <Route
          path="/moj-profil"
          element={
            <RoleRoute
              allowedRoles={[
                "Kupac",
                "RestaurantOperator",
                "Administrator",
                "Vlasnik restorana",
                "Kurir",
              ]}
            >
              <MojProfil />
            </RoleRoute>
          }
        />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
