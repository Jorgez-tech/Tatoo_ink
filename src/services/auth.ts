import { API_BASE_URL, getApiUrl } from "@/config/api";
import type { LoginResponse, User } from "@/types";

const TOKEN_KEY = "ink_studio_token";
const USER_KEY = "ink_studio_user";

export const authService = {
  login: async (email: string, password: string): Promise<LoginResponse> => {
    const response = await fetch(`${getApiUrl("auth")}/login`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      credentials: "include",
      body: JSON.stringify({ email, password }),
    });

    if (!response.ok) {
      const errorData = await response.json().catch(() => ({}));
      throw new Error(errorData.title || "Error al iniciar sesi�n");
    }

    const data: LoginResponse = await response.json();
    localStorage.setItem(TOKEN_KEY, data.token);
    localStorage.setItem(USER_KEY, JSON.stringify(data.user));

    return data;
  },

  logout: async () => {
    try {
      await fetch(`${getApiUrl("auth")}/logout`, {
        method: "POST",
        credentials: "include"
      });
    } catch {
      // Ignoramos el error, borramos token de todas formas
    } finally {
      localStorage.removeItem(TOKEN_KEY);
      localStorage.removeItem(USER_KEY);
      window.location.href = "/admin/login";
    }
  },

  getToken: () => localStorage.getItem(TOKEN_KEY),

  getUser: (): User | null => {
    const userStr = localStorage.getItem(USER_KEY);
    return userStr ? JSON.parse(userStr) : null;
  },

  isAuthenticated: () => !!localStorage.getItem(TOKEN_KEY),

  getAuthHeader: (): Record<string, string> => {
    const token = localStorage.getItem(TOKEN_KEY);
    return token ? { Authorization: `Bearer ${token}` } : {};
  },

  refresh: async (): Promise<boolean> => {
    try {
      const response = await fetch(`${getApiUrl("auth")}/refresh`, {
        method: "POST",
        credentials: "include"
      });

      if (!response.ok) {
        return false;
      }
      const data = await response.json();
      localStorage.setItem(TOKEN_KEY, data.token);
      return true;
    } catch {
      return false;
    }
  }
};
