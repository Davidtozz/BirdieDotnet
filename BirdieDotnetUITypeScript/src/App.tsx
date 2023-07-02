import './App.css';
import { BrowserRouter as Router, Route } from 'react-router-dom';
import { ChatInterfacePage, LandingPage, LoginPage, RegisterPage } from '@pages/index';

function App() {
  return (
    <div className="App">

      <Router>
        <Route path="/" Component={LandingPage} />
        <Route path="/register" Component={RegisterPage} />
        <Route path="/login" Component={LoginPage} />
        <Route path="/chat" Component={ChatInterfacePage} />
      </Router>
      
    </div>
  );
}

export default App;