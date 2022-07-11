import { BackendApi } from "../Constants/Constants";

export const sendFlightTo = async (flightId, stationId) => {
  var ret = await fetch(
    `${BackendApi}BusinessLogic/send-flight-to/${flightId}&${stationId}`
  )
    .then((res) => console.log(res))
    .catch((err) => console.log(err));
  return ret;
};

export const releaseFlightFrom = async (flightId, stationId) => {
  var ret = await fetch(
    `${BackendApi}BusinessLogic/release-flight-from/${flightId}&${stationId}`
  )
    .then((res) => console.log(res))
    .catch((err) => console.log(err));
  return ret;
};

export const receiveFlightAt = async (flightId, stationId) => {
  var ret = await fetch(
    `${BackendApi}BusinessLogic/receive-flight-at/${flightId}&${stationId}`
  )
    .then((res) => console.log(res))
    .catch((err) => console.log(err));
  return ret;
};
