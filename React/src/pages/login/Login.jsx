
import React,{ useState } from "react";
import { useNavigate } from "react-router-dom";
import { Link } from "react-router-dom";
import axios from "axios";
import { FaUser, FaLock  } from "react-icons/fa";
import "./login.scss"
const Login = () => {
  const [error, setError] = useState(false);
  const [username,setUsername]=useState("");
  const [password,setPassword]=useState("");
  const navigate = useNavigate();

  const handleLogin = async (e) => {
    
    e.preventDefault();
    let req= {
      userName : username,
      password : password
    }
    let loginResponse = await axios.post('https://localhost:7233/api/Login' , req)
    console.log(loginResponse.data);
    let token = loginResponse.data.token
    localStorage.setItem('JWT' , JSON.stringify(token))
    navigate('/')
  };

  return (
    <div className="login">
      <form onSubmit ={handleLogin}>

       <h1>Login</h1>
    

       <div className="input-container">
       <FaUser  className="icon "/>
        <input
         type="text"
         placeholder="Username" 
         value={username} 
         onChange={(e)=>setUsername(e.target.value)}
         required 
         className="form-control"/> 
        </div>


        <div className="input-container">
        <FaLock className="icon " />
        <input
         type="password" 
         placeholder="Password" 
         value={password}
         onChange={(e)=>setPassword(e.target.value)}
         required 
         className="form-control"/>
        </div>

        
        <div className="checkbox-container">
        <input type="checkbox" className="checkbox-input"/>
        <span className="checkbox-label">Remember me</span>
        </div>


        <div className="checkbox-forget-container">
        </div>
        <Link to="/forgot-password">Forgot password?</Link> 
        <button type="submit">Login</button>
        <Link to="/register">Don't have an account? Register</Link>
        {error && <span>Wrong email or password!</span>}
      </form>
    </div>

  )
}

export default Login;