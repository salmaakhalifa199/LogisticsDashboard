import React, { useState } from "react";
import { Link } from "react-router-dom"; // Import Link from React Router
import { FaUser, FaLock  } from "react-icons/fa";
import "./register.scss"
import axios from "axios";
import { useNavigate } from "react-router-dom";
const Register = () => {
  const [username, setUsername] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const [registrationMessage, setRegistrationMessage] = useState("");
  const navigate = useNavigate();
  const handleRegister = async (e) => {
    
    e.preventDefault();
    let req= {
      userName : username,
      email : email,
      password : password
    }
    try {
      let registerResponse = await axios.post('https://localhost:7233/api/Register' , req)
    console.log(registerResponse.data);
    let token = registerResponse.data.token
    localStorage.setItem('JWT' , JSON.stringify(token))
    // // window.open('http://localhost:3000/' , '_self')
    // navigate('/')
    setRegistrationMessage("Registration successful!"); 
   
    setUsername("");
    setEmail("");
    setPassword("");
  } catch (error) {
    console.error("Registration failed:", error);
    setRegistrationMessage("Registration failed. Please try again."); 
  }
  };


  return (
    <div className="register">
      <form onSubmit={handleRegister}>
        <h1>Register</h1>
        <div className="input-container">
        <FaUser  className="icon "/>
          <input
            type="text"
            placeholder="Username"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
            required
          />
        </div>
        <div className="input-container">
        <FaUser  className="icon "/>
          <input
            type="text"
            placeholder="Email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
          />
        </div>
        <div className="input-container">
        <FaLock className="icon " />
          <input
            type="password"
            placeholder="Password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />
        </div>
     {registrationMessage && <p className="registration-message">{registrationMessage}</p>}
        <button type="submit">Register</button>
        <Link to="/login">Already have an account? Login</Link>
      </form>
    </div>
  );
};

export default Register;
