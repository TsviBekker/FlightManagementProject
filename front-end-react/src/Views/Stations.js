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
            return <Station key={station.stationId} station={station} />;
          })}
        </tbody>
      </table>
    </div>
  );
};

const Station = ({ station, onClick }) => {
  const [time, setTime] = useState(station.flightInStation?.prepTime);

  useEffect(() => {
    setTimeout(() => {
      if (time > 0) {
        // console.log(station, station.flightInStation.prepTime);
        setTime(time - 1);
      }
    }, 1000);
  }, [time, station]);

  // useEffect(() => {
  //   setTimeout(() => {
  //     //IIFE
  //     (async () => {
  //       await releaseFlightFrom(
  //         station.flightInStation.flightId,
  //         station.stationId
  //       );

  //       await sendFlightTo(
  //         station.flightInStation.flightId,
  //         station.stationId + 1
  //       );
  //       await receiveFlightAt(
  //         station.flightInStation.flightId,
  //         station.stationId + 1
  //       );
  //     })();
  //   }, 1000 * station.flightInStation?.prepTime);
  // }, []);

  return (
    <tr onClick={onClick}>
      <td>{station.name}</td>
      <td>{station.flightInStation ? "Unavailable" : "Available"}</td>
      <td>{station.flightInStation ? station.flightInStation.code : "NONE"}</td>
      <td>
        {/* {formatTime(time)} */}
        {station.flightInStation
          ? formatTime(station.flightInStation?.prepTime)
          : "NO FLIGHT"}
      </td>
      <td>
        <StationHistory station={station} />
      </td>
    </tr>
  );
};
