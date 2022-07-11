import React from "react";
import "./NavBar.css";
import { NavLink } from "react-router-dom";

export const NavBar = () => {
  return (
    <nav className="nav">
      <NavLink to="/stations" className="site-title">
        Control Center
      </NavLink>
      <ul>
        <CustomLink to="/stations">Stations Overview</CustomLink>
        <CustomLink to="/scheduled">Scheduled Flights</CustomLink>
        <CustomLink to="/flight-history">Flight History</CustomLink>
      </ul>
    </nav>
  );
};

const CustomLink = ({ to, children, ...props }) => {
  const path = window.location.pathname;
  return (
    <li className={path === to ? "active" : ""}>
      <NavLink to={to} {...props}>
        {children}
      </NavLink>
    </li>
  );
};
