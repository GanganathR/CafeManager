import { useEffect, useState } from "react";
import { getEmployees, deleteEmployee } from "../services/api";
import { useNavigate, useLocation } from "react-router-dom";
import { AgGridReact } from "ag-grid-react";
import "ag-grid-community/styles/ag-grid.css";
import "ag-grid-community/styles/ag-theme-quartz.css";
import { Button, TextField, Box, Paper, Typography, Stack } from "@mui/material";
import ConfirmDialog from "../components/ConfirmDialog";
import Notification from "../components/Notification";
import { ModuleRegistry } from "ag-grid-community";
import { AllCommunityModule } from "ag-grid-community";
ModuleRegistry.registerModules([AllCommunityModule]);

export default function EmployeesPage() {
  const [employees, setEmployees] = useState([]);
  const [cafe, setCafe] = useState("");
  const [selectedId, setSelectedId] = useState(null);
  const [confirmOpen, setConfirmOpen] = useState(false);
  const [notification, setNotification] = useState({
    open: false,
    message: "",
    severity: "success",
  });
  const navigate = useNavigate();
  const location = useLocation();

  const fetchEmployees = async (cafeName = "") => {
    try {
      const res = await getEmployees(cafeName);
      setEmployees(res.data);
    } catch (err) {
      console.error("Failed to load employees", err);
      setNotification({
        open: true,
        message: "Failed to load employees.",
        severity: "error",
      });
    }
  };

  useEffect(() => {
    const params = new URLSearchParams(location.search);
    const cafeName = params.get("cafe");
    if (cafeName) {
      setCafe(cafeName);
      fetchEmployees(cafeName);
    } else {
      fetchEmployees();
    }
  }, [location.search]);

  const handleDelete = async () => {
    try {
      await deleteEmployee(selectedId);
      setNotification({
        open: true,
        message: "Employee deleted successfully.",
        severity: "success",
      });
      const params = new URLSearchParams(location.search);
      const cafeName = params.get("cafe");
      fetchEmployees(cafe || cafeName);
    } catch {
      setNotification({
        open: true,
        message: "Error deleting employee.",
        severity: "error",
      });
    }
    setConfirmOpen(false);
  };

  const handleFilter = () => {
    fetchEmployees(cafe);
  };

  const columns = [
    { headerName: "ID", field: "id" },
    { headerName: "Name", field: "name" },
    { headerName: "Email", field: "emailAddress" },
    { headerName: "Phone", field: "phoneNumber" },
    { headerName: "Days Worked", field: "daysWorked" },
    { headerName: "Cafe", field: "cafeName" },
    {
      headerName: "Actions",
      cellRenderer: (params) => (
        <Stack direction="row" spacing={1}>
          <Button
            size="small"
            variant="outlined"
            onClick={() => navigate(`/employee/edit/${params.data.id}`)}
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
        Employees
      </Typography>

      <Box sx={{ display: "flex", gap: 2, mb: 2 }}>
        <TextField
          label="Filter by Cafe Name"
          variant="outlined"
          size="small"
          value={cafe}
          onChange={(e) => setCafe(e.target.value)}
        />
        <Button variant="contained" onClick={handleFilter}>
          Apply
        </Button>
        <Button
          variant="outlined"
          onClick={() => {
            setCafe("");
            fetchEmployees();
          }}
        >
          Reset
        </Button>
        {/*  Restore Add New Employee button */}
        <Button
          variant="contained"
          color="success"
          onClick={() => navigate("/employee/add")}
        >
          Add New Employee
        </Button>
      </Box>

      <Box className="ag-theme-quartz" sx={{ height: 500 }}>
        <AgGridReact rowData={employees} columnDefs={columns} pagination />
      </Box>

      <ConfirmDialog
        open={confirmOpen}
        title="Delete Employee"
        message="Are you sure you want to delete this employee?"
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
