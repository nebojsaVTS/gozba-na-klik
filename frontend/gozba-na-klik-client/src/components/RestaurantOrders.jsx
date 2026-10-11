import "./RestaurantOrders.scss";
import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import {
  getOrders,
  acceptOrder,
  rejectOrder,
  updateOrderStatus,
} from "../services/restaurantOrderService";

const statusLabels = {
  Pending: "Na čekanju",
  Accepted: "Prihvaćena",
  Rejected: "Odbijena",
  InPreparation: "U pripremi",
  Ready: "Spremna",
  Completed: "Završena",
};

function OrderDetails({ order }) {
  return (
    <>
      {order.items.map((item) => (
        <div className="order-item" key={item.id}>
          <p className="order-item-title">
            {item.quantity}x {item.name}
            {item.variant && ` (${item.variant})`}
          </p>
          {item.toppings.length > 0 && (
            <p className="order-item-detail">
              Prilozi: {item.toppings.join(", ")}
            </p>
          )}
          {item.note && (
            <p className="order-item-detail">Napomena: {item.note}</p>
          )}
          <p className="order-item-price">Cena: {item.price} RSD</p>
        </div>
      ))}

      {order.generalNote && (
        <p className="order-general-note">
          Napomena za porudžbinu: {order.generalNote}
        </p>
      )}

      <p className="order-total">Ukupno: {order.total} RSD</p>
    </>
  );
}

function RestaurantOrders() {
  const navigate = useNavigate();
  const [user, setUser] = useState(null);
  const [orders, setOrders] = useState([]);

  useEffect(() => {
    const stored = localStorage.getItem("gozbaUser");
    if (!stored) {
      navigate("/login");
      return;
    }

    const parsedUser = JSON.parse(stored);
    if (parsedUser.role !== "RestaurantOperator") {
      navigate("/login");
      return;
    }

    setUser(parsedUser);
  }, [navigate]);

  useEffect(() => {
    if (user) {
      getOrders().then(setOrders);
    }
  }, [user]);

  const handleAccept = async (orderId) => {
    const updated = await acceptOrder(orderId);
    setOrders(updated);
  };

  const handleReject = async (orderId) => {
    const updated = await rejectOrder(orderId);
    setOrders(updated);
  };

  const handleStatusChange = async (orderId, newStatus) => {
    const updated = await updateOrderStatus(orderId, newStatus);
    setOrders(updated);
  };

  if (!user) {
    return null;
  }

  const newOrders = orders.filter((order) => order.status === "Pending");
  const activeOrders = orders.filter((order) =>
    ["Accepted", "InPreparation", "Ready"].includes(order.status)
  );

  return (
    <div className="orders-container">
      <div className="orders-content">
        <button
          type="button"
          className="orders-back-button"
          onClick={() => navigate("/restaurant-operator")}
        >
          ← Nazad
        </button>

        <h1>Porudžbine</h1>

        <section>
          <h2>Nove porudžbine</h2>

          {newOrders.length === 0 && (
            <p className="orders-empty">Trenutno nema novih porudžbina.</p>
          )}

          {newOrders.map((order) => (
            <div className="order-card" key={order.id}>
              <h3>Porudžbina #{order.id}</h3>

              <OrderDetails order={order} />

              <div className="order-actions">
                <button
                  type="button"
                  className="order-accept-button"
                  onClick={() => handleAccept(order.id)}
                >
                  Prihvati
                </button>
                <button
                  type="button"
                  className="order-reject-button"
                  onClick={() => handleReject(order.id)}
                >
                  Odbij
                </button>
              </div>
            </div>
          ))}
        </section>

        <section>
          <h2>Aktivne porudžbine</h2>

          {activeOrders.length === 0 && (
            <p className="orders-empty">Trenutno nema aktivnih porudžbina.</p>
          )}

          {activeOrders.map((order) => (
            <div className="order-card" key={order.id}>
              <h3>
                Porudžbina #{order.id} — {statusLabels[order.status]}
              </h3>

              <OrderDetails order={order} />

              {order.status === "Accepted" && (
                <button
                  type="button"
                  className="order-status-button"
                  onClick={() => handleStatusChange(order.id, "InPreparation")}
                >
                  U pripremu
                </button>
              )}

              {order.status === "InPreparation" && (
                <button
                  type="button"
                  className="order-status-button"
                  onClick={() => handleStatusChange(order.id, "Ready")}
                >
                  Spremno
                </button>
              )}
            </div>
          ))}
        </section>
      </div>
    </div>
  );
}

export default RestaurantOrders;
