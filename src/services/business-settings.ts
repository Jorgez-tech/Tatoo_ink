import { getApiUrl } from "@/config/api";
import { fetchWithAuth } from "./auth";
import type { BusinessSettings } from "@/types";

export const businessSettingsService = {
  /** Endpoint público — no requiere autenticación */
  getPublic: async (): Promise<BusinessSettings> => {
    const response = await fetch(getApiUrl("businessSettings"));
    if (!response.ok) {
      throw new Error(`Error fetching business settings: ${response.status} ${response.statusText}`);
    }
    return response.json();
  },

  /** Endpoint interno — requiere token de admin/artist. Usa fetchWithAuth para manejo de 401 automático. */
  getInternal: async (): Promise<BusinessSettings> => {
    const response = await fetchWithAuth(getApiUrl("internalBusinessSettings"));
    if (!response.ok) {
      throw new Error(`Error fetching internal business settings: ${response.status} ${response.statusText}`);
    }
    return response.json();
  },

  /** Actualiza configuración del negocio — requiere token de admin. */
  updateInternal: async (payload: BusinessSettings): Promise<BusinessSettings> => {
    const response = await fetchWithAuth(getApiUrl("internalBusinessSettings"), {
      method: "PATCH",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(payload),
    });

    if (!response.ok) {
      throw new Error(`Error updating business settings: ${response.status} ${response.statusText}`);
    }

    return response.json();
  },
};
