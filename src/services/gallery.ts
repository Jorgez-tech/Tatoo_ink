import { getApiUrl } from "@/config/api";
import type { GalleryImage } from "@/types";
import { authService } from "./auth";

export const galleryService = {
    getAll: async (): Promise<GalleryImage[]> => {
        const response = await fetch(getApiUrl("gallery"));
        if (!response.ok) {
            throw new Error(`Error fetching gallery: ${response.statusText}`);
        }
        return response.json();
    },

    getAdminAll: async (): Promise<GalleryImage[]> => {
        const response = await fetch(`${getApiUrl("gallery")}/admin`, {
            headers: {
                ...authService.getAuthHeader(),
            },
        });
        
        if (!response.ok) {
            throw new Error(`Error fetching admin gallery: ${response.status} ${response.statusText}`);
        }
        return response.json();
    },

    getById: async (id: number): Promise<GalleryImage> => {
        const response = await fetch(`${getApiUrl("gallery")}/${id}`, {
            headers: { ...authService.getAuthHeader() },
        });
        if (!response.ok) {
            throw new Error(`Error fetching image ${id}: ${response.status} ${response.statusText}`);
        }
        return response.json();
    },

    create: async (image: Partial<GalleryImage>): Promise<GalleryImage> => {
        const response = await fetch(`${getApiUrl("gallery")}/admin`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                ...authService.getAuthHeader(),
            },
            body: JSON.stringify(image),
        });

        if (!response.ok) {
            throw new Error(`Error creating image: ${response.statusText}`);
        }
        return response.json();
    },

    update: async (id: number, image: Partial<GalleryImage>): Promise<GalleryImage> => {
        const response = await fetch(`${getApiUrl("gallery")}/admin/${id}`, {
            method: "PATCH",
            headers: {
                "Content-Type": "application/json",
                ...authService.getAuthHeader(),
            },
            body: JSON.stringify(image),
        });

        if (!response.ok) {
            throw new Error(`Error updating image: ${response.statusText}`);
        }
        return response.json();
    },

    delete: async (id: number): Promise<void> => {
        const response = await fetch(`${getApiUrl("gallery")}/admin/${id}`, {
            method: "DELETE",
            headers: {
                ...authService.getAuthHeader(),
            },
        });

        if (!response.ok) {
            throw new Error(`Error deleting image: ${response.statusText}`);
        }
    },
};
