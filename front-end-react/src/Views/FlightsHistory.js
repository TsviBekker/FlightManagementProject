import React, { useEffect, useState } from "react";
import { BackendApi } from "../Constants/Constants";
import { formatDateTime } from "../Utils/Utils";

export const FlightsHistory = () => {
  const [history, setHistory] = useState({});

  useEffect(() => {
    fetch(`${BackendApi}BusinessLogic/flights-history`)
      .then((res) => res.json())
      .then((data) =>
        setHistory(
          data.reduce((acc, curr) => {
            if (!acc[curr["flightId"]]) {
              acc[curr["flightId"]] = [];
            }
            acc[curr["flightId"]].push(curr);
            return acc;
          }, {})
        )
      );
  }, [history]);

  console.log(history);

  return (
    <>
      <div className="history-container">
        {Object.entries(history).map(([id, arr]) => {
          return (
            <table>
              <thead>
                <th colSpan={4} className="table-header">
                  <p style={{ margin: "0" }}>Flight Id: {id}</p>
                </th>
                <tr>
                  <th>Code</th>
                  <th>From</th>
                  <th>To</th>
                  <th>Arrival Date</th>
                </tr>
              </thead>
              <tbody>
                {arr.map((hist) => {
                  return (
                    <tr>
                      <td>{hist.code}</td>
                      <td>{hist.from}</td>
                      <td>{hist.to}</td>
                      <td>{formatDateTime(hist.arrivedAt)}</td>
                    </tr>
                  );
                })}
              </tbody>
            </table>
          );
        })}
      </div>
    </>
  );
};
