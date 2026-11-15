import { Navigate } from "react-router-dom";

const RoleRoute = ({ allowedRoles, children }) => {
  const storedUser = localStorage.getItem("gozbaUser");

  if (!storedUser) {
    return <Navigate to="/login" replace />;
  }

  const user = JSON.parse(storedUser);

  if (!allowedRoles.includes(user.role)) {
    switch (user.role) {
      case "Kupac":
        return <Navigate to="/kupac" replace />;

      case "RestaurantOperator":
        return <Navigate to="/restaurant-operator" replace />;

      case "Administrator":
        return <Navigate to="/administrator" replace />;

      case "Vlasnik restorana":
        return <Navigate to="/vlasnik" replace />;

      case "Kurir":
        return <Navigate to="/kurir" replace />;

      default:
        localStorage.removeItem("gozbaUser");
        return <Navigate to="/login" replace />;
    }
  }

  return children;
};

export default RoleRoute;
