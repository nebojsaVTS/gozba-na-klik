const mockOrders = [
    {
        id: 25,
        status: "Pending",
        items: [
            {
                id: 1,
                name: "Cheeseburger",
                quantity: 2,
                price: 650,
                toppings: ["Luk", "Paradajz", "Ketchup", "Majonez"],
                variant: "",
                note: "Bez krastavca",
            },
            {
                id: 2,
                name: "Pljeskavica",
                quantity: 1,
                price: 550,
                toppings: ["Luk", "Urnebes", "Kupus"],
                variant: "",
                note: "",
            },
            {
                id: 3,
                name: "Coca-Cola 0.33l",
                quantity: 2,
                price: 150,
                toppings: [],
                variant: "",
                note: "",
            },
        ],
        generalNote: "",
        total: 2150,
    },
    {
        id: 24,
        status: "InPreparation",
        items: [
            {
                id: 1,
                name: "Pizza Margarita",
                quantity: 1,
                price: 900,
                toppings: ["Dodatni sir"],
                variant: "Velika",
                note: "",
            },
        ],
        generalNote: "Pozvati kada stignete.",
        total: 900,
    },
    {
        id: 23,
        status: "Ready",
        items: [
            {
                id: 1,
                name: "Burek sa mesom",
                quantity: 2,
                price: 300,
                toppings: [],
                variant: "",
                note: "",
            },
        ],
        generalNote: "",
        total: 600,
    },
];

// Privremeno vracamo mock podatke dok ne postoji pravi Order backend (dolazi u PR5)
export const getOrders = async () => {
    return [...mockOrders];
};

export const acceptOrder = async (orderId) => {
    const order = mockOrders.find((o) => o.id === orderId);
    if (order) {
        order.status = "Accepted";
    }
    return [...mockOrders];
};

export const rejectOrder = async (orderId) => {
    const order = mockOrders.find((o) => o.id === orderId);
    if (order) {
        order.status = "Rejected";
    }
    return [...mockOrders];
};

export const updateOrderStatus = async (orderId, newStatus) => {
    const order = mockOrders.find((o) => o.id === orderId);
    if (order) {
        order.status = newStatus;
    }
    return [...mockOrders];
};
