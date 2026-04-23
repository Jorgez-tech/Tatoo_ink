import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { Button } from "@/components/ui/button";
import { Card, CardHeader, CardTitle, CardContent } from "@/components/ui/card";
import { contactService } from "@/services/contact";
import type { ContactMessage } from "@/types";
import { InboxIcon } from "lucide-react";

export default function Messages() {
  const [messages, setMessages] = useState<ContactMessage[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState("");
  const navigate = useNavigate();

  useEffect(() => {
    document.title = "Mensajes — Admin | Ink Studio";
    const fetchMessages = async () => {
      try {
        const data = await contactService.getAdminAll();
        setMessages(data);
      } catch (err) {
        setError("Error al cargar los mensajes. Verifica tu sesión.");
        if (err instanceof Error && (err.message.includes("401") || err.message.includes("403"))) {
          navigate("/admin/login");
        }
      } finally {
        setIsLoading(false);
      }
    };
    fetchMessages();
  }, [navigate]);

  return (
    <div>
      {/* Page header */}
      <div className="mb-8">
        <h1 className="text-2xl font-bold tracking-tight">Mensajes de Contacto</h1>
        <p className="text-zinc-400 text-sm mt-1">
          {!isLoading && !error && `${messages.length} mensaje${messages.length !== 1 ? "s" : ""} recibido${messages.length !== 1 ? "s" : ""}`}
        </p>
      </div>

      {isLoading ? (
        <div className="text-center py-20 text-zinc-500">Cargando mensajes...</div>
      ) : error ? (
        <div className="bg-red-900/20 border border-red-500/50 p-4 rounded text-red-200">
          {error}
        </div>
      ) : messages.length === 0 ? (
        <div className="flex flex-col items-center justify-center py-24 text-zinc-600 gap-3">
          <InboxIcon className="w-10 h-10" />
          <p className="text-sm">No hay mensajes de contacto todavía.</p>
        </div>
      ) : (
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
          {messages.map((msg) => (
            <Card key={msg.id} className="bg-zinc-900 border-zinc-800 overflow-hidden">
              <CardHeader className="p-4 border-b border-zinc-800">
                <CardTitle className="text-lg flex justify-between items-start">
                  <div className="flex flex-col">
                    <span className="truncate font-semibold text-white">{msg.name}</span>
                    <span className="text-sm text-zinc-400 font-normal">{msg.email}</span>
                  </div>
                  {msg.wantsAppointment && (
                    <span className="text-xs px-2 py-1 rounded bg-blue-900/50 text-blue-300 flex-shrink-0">
                      Cita
                    </span>
                  )}
                </CardTitle>
              </CardHeader>
              <CardContent className="p-4">
                <p className="text-sm text-zinc-300 line-clamp-3 mb-4">{msg.message}</p>
                <p className="text-xs text-zinc-500 mb-4">
                  Recibido: {new Date(msg.createdAt).toLocaleDateString("es-ES", { day: "2-digit", month: "short", year: "numeric" })}
                </p>
                <Button
                  variant="outline"
                  className="w-full border-zinc-700 text-zinc-300 hover:bg-zinc-800"
                  onClick={() => navigate("/admin/messages/" + msg.id)}
                >
                  Ver detalle
                </Button>
              </CardContent>
            </Card>
          ))}
        </div>
      )}
    </div>
  );
}
