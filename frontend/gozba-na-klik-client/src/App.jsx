import { BrowserRouter, Routes, Route, UNSAFE_DataRouterStateContext } from "react-router-dom";
import WelcomePage from "./components/WelcomePage";
import Register from "./components/Register";
import Login from "./components/Login";

function App() {
  return (
    <BrowserRouter>
     <Routes>
      
      <Route path="/" element={<WelcomePage />} />
      <Route path="/register" element={<Register />} />
      <Route path="/login" element={<Login />} />
      <Route path="/kupac" element={<div>Pocetna stranica za kupca</div>} />
      <Route path="/vlasnik" element={<div>Pocetna stranica za vlasnika</div>} />
      <Route path="/administrator" element={<div>Pocetna stranica za administratora</div>} />
      <Route path="/kurir" element={<div>Pocetna stranica za kurira</div>} />
     
     </Routes>
    </BrowserRouter>
  );
}

export default App;