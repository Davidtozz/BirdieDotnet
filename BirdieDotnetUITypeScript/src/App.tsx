import './App.css';
import { Route, Routes} from 'react-router-dom';
import ChatInterfacePage  from './pages/ChatInterfacePage';
import LandingPage from './pages/LandingPage'; 
import LoginPage from './pages/LoginPage';
import RegisterPage from './pages/RegisterPage';

function App() {
  return (
    <Routes>
      <Route path="/" Component={LandingPage} />
      <Route path="/register" Component={RegisterPage} />
      <Route path="/login" Component={LoginPage} />
      <Route path="/chat" Component={ChatInterfacePage} />
    </Routes>
  );
}

export default App;