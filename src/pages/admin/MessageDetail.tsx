import { useState, useEffect } from "react";
import { useParams, useNavigate, Link } from "react-router-dom";
import { Card, CardHeader, CardTitle, CardContent } from "@/components/ui/card";
import { contactService } from "@/services/contact";
import type { ContactMessage } from "@/types";
import { ArrowLeft } from "lucide-react";

export default function MessageDetail() {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const [msg, setMsg] = useState<ContactMessage | null>(null);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState("");

  useEffect(() => {
    document.title = "Detalle de Mensaje — Admin | Ink Studio";
    if (!id) {
      navigate("/admin/messages");
      return;
    }

    const fetchMessage = async () => {
      try {
        const data = await contactService.getAdminById(Number(id));
        setMsg(data);
      } catch (err) {
        setError("Error al cargar el mensaje.");
        if (err instanceof Error && (err.message.includes("401") || err.message.includes("403"))) {
          navigate("/admin/login");
        }
      } finally {
        setIsLoading(false);
      }
    };

    fetchMessage();
  }, [id, navigate]);

  return (
    <div className="max-w-2xl">
      {/* Breadcrumb back link */}
      <Link
        to="/admin/messages"
        className="inline-flex items-center gap-2 text-sm text-zinc-400 hover:text-white transition-colors mb-6"
      >
        <ArrowLeft className="w-4 h-4" />
        Volver a Mensajes
      </Link>

      <div className="mb-6">
        <h1 className="text-2xl font-bold tracking-tight">Detalle de Mensaje</h1>
      </div>

      {isLoading ? (
        <div className="text-center py-20 text-zinc-500">Cargando...</div>
      ) : error ? (
        <div className="bg-red-900/20 border border-red-500/50 p-4 rounded text-red-200">
          {error}
        </div>
      ) : msg && (
        <Card className="bg-zinc-900 border-zinc-800 overflow-hidden">
          <CardHeader className="p-6 border-b border-zinc-800">
            <div className="flex justify-between items-start">
              <div>
                <CardTitle className="text-2xl font-bold text-white mb-2">{msg.name}</CardTitle>
                <div className="text-zinc-400 text-sm space-y-1">
                  <p>
                    Email:{" "}
                    <a href={`mailto:${msg.email}`} className="text-blue-400 hover:underline">
                      {msg.email}
                    </a>
                  </p>
                  <p>
                    Teléfono:{" "}
                    <a href={`tel:${msg.phone}`} className="text-blue-400 hover:underline">
                      {msg.phone}
                    </a>
                  </p>
                </div>
              </div>
              <div className="flex flex-col items-end gap-2">
                {msg.wantsAppointment && (
                  <span className="px-3 py-1 text-sm font-medium rounded-full bg-blue-900/50 text-blue-300 border border-blue-800">
                    Quiere una Cita
                  </span>
                )}
                <span className="text-xs text-zinc-500">
                  {new Date(msg.createdAt).toLocaleString("es-ES")}
                </span>
              </div>
            </div>
          </CardHeader>
          <CardContent className="p-6">
            <h3 className="text-sm font-medium text-zinc-400 uppercase tracking-wider mb-4">
              Mensaje
            </h3>
            <div className="bg-zinc-950 p-4 rounded-md border border-zinc-800/50 text-zinc-300 leading-relaxed whitespace-pre-wrap">
              {msg.message}
            </div>

            <div className="mt-8 pt-6 border-t border-zinc-800">
              <p className="text-sm text-zinc-500">
                Notificación por correo:{" "}
                {msg.emailSent ? (
                  <span className="text-green-400">Enviada al artista</span>
                ) : (
                  <span className="text-yellow-500">No enviada</span>
                )}
              </p>
            </div>
          </CardContent>
        </Card>
      )}
    </div>
  );
}
