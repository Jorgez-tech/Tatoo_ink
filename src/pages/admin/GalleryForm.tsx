import { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { Textarea } from "@/components/ui/textarea";
import { galleryService } from "@/services/gallery";
import type { GalleryImage } from "@/types";

export default function GalleryForm() {
  const { id } = useParams<{ id: string }>();
  const isEditing = Boolean(id);
  const navigate = useNavigate();

  const [formData, setFormData] = useState<Partial<GalleryImage>>({
    src: "", // Fake file input URL o input default (para props)
    alt: "",
    category: "",
    photographer: "",
    description: "",
    isPublic: true,
  });

  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState("");

  useEffect(() => {
    if (isEditing && id) {
      setIsLoading(true);
      galleryService.getAdminAll()
        .then((images) => {
          const img = images.find((i) => i.id === Number(id));
          if (img) {
            setFormData(img);
          } else {
            setError("Imagen no encontrada.");
          }
        })
        .catch(() => setError("Error al cargar la imagen."))
        .finally(() => setIsLoading(false));
    }
  }, [id, isEditing]);

  const handleChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>
  ) => {
    const { name, value, type } = e.target as HTMLInputElement;
    const checked = (e.target as HTMLInputElement).checked;

    setFormData((prev) => ({
      ...prev,
      [name]: type === "checkbox" ? checked : value,
    }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setIsLoading(true);
    setError("");

    try {
      if (isEditing && id) {
        await galleryService.update(Number(id), formData);
      } else {
        await galleryService.create({ ...formData, src: formData.src || "/placeholder.jpg" });
      }
      navigate("/admin");
    } catch (err) {
      setError(err instanceof Error ? err.message : "Error interno");
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <div className="min-h-screen bg-zinc-950 text-white p-6">
      <div className="max-w-2xl mx-auto">
        <header className="flex justify-between items-center mb-8">
          <h1 className="text-2xl font-bold">
            {isEditing ? "Editar Trabajo" : "Nuevo Trabajo"}
          </h1>
          <Button
            variant="outline"
            onClick={() => navigate("/admin")}
            className="border-zinc-800 text-zinc-400 hover:text-white"
          >
            Volver
          </Button>
        </header>

        {error && (
          <div className="bg-red-900/50 border border-red-500 text-red-200 p-4 rounded mb-6">
            {error}
          </div>
        )}

        <form onSubmit={handleSubmit} className="space-y-6 bg-zinc-900 p-6 rounded border border-zinc-800">
          <div className="space-y-2">
            <Label htmlFor="src">URL de la Imagen (temporal)</Label>
            <Input
              id="src"
              name="src"
              value={formData.src || ""}
              onChange={handleChange}
              placeholder="/images/tattoos/mi-tattoo.jpg"
              className="bg-zinc-800 border-zinc-700 text-white"
              required
            />
          </div>

          <div className="space-y-2">
            <Label htmlFor="alt">Texto Alternativo (Alt)</Label>
            <Input
              id="alt"
              name="alt"
              value={formData.alt || ""}
              onChange={handleChange}
              placeholder="Tatuaje de dragón en antebrazo"
              className="bg-zinc-800 border-zinc-700 text-white"
              required
            />
          </div>

          <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
            <div className="space-y-2">
              <Label htmlFor="category">Categoría</Label>
              <Input
                id="category"
                name="category"
                value={formData.category || ""}
                onChange={handleChange}
                placeholder="Neotradicional, Realismo, etc."
                className="bg-zinc-800 border-zinc-700 text-white"
              />
            </div>
            <div className="space-y-2">
              <Label htmlFor="photographer">Tatuador / Artista</Label>
              <Input
                id="photographer"
                name="photographer"
                value={formData.photographer || ""}
                onChange={handleChange}
                placeholder="Nombre del artista"
                className="bg-zinc-800 border-zinc-700 text-white"
              />
            </div>
          </div>

          <div className="space-y-2">
            <Label htmlFor="description">Descripción (Opcional)</Label>
            <Textarea
              id="description"
              name="description"
              value={formData.description || ""}
              onChange={handleChange}
              placeholder="Detalles técnicos, inspiración o historia del diseño..."
              className="bg-zinc-800 border-zinc-700 text-white min-h-[100px]"
            />
          </div>

          <div className="flex items-center gap-2 pt-2">
            <input
              type="checkbox"
              id="isPublic"
              name="isPublic"
              checked={formData.isPublic}
              onChange={handleChange}
              className="w-4 h-4 bg-zinc-800 border-zinc-700 rounded rounded-sm accent-zinc-100"
            />
            <Label htmlFor="isPublic" className="cursor-pointer">
              Publicar inmediatamente (Visible en el sitio)
            </Label>
          </div>

          <div className="pt-4 flex gap-4">
            <Button
              type="submit"
              disabled={isLoading}
              className="bg-zinc-100 text-zinc-950 hover:bg-zinc-300 w-full"
            >
              {isLoading ? "Guardando..." : isEditing ? "Guardar Cambios" : "Subir Trabajo"}
            </Button>
          </div>
        </form>
      </div>
    </div>
  );
}
