import { createContext, useContext, useState, useCallback, useEffect } from 'react';
import type { ReactNode } from 'react';
import { REFRESH_KEY } from '../services/api';

interface AuthUser {
    email: string;
    role: string;
    artistId: number | null;
    displayName: string | null;
}

interface AuthContextValue {
    user: AuthUser | null;
    token: string | null;
    login: (email: string, password: string) => Promise<void>;
    logout: () => void;
    isLoading: boolean;
}

const AuthContext = createContext<AuthContextValue | null>(null);

const API_BASE = import.meta.env.VITE_API_URL ?? 'http://localhost:5177';
const TOKEN_KEY = 'tattoo_access_token';
const USER_KEY = 'tattoo_user';

export function AuthProvider({ children }: { children: ReactNode }) {
    const [user, setUser] = useState<AuthUser | null>(null);
    const [token, setToken] = useState<string | null>(null);
    const [isLoading, setIsLoading] = useState(true);

    // Rehydrate from localStorage on mount
    useEffect(() => {
        const storedToken = localStorage.getItem(TOKEN_KEY);
        const storedUser = localStorage.getItem(USER_KEY);
        if (storedToken && storedUser) {
            setToken(storedToken);
            setUser(JSON.parse(storedUser));
        }
        setIsLoading(false);
    }, []);

    const login = useCallback(async (email: string, password: string) => {
        const res = await fetch(`${API_BASE}/api/auth/login`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ email, password }),
        });

        if (!res.ok) {
            const error = await res.json().catch(() => ({ error: 'Error de conexión' }));
            throw new Error(error?.error ?? 'Credenciales inválidas');
        }

        const data = await res.json();
        // .NET serializa en PascalCase por defecto; soportar ambas variantes
        const accessToken = data.token ?? data.Token;
        const refreshToken = data.refreshToken ?? data.RefreshToken;
        const userEmail = data.email ?? data.Email ?? '';
        const userRole = data.role ?? data.Role ?? 'Artist';
        const userArtistId = data.artistId ?? data.ArtistId ?? null;
        const userDisplayName = data.displayName ?? data.DisplayName ?? null;

        if (!accessToken) throw new Error('El servidor no devolvió un token válido');

        const authUser: AuthUser = {
            email: userEmail,
            role: userRole,
            artistId: userArtistId,
            displayName: userDisplayName,
        };

        setToken(accessToken);
        setUser(authUser);
        localStorage.setItem(TOKEN_KEY, accessToken);
        localStorage.setItem(USER_KEY, JSON.stringify(authUser));

        // Guardar refresh token para que el interceptor de axios pueda renovar silenciosamente
        if (refreshToken) localStorage.setItem(REFRESH_KEY, refreshToken);
    }, []);

    const logout = useCallback(() => {
        setToken(null);
        setUser(null);
        localStorage.removeItem(TOKEN_KEY);
        localStorage.removeItem(REFRESH_KEY);
        localStorage.removeItem(USER_KEY);
    }, []);

    return (
        <AuthContext.Provider value={{ user, token, login, logout, isLoading }}>
            {children}
        </AuthContext.Provider>
    );
}

export function useAuth(): AuthContextValue {
    const ctx = useContext(AuthContext);
    if (!ctx) throw new Error('useAuth must be used within AuthProvider');
    return ctx;
}
