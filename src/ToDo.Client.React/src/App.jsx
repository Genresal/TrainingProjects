import { useState } from 'react'
import './App.css'
import Home from './pages/Home';
import Table from './pages/Table';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import React from 'react';
import Navigation from './components/Navigation';

function App() {
  return (
    <BrowserRouter>
    <Navigation />  {/* Include the Navigation component */}
        <Routes>
          <Route path="/" element={<Home/>} />
          <Route path="/table" element={<Table/>} />
        </Routes>
    </BrowserRouter>
  )
}

export default App
