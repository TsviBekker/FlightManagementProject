import React, { useState, useEffect } from "react";
import { Routes, Router, Route } from "react-router-dom";
import { NavBar } from "./Components/NavBar/NavBar";
import { Home } from "./Views/Home";
import { Stations } from "./Views/Stations";
import { ScheduledFlights } from "./Views/ScheduledFlights";
import "./App.css";
import { FlightsHistory } from "./Views/FlightsHistory";
import { StationsProvider } from "./Contexts/StationsContext";

export const App = () => {
  return (
    <>
      <StationsProvider>
        <NavBar />
        <div className="center">
          <Routes>
            <Route path="stations" element={<Stations />}></Route>
            <Route path="scheduled" element={<ScheduledFlights />}></Route>
            <Route path="flight-history" element={<FlightsHistory />}></Route>
          </Routes>
        </div>
      </StationsProvider>
    </>
  );
};
