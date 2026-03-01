import { useEffect, useState } from "react";
import { getApiUrl } from "@/config/api";
import type { Artist } from "@/types";
import { Instagram, Loader2 } from "lucide-react";
import { ImageWithFallback } from "../ui/ImageWithFallback";

export function Artists() {
  const [artists, setArtists] = useState<Artist[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchArtists = async () => {
      try {
        const response = await fetch(getApiUrl("artist"));
        if (!response.ok) throw new Error("Error fetching artists");
        const data = await response.json();
        setArtists(data);
      } catch (err) {
        console.error("Failed to fetch artists:", err);
        setError("No se pudieron cargar los artistas.");
      } finally {
        setLoading(false);
      }
    };

    fetchArtists();
  }, []);

  if (loading) {
    return (
      <div className="py-20 flex justify-center items-center">
        <Loader2 className="w-8 h-8 animate-spin text-gray-400" />
      </div>
    );
  }

  if (error || artists.length === 0) return null;

  return (
    <section id="tatuadores" className="py-12 sm:py-16 md:py-20 px-4 bg-white">
      <div className="max-w-7xl mx-auto text-center">
        <h2 className="text-3xl sm:text-4xl md:text-5xl font-bold mb-4">Nuestros Artistas</h2>
        <p className="text-gray-600 mb-12 max-w-2xl mx-auto">
          Contamos con especialistas en diversos estilos para dar vida a tus ideas con la mayor precisión y arte.
        </p>

        <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-8 sm:gap-10">
          {artists.map((artist) => (
            <div key={artist.id} className="group flex flex-col items-center">
              <div className="relative w-48 h-48 sm:w-56 sm:h-56 mb-6 overflow-hidden rounded-full border-4 border-black/5 group-hover:border-black transition-colors">
                <ImageWithFallback
                  src={artist.avatarUrl || `https://api.dicebear.com/7.x/avataaars/svg?seed=${artist.displayName}`}
                  alt={artist.displayName}
                  className="w-full h-full object-cover grayscale group-hover:grayscale-0 transition-all duration-500"
                />
              </div>
              <h3 className="text-xl sm:text-2xl font-bold mb-2">{artist.displayName}</h3>
              <p className="text-sm sm:text-base text-gray-600 mb-4 px-4 line-clamp-3">
                {artist.bio || "Artista especializado en diversos estilos de tatuaje."}
              </p>
              {artist.instagramUrl && (
                <a
                  href={artist.instagramUrl}
                  target="_blank"
                  rel="noopener noreferrer"
                  className="flex items-center gap-2 text-black hover:text-gray-600 transition-colors"
                >
                  <Instagram className="w-4 h-4" />
                  <span className="text-sm font-medium">@{artist.instagramUrl.split('/').pop()}</span>
                </a>
              )}
            </div>
          ))}
        </div>
      </div>
    </section>
  );
}
