import React, { useEffect, useState } from "react";
import { BackendApi } from "../Constants/Constants";

export const ScheduledFlights = () => {
  const [flights, setFlights] = useState([]);

  useEffect(() => {
    fetch(`${BackendApi}BusinessLogic/scheduled-flights`)
      .then((res) => res.json())
      .then((data) => setFlights(data));
  }, []);

  return (
    <>
      <table>
        <thead>
          <tr>
            <th>Flight No.</th>
            <th>Flight Code</th>
            <th>From Station</th>
            <th>To Station</th>
            <th>Status</th>
          </tr>
        </thead>
        <tbody>
          {flights.map((sf) => {
            return (
              <tr>
                <td>{sf.flight.flightId}</td>
                <td>{sf.flight.code}</td>
                <td>{sf.from}</td>
                <td>{sf.to}</td>
                <td>{sf.status}</td>
              </tr>
            );
          })}
        </tbody>
      </table>
    </>
  );
};
