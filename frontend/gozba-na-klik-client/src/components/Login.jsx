import "./Login.scss";
import React, { useState } from "react";

const Login = () => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [message, setMessage] = useState("");
  const [isError, setIsError] = useState(false);

  const handleSubmit = (e) => {
    e.preventDefault();

    if (username === "" || password === "") {
      setMessage("Sva polja moraju biti popunjena.");
      setIsError(true);
      return;
    }

    setMessage("Forma je uspešno popunjena.");
    setIsError(false);
    setUsername("");
    setPassword("");
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

        {message && (
          <p
            className={
              isError ? "login-message error" : "login-message success"
            }
          >
            {message}
          </p>
        )}
      </form>
    </div>
  );
};

export default Login;
