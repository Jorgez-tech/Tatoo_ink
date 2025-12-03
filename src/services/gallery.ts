import { getApiUrl } from "@/config/api";
import type { GalleryImage } from "@/types";

export const galleryService = {
    getAll: async (): Promise<GalleryImage[]> => {
        const response = await fetch(getApiUrl("gallery"));
        if (!response.ok) {
            throw new Error(`Error fetching gallery: ${response.statusText}`);
        }
        return response.json();
    },
};
