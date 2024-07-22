import React, { useEffect } from 'react';

import './App.css';
import { CustomersList, deleteCustomerById, getCustomerById } from './api/api';
import LoginPage from './screens/loginPage/LoginPage';

function App() {



  useEffect(() => {

    // getCustomerById(1)
    // deleteCustomerById(5)
    CustomersList(2)
  }, [])


  return (
    <div className="App">
      <LoginPage />
    </div>
  );
}

export default App;
