import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Home from "./pages/Home";
import Login from "./pages/admin/Login";
import Dashboard from "./pages/admin/Dashboard";
import GalleryForm from "./pages/admin/GalleryForm";
import { ProtectedRoute } from "./components/shared/ProtectedRoute";

export default function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/admin/login" element={<Login />} />
        <Route
          path="/admin"
          element={
            <ProtectedRoute>
              <Dashboard />
            </ProtectedRoute>
          }
        />
        <Route
          path="/admin/gallery/new"
          element={
            <ProtectedRoute>
              <GalleryForm />
            </ProtectedRoute>
          }
        />
        <Route
          path="/admin/gallery/edit/:id"
          element={
            <ProtectedRoute>
              <GalleryForm />
            </ProtectedRoute>
          }
        />
      </Routes>
    </Router>
  );
}
