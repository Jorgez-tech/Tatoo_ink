import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Home from "./pages/Home";
import Login from "./pages/admin/Login";
import Dashboard from "./pages/admin/Dashboard";
import GalleryForm from "./pages/admin/GalleryForm";
import Messages from "./pages/admin/Messages";
import MessageDetail from "./pages/admin/MessageDetail";
import BusinessSettingsPage from "./pages/admin/BusinessSettings";
import AdminLayout from "./components/layout/AdminLayout";

export default function App() {
  return (
    <Router>
      <Routes>
        {/* Sitio público */}
        <Route path="/" element={<Home />} />

        {/* Login (sin layout) */}
        <Route path="/admin/login" element={<Login />} />

        {/* Panel admin — todas las rutas hijas usan AdminLayout */}
        <Route path="/admin" element={<AdminLayout />}>
          <Route index element={<Dashboard />} />
          <Route path="gallery/new" element={<GalleryForm />} />
          <Route path="gallery/edit/:id" element={<GalleryForm />} />
          <Route path="messages" element={<Messages />} />
          <Route path="messages/:id" element={<MessageDetail />} />
          <Route path="settings" element={<BusinessSettingsPage />} />
        </Route>
      </Routes>
    </Router>
  );
}
