import { api } from "@/config/api";
import type { GalleryImage } from "@/types";

export const galleryService = {
    getAll: async (): Promise<GalleryImage[]> => {
        const response = await api.get<GalleryImage[]>("/gallery");
        return response.data;
    },
};
