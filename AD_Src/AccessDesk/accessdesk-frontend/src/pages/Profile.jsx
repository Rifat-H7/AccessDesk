import React, { useEffect, useState } from "react";
import api from "../api";
import { useNavigate } from "react-router-dom";

export default function Profile() {
  const [profile, setProfile] = useState(null);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchProfile = async () => {
      try {
        const token = localStorage.getItem("token");
        const res = await api.get("/profile", {
          headers: { Authorization: `Bearer ${token}` }
        });
        setProfile(res.data.data);
      } catch (err) {
        navigate("/login");
      }
    };
    fetchProfile();
  }, [navigate]);

  if (!profile) return <p>Loading...</p>;

  return (
    <div style={{ maxWidth: 400, margin: "auto" }}>
      <h2>Profile</h2>
      <p><b>ID:</b> {profile.id}</p>
      <p><b>Username:</b> {profile.username}</p>
      <p><b>Email:</b> {profile.email}</p>
      <p><b>Full Name:</b> {profile.fullName}</p>
      <p><b>Roles:</b> {profile.roles.join(", ")}</p>
      <button onClick={() => { localStorage.removeItem("token"); navigate("/login"); }}>
        Logout
      </button>
    </div>
  );
}
