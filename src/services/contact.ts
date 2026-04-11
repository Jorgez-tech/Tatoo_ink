import { getApiUrl } from "@/config/api";
import type { ContactMessage } from "@/types";
import { authService } from "./auth";

export const contactService = {
  getAdminAll: async (): Promise<ContactMessage[]> => {
    const response = await fetch(getApiUrl("contact"), {
      headers: {
        ...authService.getAuthHeader(),
      },
    });

    if (!response.ok) {
      throw new Error(`Error fetching admin contacts: ${response.status} ${response.statusText}`);
    }
    return response.json();
  },

  getAdminById: async (id: number): Promise<ContactMessage> => {
    const response = await fetch(`${getApiUrl("contact")}/${id}`, {
      headers: {
        ...authService.getAuthHeader(),
      },
    });

    if (!response.ok) {
      throw new Error(`Error fetching contact details: ${response.statusText}`);
    }
    return response.json();
  },
};
