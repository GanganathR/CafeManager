import { BrowserRouter as Router, Routes, Route, Link } from "react-router-dom";
import CafesPage from "./pages/CafesPage";
import EmployeesPage from "./pages/EmployeesPage";
import CafeFormPage from "./pages/CafeFormPage";
import EmployeeFormPage from "./pages/EmployeeFormPage";

export default function App() {
  return (
    <Router>
      <nav style={{ padding: "10px", background: "#f5f5f5" }}>
        <Link to="/cafes" style={{ marginRight: "20px" }}>Cafes</Link>
        <Link to="/employees">Employees</Link>
      </nav>
      <Routes>
        {/* Redirect root to /cafes */}
        {/* <Route path="/" element={<Navigate to="/cafes" replace />} /> */}
       <Route path="/" element={<CafesPage />} />
        <Route path="/cafes" element={<CafesPage />} />
        <Route path="/employees" element={<EmployeesPage />} />
        <Route path="/cafe/add" element={<CafeFormPage />} />
        <Route path="/cafe/edit/:id" element={<CafeFormPage />} />
        <Route path="/employee/add" element={<EmployeeFormPage />} />
        <Route path="/employee/edit/:id" element={<EmployeeFormPage />} />
      </Routes>
    </Router>
  );
}
