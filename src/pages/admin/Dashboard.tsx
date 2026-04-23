import { useState, useEffect, useCallback } from "react";
import { useNavigate } from "react-router-dom";
import { Button } from "@/components/ui/button";
import { Card, CardHeader, CardTitle, CardContent } from "@/components/ui/card";
import { galleryService } from "@/services/gallery";
import type { GalleryImage } from "@/types";
import { Plus, ImageOff } from "lucide-react";

export default function Dashboard() {
  const [images, setImages] = useState<GalleryImage[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState("");
  const navigate = useNavigate();

  const fetchImages = useCallback(async () => {
    try {
      const data = await galleryService.getAdminAll();
      setImages(data);
    } catch (err) {
      setError("Error al cargar las imágenes. Verifica tu sesión.");
      if (err instanceof Error && (err.message.includes("401") || err.message.includes("403"))) {
        navigate("/admin/login");
      }
    } finally {
      setIsLoading(false);
    }
  }, [navigate]);

  useEffect(() => {
    document.title = "Galería — Admin | Ink Studio";
    fetchImages();
  }, [fetchImages]);

  const handleDelete = async (id?: number) => {
    if (!id) return;
    if (!confirm("¿Seguro que deseas eliminar esta imagen?")) return;
    try {
      await galleryService.delete(id);
      fetchImages();
    } catch {
      alert("Error al eliminar la imagen");
    }
  };

  // Stats
  const total = images.length;
  const publicas = images.filter((i) => i.isPublic).length;
  const ocultas = total - publicas;

  return (
    <div>
      {/* Page header */}
      <div className="flex items-center justify-between mb-8">
        <div>
          <h1 className="text-2xl font-bold tracking-tight">Gestión de Galería</h1>
          <p className="text-zinc-400 text-sm mt-1">Administra los trabajos publicados en el sitio</p>
        </div>
        <Button
          className="bg-zinc-100 text-zinc-950 hover:bg-zinc-300 gap-2"
          onClick={() => navigate("/admin/gallery/new")}
        >
          <Plus className="w-4 h-4" />
          Agregar Trabajo
        </Button>
      </div>

      {/* Stats */}
      <div className="grid grid-cols-3 gap-4 mb-8">
        {[
          { label: "Total", value: total, color: "text-white" },
          { label: "Públicas", value: publicas, color: "text-green-400" },
          { label: "Ocultas", value: ocultas, color: "text-zinc-400" },
        ].map(({ label, value, color }) => (
          <div key={label} className="bg-zinc-900 border border-zinc-800 rounded-lg p-4">
            <p className="text-xs text-zinc-500 uppercase tracking-wider mb-1">{label}</p>
            <p className={`text-3xl font-bold ${color}`}>{value}</p>
          </div>
        ))}
      </div>

      {/* Content */}
      {isLoading ? (
        <div className="text-center py-20 text-zinc-500">Cargando galería...</div>
      ) : error ? (
        <div className="bg-red-900/20 border border-red-500/50 p-4 rounded text-red-200">
          {error}
        </div>
      ) : images.length === 0 ? (
        <div className="flex flex-col items-center justify-center py-24 text-zinc-600 gap-3">
          <ImageOff className="w-10 h-10" />
          <p className="text-sm">No hay imágenes todavía. Agrega el primer trabajo.</p>
        </div>
      ) : (
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
          {images.map((image) => (
            <Card key={image.id} className="bg-zinc-900 border-zinc-800 overflow-hidden">
              <div className="aspect-video w-full overflow-hidden bg-zinc-800">
                <img
                  src={image.src}
                  alt={image.alt}
                  className="w-full h-full object-cover opacity-80 hover:opacity-100 transition-opacity"
                />
              </div>
              <CardHeader className="p-4">
                <CardTitle className="text-lg flex justify-between items-center">
                  <span className="truncate text-white">{image.category || "Sin categoría"}</span>
                  <span
                    className={`text-xs px-2 py-1 rounded flex-shrink-0 ${
                      image.isPublic
                        ? "bg-green-900/50 text-green-300"
                        : "bg-zinc-800 text-zinc-400"
                    }`}
                  >
                    {image.isPublic ? "Público" : "Oculto"}
                  </span>
                </CardTitle>
                {image.photographer && (
                  <p className="text-xs text-zinc-500 mt-1">{image.photographer}</p>
                )}
              </CardHeader>
              <CardContent className="p-4 pt-0 flex gap-2">
                <Button
                  variant="outline"
                  size="sm"
                  className="flex-1 border-zinc-700 text-zinc-300 hover:bg-zinc-800"
                  onClick={() => navigate(`/admin/gallery/edit/${image.id}`)}
                >
                  Editar
                </Button>
                <Button
                  variant="outline"
                  size="sm"
                  className="flex-1 border-red-900/50 text-red-400 hover:bg-red-900/20"
                  onClick={() => handleDelete(image.id)}
                >
                  Eliminar
                </Button>
              </CardContent>
            </Card>
          ))}
        </div>
      )}
    </div>
  );
}
