import React, { useState } from "react";
import api from "../api";
import { useNavigate } from "react-router-dom";

export default function Login() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();

  const handleLogin = async (e) => {
    e.preventDefault();
    setError("");
    setLoading(true);

    try {
      console.log('Attempting login with:', { username, password: '***' });
      
      const res = await api.post("/login", { 
        UsernameOrEmail: username.trim(), 
        Password: password 
      });
      
      console.log('Login successful:', res.data);
      localStorage.setItem("token", res.data.token);
      navigate("/profile");
    } catch (err) {
      console.error('Login error:', err);
      
      if (err.code === 'NETWORK_ERROR' || !err.response) {
        setError("Network error: Unable to connect to server. Please check if the server is running.");
      } else if (err.response?.status === 404) {
        setError("Login endpoint not found. Please check the server configuration.");
      } else if (err.response?.status === 405) {
        setError("Method not allowed. Please check the server endpoint configuration.");
      } else {
        setError(err.response?.data?.message || "Login failed");
      }
    } finally {
      setLoading(false);
    }
  };

  return (
    <div style={{ maxWidth: 300, margin: "auto" }}>
      <h2>Login</h2>
      {error && <p style={{ color: "red" }}>{error}</p>}
      <form onSubmit={handleLogin}>
        <input
          type="text"
          placeholder="Username"
          value={username}
          onChange={(e) => setUsername(e.target.value)}
          required
          disabled={loading}
        /><br /><br />
        <input
          type="password"
          placeholder="Password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          required
          disabled={loading}
        /><br /><br />
        <button type="submit" disabled={loading}>
          {loading ? "Logging in..." : "Login"}
        </button>
      </form>
    </div>
  );
}