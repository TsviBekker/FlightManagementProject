import React, { useContext, useRef } from "react";
import { useEffect, useState } from "react";
import { StationHistory } from "../Components/Modal/Modal";
import { BackendApi } from "../Constants/Constants";
import { StationsContext } from "../Contexts/StationsContext";
import {
  receiveFlightAt,
  releaseFlightFrom,
  sendFlightTo,
} from "../Modules/flightManager";

const formatTime = (seconds) => {
  return `${seconds > 60 ? seconds / 60 + "min" : ""} ${seconds % 60}sec`;
};

export const Stations = () => {
  const [stations, setStations] = useState([]);

  useEffect(() => {
    var timout = setTimeout(() => {
      fetch(`${BackendApi}BusinessLogic/stations-overview`)
        .then((res) => res.json())
        .then((data) => {
          console.log("called");
          return setStations(data);
        })
        .catch((err) => console.log(err));
    }, 1000);
    return () => clearTimeout(timout);
  }, [stations]);

  // const stations = useContext(StationsContext);

  // console.log(stations);

  return (
    <div>
      <h1 className="center">Stations Overview</h1>
      <table>
        <thead>
          <tr>
            <th>Station Name</th>
            <th>Status</th>
            <th>Ocupied By</th>
            <th>Preparation Time</th>
          </tr>
        </thead>

        <tbody>
          {stations.map((station) => {
            return (
              <Station
                setStations={setStations}
                key={station.stationId}
                station={station}
              />
            );
          })}
        </tbody>
      </table>
    </div>
  );
};

const Station = ({ station, onClick, setStations }) => {
  const [time, setTime] = useState(station.flightInStation?.prepTime || 0);
  // const [init, setInit] = useState(false);

  useEffect(() => {
    setTimeout(() => {
      time > 0 && setTime(time - 1);
      //ADD HTTP REQUEST ==> SENDING FLIGHT AWAY FROM STATION
      if (time <= 0 && station.flightInStation) {
        //IIFE
        (async () => {
          await releaseFlightFrom(
            station.flightInStation.flightId,
            station.stationId
          );
          await sendFlightTo(
            station.flightInStation.flightId,
            station.stationId + 1
          );
          await receiveFlightAt(
            station.flightInStation.flightId,
            station.stationId + 1
          );
          var response = await fetch(
            `${BackendApi}BusinessLogic/stations-overview`
          );
          var data = await response.json();
          setStations(data);
        })();
      }
    }, 1000);
  }, [time]);

  return (
    <tr onClick={onClick}>
      <td>{station.name}</td>
      <td>{station.flightInStation ? "Unavailable" : "Available"}</td>
      <td>{station.flightInStation ? station.flightInStation.code : "NONE"}</td>
      <td>{formatTime(time)}</td>
      <td>
        <StationHistory station={station} />
      </td>
    </tr>
  );
};
