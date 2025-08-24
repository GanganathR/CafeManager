import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { createEmployee, updateEmployee, getEmployees, getCafes } from "../services/api";
import { TextField, Button, MenuItem, RadioGroup, FormControlLabel, Radio } from "@mui/material";

export default function EmployeeFormPage() {
  const { id } = useParams();
  const [form, setForm] = useState({
    name: "",
    emailAddress: "",
    phoneNumber: "",
    gender: "Male",
    cafeId: "",
    startDate: "",
    daysWorked: 0,
  });
  const [cafes, setCafes] = useState([]);
  const navigate = useNavigate();

  // Load cafes
  useEffect(() => {
    async function fetchCafes() {
      const res = await getCafes();
      setCafes(res.data);
    }
    fetchCafes();
  }, []);

  // Load employee for edit (after cafes loaded)
  useEffect(() => {
    if (id && cafes.length > 0) {
      async function fetchEmployee() {
        const res = await getEmployees();
        const emp = res.data.find((e) => e.id === id);

        if (emp) {
          const startDate = new Date();
          startDate.setDate(startDate.getDate() - emp.daysWorked);

          setForm({
            name: emp.name,
            emailAddress: emp.emailAddress,
            phoneNumber: emp.phoneNumber,
            gender: emp.gender,
            cafeId: emp.cafeId || "",   //  backend now returns cafeId
            startDate: startDate.toISOString().split("T")[0],
            daysWorked: emp.daysWorked,
          });
        }
      }
      fetchEmployee();
    }
  }, [id, cafes]);

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    const payload = {
      name: form.name,
      emailAddress: form.emailAddress,
      phoneNumber: form.phoneNumber,
      gender: form.gender,
      cafeId: form.cafeId || null,
      startDate: new Date(form.startDate).toISOString(),
    };

    try {
      if (id) {
        await updateEmployee({ id, ...payload });
      } else {
        await createEmployee(payload);
      }
      navigate("/employees");
    } catch (err) {
      console.error("Save failed", err);
      alert("Failed to save employee.");
    }
  };

  return (
    <form onSubmit={handleSubmit} style={{ maxWidth: "600px", margin: "auto" }}>
      <h2>{id ? "Edit Employee" : "Add New Employee"}</h2>

      <TextField fullWidth label="Name" name="name" value={form.name} onChange={handleChange} margin="normal" />
      <TextField fullWidth label="Email" name="emailAddress" value={form.emailAddress} onChange={handleChange} margin="normal" />
      <TextField fullWidth label="Phone Number" name="phoneNumber" value={form.phoneNumber} onChange={handleChange} margin="normal" />

      <RadioGroup row name="gender" value={form.gender} onChange={handleChange}>
        <FormControlLabel value="Male" control={<Radio />} label="Male" />
        <FormControlLabel value="Female" control={<Radio />} label="Female" />
      </RadioGroup>

      {/*  Assigned Cafe Dropdown */}
      <TextField
        select
        label="Assigned Cafe"
        name="cafeId"
        value={form.cafeId || ""}
        onChange={handleChange}
        fullWidth
        margin="normal"
      >
        {cafes.length === 0 ? (
          <MenuItem disabled>Loading cafes...</MenuItem>
        ) : (
          cafes.map((c) => (
            <MenuItem key={c.id} value={c.id}>
              {c.name}
            </MenuItem>
          ))
        )}
      </TextField>

      <TextField
        label="Start Date"
        type="date"
        name="startDate"
        value={form.startDate}
        onChange={handleChange}
        fullWidth
        margin="normal"
        InputLabelProps={{ shrink: true }}
      />

      <Button type="submit" variant="contained" color="primary">
        Submit
      </Button>
      <Button onClick={() => navigate("/employees")} style={{ marginLeft: "10px" }}>
        Cancel
      </Button>
    </form>
  );
}
