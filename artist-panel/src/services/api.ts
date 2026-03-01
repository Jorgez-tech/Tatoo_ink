import axios from 'axios';

export const API_BASE = import.meta.env.VITE_API_URL ?? 'http://localhost:5177';

const TOKEN_KEY = 'tattoo_access_token';
const REFRESH_KEY = 'tattoo_refresh_token';
const USER_KEY = 'tattoo_user';

const api = axios.create({ baseURL: API_BASE });

// Adjuntar token JWT a cada request automáticamente
api.interceptors.request.use((config) => {
    const token = localStorage.getItem(TOKEN_KEY);
    if (token) config.headers.Authorization = `Bearer ${token}`;
    return config;
});

// Flag para evitar bucles de reintento infinito
let isRefreshing = false;

// Si el servidor responde 401, intentar renovar el token silenciosamente una vez
api.interceptors.response.use(
    (res) => res,
    async (error) => {
        const original = error.config;

        if (error.response?.status === 401 && !original._retry && !isRefreshing) {
            original._retry = true;
            isRefreshing = true;

            const refreshToken = localStorage.getItem(REFRESH_KEY);

            if (!refreshToken) {
                // Sin refresh token → logout
                _clearSession();
                return Promise.reject(error);
            }

            try {
                const { data } = await axios.post(
                    `${API_BASE}/api/auth/refresh`,
                    { refreshToken },
                    { headers: { 'Content-Type': 'application/json' } }
                );

                const newToken = data.token ?? data.Token;
                const newRefreshToken = data.refreshToken ?? data.RefreshToken;

                if (!newToken) throw new Error('Refresh response missing token');

                // Actualizar storage con los nuevos tokens
                localStorage.setItem(TOKEN_KEY, newToken);
                localStorage.setItem(REFRESH_KEY, newRefreshToken);

                // Reintentar la petición original con el nuevo token
                original.headers.Authorization = `Bearer ${newToken}`;
                return api(original);
            } catch {
                // Refresh falló → limpiar sesión y redirigir al login
                _clearSession();
                return Promise.reject(error);
            } finally {
                isRefreshing = false;
            }
        }

        return Promise.reject(error);
    }
);

function _clearSession() {
    localStorage.removeItem(TOKEN_KEY);
    localStorage.removeItem(REFRESH_KEY);
    localStorage.removeItem(USER_KEY);
    window.location.href = '/login';
}

export { REFRESH_KEY };
export default api;

