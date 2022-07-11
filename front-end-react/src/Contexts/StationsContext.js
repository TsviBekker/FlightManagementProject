import { createContext, useEffect, useState } from "react";
import { BackendApi } from "../Constants/Constants";

export const StationsContext = createContext();

export const StationsProvider = (props) => {
  const [stations, setStations] = useState([]);

  useEffect(() => {
    fetch(`${BackendApi}BusinessLogic/stations-overview`)
      .then((res) => res.json())
      .then((data) => setStations(data))
      .catch((err) => console.log(err));
  }, []);

  return (
    <StationsContext.Provider value={stations}>
      {props.children}
    </StationsContext.Provider>
  );
};
