import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { getOrdersByUser } from "../services/orderService";
import "./CustomerOrders.scss";

const CustomerOrders = () => {
  const navigate = useNavigate();

  const [orders, setOrders] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");

  useEffect(() => {
    const loadOrders = async () => {
      try {
        const storedUser = localStorage.getItem("gozbaUser");

        if (!storedUser) {
          navigate("/login");
          return;
        }

        const user = JSON.parse(storedUser);

        const data = await getOrdersByUser(user.id);
        setOrders(data);
      } catch (err) {
        console.error(err);
        setError("Došlo je do greške prilikom učitavanja porudžbina.");
      } finally {
        setLoading(false);
      }
    };

    loadOrders();
  }, [navigate]);

  const getStatusLabel = (status) => {
    switch (status) {
      case "Pending":
        return "Na čekanju";

      case "Accepted":
        return "Prihvaćena";

      case "Rejected":
        return "Odbijena";

      case "InPreparation":
        return "U pripremi";

      case "Ready":
        return "Spremna";

      case "Completed":
        return "Završena";

      default:
        return status;
    }
  };

  const formatDate = (date) => {
    return new Date(date).toLocaleString("sr-RS");
  };

  if (loading) {
    return (
      <div className="customer-orders-page">
        <div className="customer-orders-container">
          <p>Učitavanje porudžbina...</p>
        </div>
      </div>
    );
  }

  return (
    <div className="customer-orders-page">
      <div className="customer-orders-container">
        <div className="customer-orders-header">
          <button
            type="button"
            className="customer-orders-back-button"
            onClick={() => navigate("/kupac")}
          >
            Nazad
          </button>

          <h1>Moje porudžbine</h1>
        </div>

        {error && <p className="customer-orders-error">{error}</p>}

        {!error && orders.length === 0 && (
          <div className="customer-orders-empty">
            <h2>Još nemaš porudžbina</h2>
            <p>Kada napraviš prvu porudžbinu, pojaviće se ovde.</p>

            <button type="button" onClick={() => navigate("/restaurants")}>
              Pogledaj restorane
            </button>
          </div>
        )}

        {!error && orders.length > 0 && (
          <div className="customer-orders-list">
            {orders.map((order) => (
              <div className="customer-order-card" key={order.id}>
                <div className="customer-order-top">
                  <div>
                    <h2>{order.restaurantName}</h2>
                    <p>Porudžbina #{order.id}</p>
                  </div>

                  <span
                    className={`customer-order-status status-${order.status.toLowerCase()}`}
                  >
                    {getStatusLabel(order.status)}
                  </span>
                </div>

                <div className="customer-order-info">
                  <p>
                    <strong>Datum:</strong> {formatDate(order.createdAt)}
                  </p>

                  <p>
                    <strong>Adresa:</strong> {order.deliveryStreet},{" "}
                    {order.deliveryCity}
                  </p>

                  {order.deliveryPhoneNumber && (
                    <p>
                      <strong>Telefon:</strong> {order.deliveryPhoneNumber}
                    </p>
                  )}
                </div>

                <div className="customer-order-items">
                  <h3>Stavke</h3>

                  {order.items.map((item) => (
                    <div className="customer-order-item" key={item.id}>
                      <div className="customer-order-item-main">
                        <span>
                          {item.quantity}x {item.itemName}
                        </span>

                        <span>{item.unitPrice * item.quantity} RSD</span>
                      </div>

                      {item.selectedSides?.length > 0 && (
                        <p>
                          <strong>Prilozi:</strong>{" "}
                          {item.selectedSides.join(", ")}
                        </p>
                      )}

                      {item.note && (
                        <p>
                          <strong>Napomena:</strong> {item.note}
                        </p>
                      )}
                    </div>
                  ))}
                </div>

                <div className="customer-order-total">
                  <span>Ukupno:</span>
                  <strong>{order.totalPrice} RSD</strong>
                </div>
              </div>
            ))}
          </div>
        )}
      </div>
    </div>
  );
};

export default CustomerOrders;
