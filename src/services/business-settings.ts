import { getApiUrl } from "@/config/api";
import { authService } from "./auth";
import type { BusinessSettings } from "@/types";

export const businessSettingsService = {
  getPublic: async (): Promise<BusinessSettings> => {
    const response = await fetch(getApiUrl("businessSettings"));
    if (!response.ok) {
      throw new Error(`Error fetching business settings: ${response.status} ${response.statusText}`);
    }
    return response.json();
  },

  getInternal: async (): Promise<BusinessSettings> => {
    const response = await fetch(getApiUrl("internalBusinessSettings"), {
      method: "GET",
      headers: {
        ...authService.getAuthHeader(),
      },
    });

    if (!response.ok) {
      throw new Error(`Error fetching internal business settings: ${response.status} ${response.statusText}`);
    }

    return response.json();
  },

  updateInternal: async (payload: BusinessSettings): Promise<BusinessSettings> => {
    const response = await fetch(getApiUrl("internalBusinessSettings"), {
      method: "PATCH",
      headers: {
        "Content-Type": "application/json",
        ...authService.getAuthHeader(),
      },
      body: JSON.stringify(payload),
    });

    if (!response.ok) {
      throw new Error(`Error updating business settings: ${response.status} ${response.statusText}`);
    }

    return response.json();
  },
};
