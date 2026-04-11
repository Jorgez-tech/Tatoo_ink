import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { Button } from "@/components/ui/button";
import { Card, CardHeader, CardTitle, CardContent } from "@/components/ui/card";
import { authService } from "@/services/auth";
import { galleryService } from "@/services/gallery";
import type { GalleryImage } from "@/types";

export default function Dashboard() {
  const [images, setImages] = useState<GalleryImage[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState("");
  const navigate = useNavigate();

  const fetchImages = async () => {
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
  };

  useEffect(() => {
    if (!authService.isAuthenticated()) {
      navigate("/admin/login");
    } else {
      fetchImages();
    }
  }, [navigate]);

  const handleLogout = async () => {
    await authService.logout();
    navigate("/");
  };

  const handleDelete = async (id?: number) => {
    if (!id) return;
    if (!confirm("¿Seguro que deseas eliminar esta imagen?")) return;
    try {
      await galleryService.delete(id);
      fetchImages(); // Recargar la lista
    } catch (err) {
      alert("Error al eliminar la imagen");
    }
  };

  return (
    <div className="min-h-screen bg-zinc-950 text-white p-6">
      <header className="max-w-7xl mx-auto flex justify-between items-center mb-10">
        <h1 className="text-3xl font-bold tracking-tight">Panel de Administración</h1>
        <div className="flex gap-4">
          <Button
            variant="outline"
            onClick={() => navigate("/")}
            className="border-zinc-800 text-zinc-400 hover:text-white"
          >
            Ver Sitio
          </Button>
          <Button
            onClick={handleLogout}
            className="bg-red-900/80 hover:bg-red-800 text-white"
          >
            Cerrar Sesión
          </Button>
        </div>
      </header>

      <main className="max-w-7xl mx-auto">
        <section className="mb-12">
          <div className="flex justify-between items-center mb-6">
            <h2 className="text-xl font-semibold">Gestión de Galería</h2>
            <Button
              className="bg-zinc-100 text-zinc-950 hover:bg-zinc-300"
              onClick={() => navigate("/admin/gallery/new")}
            >
              Agregar Trabajo
            </Button>
          </div>

          {isLoading ? (
            <div className="text-center py-20">Cargando galería...</div>
          ) : error ? (
            <div className="bg-red-900/20 border border-red-500/50 p-4 rounded text-red-200">
              {error}
            </div>
          ) : (
            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
              {images.map((image) => (
                <Card key={image.id} className="bg-zinc-900 border-zinc-800 overflow-hidden">
                  <div className="aspect-video w-full overflow-hidden">
                    <img
                      src={image.src}
                      alt={image.alt}
                      className="w-full h-full object-cover opacity-80"
                    />
                  </div>
                  <CardHeader className="p-4">
                    <CardTitle className="text-lg flex justify-between items-center">
                      <span className="truncate text-white">{image.category || "Sin categoría"}</span>
                      <span className={`text-xs px-2 py-1 rounded ${image.isPublic ? 'bg-green-900/50 text-green-300' : 'bg-zinc-800 text-zinc-400'}`}>
                        {image.isPublic ? 'Público' : 'Oculto'}
                      </span>
                    </CardTitle>
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
        </section>
      </main>
    </div>
  );
}
