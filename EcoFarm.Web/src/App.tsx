import React from 'react';
import './App.css';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Home from './pages/Home';
import RouteManager from './pages/RouteManager';

function App() {
  return (
    <>
    <RouteManager />
    </>
  );
}

export default App;
