import { NavLink } from 'react-router-dom';
import { useAuth } from '../../context/AuthContext';

const navLinks = [
    { to: '/', icon: '📊', label: 'Dashboard' },
    { to: '/gallery', icon: '🖼️', label: 'Mi Galería' },
    { to: '/profile', icon: '👤', label: 'Mi Perfil' },
];

export function Sidebar() {
    const { user, logout } = useAuth();

    const initials = user?.displayName
        ? user.displayName.split(' ').map(n => n[0]).join('').toUpperCase().slice(0, 2)
        : user?.email?.[0]?.toUpperCase() ?? '?';

    return (
        <aside className="sidebar">
            {/* Brand */}
            <div className="sidebar-brand">
                <div className="sidebar-brand-icon">🖋️</div>
                <div>
                    <div className="sidebar-brand-title">Tattoo Ink</div>
                    <div className="sidebar-brand-subtitle">Panel de artista</div>
                </div>
            </div>

            {/* Nav */}
            <nav className="sidebar-nav">
                {navLinks.map(link => (
                    <NavLink
                        key={link.to}
                        to={link.to}
                        end={link.to === '/'}
                        className={({ isActive }) => `sidebar-nav-link${isActive ? ' active' : ''}`}
                    >
                        <span className="nav-icon">{link.icon}</span>
                        {link.label}
                    </NavLink>
                ))}
            </nav>

            {/* User + logout */}
            <div className="sidebar-footer">
                <div className="sidebar-user">
                    <div className="sidebar-user-avatar">{initials}</div>
                    <div>
                        <div className="sidebar-user-name">{user?.displayName ?? user?.email}</div>
                        <div className="sidebar-user-role">{user?.role}</div>
                    </div>
                </div>
                <button
                    className="btn btn-ghost"
                    style={{ width: '100%', justifyContent: 'center', fontSize: '0.8rem' }}
                    onClick={logout}
                >
                    🚪 Cerrar sesión
                </button>
            </div>
        </aside>
    );
}
