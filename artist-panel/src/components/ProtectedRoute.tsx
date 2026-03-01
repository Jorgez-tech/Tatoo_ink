import { Navigate, Outlet } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';

export function ProtectedRoute() {
    const { user, isLoading } = useAuth();

    if (isLoading) return (
        <div style={{ display: 'flex', alignItems: 'center', justifyContent: 'center', minHeight: '100vh' }}>
            <span className="spinner" style={{ width: 32, height: 32, borderWidth: 3 }} />
        </div>
    );

    return user ? <Outlet /> : <Navigate to="/login" replace />;
}
