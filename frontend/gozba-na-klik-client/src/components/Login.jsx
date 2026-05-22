import "./Login.scss";
import { useState } from "react";
import { useNavigate } from "react-router-dom";

const Login = () => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [message, setMessage] = useState("");
  const [isError, setIsError] = useState(false);
  const navigate = useNavigate();

  const handleSubmit = (e) => {
    e.preventDefault();

    if (username === "" || password === "") {
      setMessage("Sva polja moraju biti popunjena.");
      setIsError(true);
      return;
    }

    // nema backedn koji vraca ulogu korisnika
    // stavljam const uloga= "KUPAC" za fiksnu vrednost da testiram redirect
    //ovo sve menja backend kad napravimo 

    const uloga = "KUPAC";

    if (uloga === "KUPAC") navigate("/kupac");
    else if (uloga === "VLASNIK RESTORANA") navigate("/vlasnik");
    else if (uloga === "ADMINISTRATOR") navigate("/administrator");
    else if (uloga === "KURIR") navigate("/kurir");
  };

  return (
    <div className="login-container">
      <h2>Prijava</h2>

      <form className="login-form" onSubmit={handleSubmit}>
        <input
            type="text"
            placeholder="Username"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
          />

        <input
            type="password"
            placeholder="Password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
          <button type="submit">Prijavi se</button>

          {message && <p className={`login-message ${isError ? "error" : "success"}`}>{message}</p>}
      </form>
     </div>   
  
  );
};

export default Login;