import { BrowserRouter as Router, Routes, Route, Link } from "react-router-dom";
import Login from "./pages/Login";
import Register from "./pages/Register";
import Profile from "./pages/Profile";

export default function App() {
  return (
    <Router>
      <nav style={{ marginBottom: 20 }}>
        <Link to="/login" style={{ marginRight: 10 }}>Login</Link>
        <Link to="/register" style={{ marginRight: 10 }}>Register</Link>
        <Link to="/profile">Profile</Link>
      </nav>
      <Routes>
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route path="/profile" element={<Profile />} />
      </Routes>
    </Router>
  );
}
