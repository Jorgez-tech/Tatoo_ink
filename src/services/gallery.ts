import { getApiUrl } from "@/config/api";
import { fetchWithAuth } from "./auth";
import type { GalleryImage } from "@/types";

export const galleryService = {
  /** Galería pública — no requiere autenticación */
  getAll: async (): Promise<GalleryImage[]> => {
    const response = await fetch(getApiUrl("gallery"));
    if (!response.ok) {
      throw new Error(`Error fetching gallery: ${response.statusText}`);
    }
    return response.json();
  },

  /** Galería completa para admin (incluye ocultas) — requiere token admin/artist */
  getAdminAll: async (): Promise<GalleryImage[]> => {
    const response = await fetchWithAuth(`${getApiUrl("gallery")}/admin`);
    if (!response.ok) {
      throw new Error(`Error fetching admin gallery: ${response.status} ${response.statusText}`);
    }
    return response.json();
  },

  /** Detalle de imagen — requiere token para acceder por ID */
  getById: async (id: number): Promise<GalleryImage> => {
    const response = await fetchWithAuth(`${getApiUrl("gallery")}/${id}`);
    if (!response.ok) {
      throw new Error(`Error fetching image ${id}: ${response.status} ${response.statusText}`);
    }
    return response.json();
  },

  /** Crea nueva imagen — requiere token admin/artist. La URL (Src) debe ser externa (Cloudinary, CDN, etc.) */
  create: async (image: Partial<GalleryImage>): Promise<GalleryImage> => {
    const response = await fetchWithAuth(`${getApiUrl("gallery")}/admin`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(image),
    });

    if (!response.ok) {
      throw new Error(`Error creating image: ${response.statusText}`);
    }
    return response.json();
  },

  /** Actualiza imagen existente (PATCH semántico) — requiere token admin/artist */
  update: async (id: number, image: Partial<GalleryImage>): Promise<GalleryImage> => {
    const response = await fetchWithAuth(`${getApiUrl("gallery")}/admin/${id}`, {
      method: "PATCH",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(image),
    });

    if (!response.ok) {
      throw new Error(`Error updating image: ${response.statusText}`);
    }
    return response.json();
  },

  /** Elimina imagen — requiere token de rol admin (no artist) */
  delete: async (id: number): Promise<void> => {
    const response = await fetchWithAuth(`${getApiUrl("gallery")}/admin/${id}`, {
      method: "DELETE",
    });

    if (!response.ok) {
      throw new Error(`Error deleting image: ${response.statusText}`);
    }
  },
};
