import { useEffect, useState } from "react";
import { TextField, Button, Box, Paper, Typography, Stack } from "@mui/material";
import { useNavigate, useParams } from "react-router-dom";
import { createCafe, updateCafe, getCafes } from "../services/api";

export default function CafeFormPage() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [form, setForm] = useState({
    name: "",
    description: "",
    location: ""
  });
  const [dirty, setDirty] = useState(false);

  // Load existing data when editing
  useEffect(() => {
    if (id) {
      fetchCafe();
    }
  }, [id]);

  const fetchCafe = async () => {
    try {
      const res = await getCafes();
      const cafe = res.data.find((c) => c.id === id);
      if (cafe) {
        setForm({
          name: cafe.name,
          description: cafe.description,
          location: cafe.location
        });
      }
    } catch (err) {
      console.error("Failed to fetch cafe:", err);
    }
  };

  const handleChange = (e) => {
    setDirty(true);
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    //  Validation
    if (form.name.length < 6 || form.name.length > 10) {
      alert("Name must be between 6 and 10 characters.");
      return;
    }
    if (form.description.length > 256) {
      alert("Description must be less than 256 characters.");
      return;
    }

    try {
      if (id) {
        await updateCafe({ id, ...form });
      } else {
        await createCafe(form);
      }
      navigate("/cafes");
    } catch (err) {
      console.error("Failed to save cafe:", err);
      alert("Failed to save cafe.");
    }
  };

  // Warn user if there are unsaved changes
  useEffect(() => {
    const handleBeforeUnload = (e) => {
      if (dirty) {
        e.preventDefault();
        e.returnValue = "";
      }
    };
    window.addEventListener("beforeunload", handleBeforeUnload);
    return () => window.removeEventListener("beforeunload", handleBeforeUnload);
  }, [dirty]);

  return (
    <Paper elevation={3} sx={{ p: 3, borderRadius: 2, maxWidth: 600, mx: "auto" }}>
      <Typography variant="h5" gutterBottom>
        {id ? "Edit Cafe" : "Add New Cafe"}
      </Typography>
      <form onSubmit={handleSubmit}>
        <TextField
          fullWidth
          label="Name"
          name="name"
          value={form.name}
          onChange={handleChange}
          margin="normal"
        />
        <TextField
          fullWidth
          label="Description"
          name="description"
          value={form.description}
          onChange={handleChange}
          margin="normal"
          multiline
          rows={3}
        />
        <TextField
          fullWidth
          label="Location"
          name="location"
          value={form.location}
          onChange={handleChange}
          margin="normal"
        />

        <Stack direction="row" spacing={2} sx={{ mt: 2 }}>
          <Button type="submit" variant="contained" color="primary">
            Submit
          </Button>
          <Button variant="outlined" onClick={() => navigate("/cafes")}>
            Cancel
          </Button>
        </Stack>
      </form>
    </Paper>
  );
}
