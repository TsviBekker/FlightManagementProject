import { BackendApi } from "../Constants/Constants";

export const sendFlightTo = async (flightId, stationId) => {
  fetch(`${BackendApi}BusinessLogic/send-flight-to/${flightId}&${stationId}`)
    .then((res) => res.json())
    .then((data) => console.log(data))
    .catch((err) => console.log(err));
};

export const releaseFlightFrom = async (flightId, stationId) => {
  fetch(
    `${BackendApi}BusinessLogic/release-flight-from/${flightId}&${stationId}`
  )
    .then((res) => res.json())
    .then((data) => console.log(data))
    .catch((err) => console.log(err));
};

export const receiveFlightAt = async (flightId, stationId) => {
  fetch(`${BackendApi}BusinessLogic/receive-flight-at/${flightId}&${stationId}`)
    .then((res) => res.json())
    .then((data) => console.log(data))
    .catch((err) => console.log(err));
};
