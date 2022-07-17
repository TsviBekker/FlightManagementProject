import React, { useState } from "react";
import Box from "@mui/material/Box";
import Button from "@mui/material/Button";
import Typography from "@mui/material/Typography";
import Modal from "@mui/material/Modal";
import { BackendApi } from "../../Constants/Constants";
import { formatDateTime } from "../../Utils/Utils";

const style = {
  position: "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  width: "75%",
  bgcolor: "background.paper",
  border: "2px solid #000",
  boxShadow: 24,
  p: 4,
};

export const StationHistory = ({ station }) => {
  const [open, setOpen] = useState(false);
  const [history, setHistory] = useState([]);

  const handleOpen = () => {
    console.log("Opening History...", station);
    setOpen(true);
    fetch(`${BackendApi}BusinessLogic/get-station-history/${station.stationId}`)
      .then((res) => res.json())
      .then((data) => setHistory(data));
  };

  const handleClose = () => setOpen(false);

  return (
    <div>
      <Button onClick={handleOpen}>Show history</Button>
      <Modal open={open} onClose={handleClose}>
        <Box sx={style}>
          <Typography id="modal-modal-title" variant="h6" component="h2">
            {station.name} History
          </Typography>
          <table>
            <thead>
              <tr>
                <th>flight id</th>
                <th>Arrived At</th>
                <th>Departed At</th>
              </tr>
            </thead>
            {history.length !== 0
              ? history.map((h) => {
                  return (
                    <tr>
                      <td>{h.flightId}</td>
                      <td>{formatDateTime(h.arrivedAt)}</td>
                      <td>{formatDateTime(h.departedAt)}</td>
                    </tr>
                  );
                })
              : "NO HISTORY FOR NOW..."}
          </table>
        </Box>
      </Modal>
    </div>
  );
};
