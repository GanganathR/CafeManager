import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import App from "./App.jsx";
import { ThemeProvider, createTheme, CssBaseline } from "@mui/material";

const theme = createTheme({
  palette: {
    primary: {
      main: "#1976d2", // professional blue
    },
    secondary: {
      main: "#388e3c", // green for success buttons
    },
    error: {
      main: "#d32f2f",
    },
  },
  typography: {
    fontFamily: "'Inter', 'Roboto', sans-serif",
    h2: {
      fontWeight: 600,
      fontSize: "1.8rem",
      marginBottom: "1rem",
    },
    button: {
      textTransform: "none",
      fontWeight: 500,
    },
  },
  shape: {
    borderRadius: 10,
  },
});

createRoot(document.getElementById("root")).render(
  <StrictMode>
    <ThemeProvider theme={theme}>
      <CssBaseline />
      <App />
    </ThemeProvider>
  </StrictMode>
);
