import { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { Button } from "@/components/ui/button";
import { Card, CardHeader, CardTitle, CardContent } from "@/components/ui/card";
import { authService } from "@/services/auth";
import { contactService } from "@/services/contact";
import type { ContactMessage } from "@/types";

export default function MessageDetail() {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const [msg, setMsg] = useState<ContactMessage | null>(null);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState("");

  useEffect(() => {
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

    if (!authService.isAuthenticated()) {
      navigate("/admin/login");
    } else {
      fetchMessage();
    }
  }, [id, navigate]);

  return (
    <div className="min-h-screen bg-zinc-950 text-white p-6">
      <header className="max-w-2xl mx-auto flex justify-between items-center mb-10">
        <h1 className="text-3xl font-bold tracking-tight">Detalle de Mensaje</h1>
        <Button 
          variant="outline" 
          onClick={() => navigate("/admin/messages")}
          className="border-zinc-800 text-zinc-400 hover:text-white"
        >
          Volver a Mensajes
        </Button>
      </header>

      <main className="max-w-2xl mx-auto">
        {isLoading ? (
          <div className="text-center py-20">Cargando...</div>
        ) : error ? (
          <div className="bg-red-900/20 border border-red-500/50 p-4 rounded text-red-200">
            {error}
          </div>
        ) : msg && (
          <Card className="bg-zinc-900 border-zinc-800 overflow-hidden">
            <CardHeader className="p-6 border-b border-zinc-800 bg-zinc-900/50">
              <div className="flex justify-between items-start">
                <div>
                  <CardTitle className="text-2xl font-bold text-white mb-2">{msg.name}</CardTitle>
                  <div className="text-zinc-400 text-sm space-y-1">
                    <p>Email: <a href={`mailto:${msg.email}`} className="text-blue-400 hover:underline">{msg.email}</a></p>
                    <p>Teléfono: <a href={`tel:${msg.phone}`} className="text-blue-400 hover:underline">{msg.phone}</a></p>
                  </div>
                </div>
                <div className="flex flex-col items-end gap-2">
                  {msg.wantsAppointment && (
                    <span className="px-3 py-1 text-sm font-medium rounded-full bg-blue-900/50 text-blue-300 border border-blue-800">
                      Quiere una Cita
                    </span>
                  )}
                  <span className="text-xs text-zinc-500">
                    {new Date(msg.createdAt).toLocaleString()}
                  </span>
                </div>
              </div>
            </CardHeader>
            <CardContent className="p-6">
              <h3 className="text-sm font-medium text-zinc-400 uppercase tracking-wider mb-4">Mensaje</h3>
              <div className="bg-zinc-950 p-4 rounded-md border border-zinc-800/50 text-zinc-300 leading-relaxed whitespace-pre-wrap">
                {msg.message}
              </div>
              
              <div className="mt-8 pt-6 border-t border-zinc-800 flex justify-between items-center">
                <div className="text-sm text-zinc-500">
                  Notificación por correo: {msg.emailSent ? <span className="text-green-400">Enviada al artista</span> : <span className="text-yellow-500">No enviada</span>}
                </div>
              </div>
            </CardContent>
          </Card>
        )}
      </main>
    </div>
  );
}
