import React, { useEffect } from 'react';

import './App.css';
import { CustomersList, deleteCustomerById, getCustomerById } from './api/api';
import LoginPage from './screens/loginPage/LoginPage';
import { Provider } from 'react-redux';
import store from './redux/store';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import HomePage from './screens/homePage/HomePage';


const App = () => {




  return (
    <Provider store={store}>
      <Router>
        <Routes>
          <Route path="/" element={<LoginPage />} />
          <Route path="/home" element={<HomePage />} />
        </Routes>
      </Router>
    </Provider>
  );
}

export default App;
