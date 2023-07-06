import './App.css';

import { Route, Routes} from 'react-router-dom';
import { createContext } from 'react';

import ChatInterfacePage  from './pages/ChatInterfacePage';
import LandingPage from './pages/LandingPage'; 
import LoginPage from './pages/LoginPage';
import RegisterPage from './pages/RegisterPage';

import ApiService from 'services/ApiService';

export const ApiServiceContext = createContext<ApiService | null>(null);

function App() {

  const apiService: ApiService = ApiService.instance;


  return (
    <ApiServiceContext.Provider value={apiService}>
      <Routes>
        <Route path="/" Component={LandingPage} />
        <Route path="/register" Component={RegisterPage} />
        <Route path="/login" Component={LoginPage} />
        <Route path="/chat" Component={ChatInterfacePage} />
      </Routes>
    </ApiServiceContext.Provider>
  );
}

export default App;