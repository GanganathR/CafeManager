import { useEffect, useState } from "react";
import { getCafes, deleteCafe } from "../services/api";
import { useNavigate } from "react-router-dom";
import { AgGridReact } from "ag-grid-react";
import "ag-grid-community/styles/ag-grid.css";
import "ag-grid-community/styles/ag-theme-quartz.css";
import { Button, Box, Paper, Typography, Stack, TextField } from "@mui/material";
import ConfirmDialog from "../components/ConfirmDialog";
import Notification from "../components/Notification";
import { ModuleRegistry } from "ag-grid-community";
import { AllCommunityModule } from "ag-grid-community";
ModuleRegistry.registerModules([AllCommunityModule]);

export default function CafesPage() {
  const [cafes, setCafes] = useState([]);
  const [location, setLocation] = useState(""); // 🔹 location filter
  const [selectedId, setSelectedId] = useState(null);
  const [confirmOpen, setConfirmOpen] = useState(false);
  const [notification, setNotification] = useState({
    open: false,
    message: "",
    severity: "success",
  });
  const navigate = useNavigate();

  useEffect(() => {
    fetchCafes();
  }, []);

  const fetchCafes = async (loc = "") => {
    try {
      const res = await getCafes(loc); 
      setCafes(res.data);
    } catch (err) {
      console.error("Failed to load cafes", err);
      setNotification({
        open: true,
        message: "Failed to load cafes.",
        severity: "error",
      });
    }
  };

  const handleDelete = async () => {
    try {
      await deleteCafe(selectedId);
      setNotification({
        open: true,
        message: "Cafe deleted successfully.",
        severity: "success",
      });
      fetchCafes(location);
    } catch {
      setNotification({
        open: true,
        message: "Error deleting cafe.",
        severity: "error",
      });
    }
    setConfirmOpen(false);
  };

  const handleFilter = () => {
    fetchCafes(location);
  };

  const handleReset = () => {
    setLocation("");
    fetchCafes();
  };

  const columns = [
    { headerName: "ID", field: "id" ,hide:true},
    { headerName: "Name", field: "name" },
    { headerName: "Description", field: "description" },
    { headerName: "Location", field: "location" },
    {
      headerName: "Employees",
      field: "employees",
      cellRenderer: (params) => (
        <Button
          variant="text"
          size="small"
          onClick={() =>
            navigate(`/employees?cafe=${encodeURIComponent(params.data.name)}`)
          }
        >
          {params.value}
        </Button>
      ),
    },
    {
      headerName: "Actions",
      cellRenderer: (params) => (
        <Stack direction="row" spacing={1}>
          <Button
            size="small"
            variant="outlined"
            onClick={() => navigate(`/cafe/edit/${params.data.id}`)}
          >
            Edit
          </Button>
          <Button
            size="small"
            variant="contained"
            color="error"
            onClick={() => {
              setSelectedId(params.data.id);
              setConfirmOpen(true);
            }}
          >
            Delete
          </Button>
        </Stack>
      ),
    },
  ];

  return (
    <Paper elevation={3} sx={{ p: 3, borderRadius: 2 }}>
      <Typography variant="h5" gutterBottom>
        Cafes
      </Typography>

      {/* Location Filter */}
      <Box sx={{ display: "flex", gap: 2, mb: 2 }}>
        <TextField
          label="Filter by Location"
          variant="outlined"
          value={location}
          onChange={(e) => setLocation(e.target.value)}
        />
        <Button variant="contained" color="primary" onClick={handleFilter}>
          Apply
        </Button>
        <Button variant="outlined" onClick={handleReset}>
          Reset
        </Button>
      </Box>

      <Button
        variant="contained"
        color="success"
        onClick={() => navigate("/cafe/add")}
        style={{ marginBottom: "10px" }}
      >
        Add New Cafe
      </Button>

      <Box className="ag-theme-quartz" sx={{ height: 500 }}>
        <AgGridReact rowData={cafes} columnDefs={columns} pagination />
      </Box>

      <ConfirmDialog
        open={confirmOpen}
        title="Delete Cafe"
        message="Are you sure you want to delete this cafe?"
        onConfirm={handleDelete}
        onCancel={() => setConfirmOpen(false)}
      />

      <Notification
        open={notification.open}
        message={notification.message}
        severity={notification.severity}
        onClose={() => setNotification({ ...notification, open: false })}
      />
    </Paper>
  );
}
