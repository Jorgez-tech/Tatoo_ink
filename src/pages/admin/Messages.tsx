import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { Button } from "@/components/ui/button";
import { Card, CardHeader, CardTitle, CardContent } from "@/components/ui/card";
import { authService } from "@/services/auth";
import { contactService } from "@/services/contact";
import type { ContactMessage } from "@/types";

export default function Messages() {
  const [messages, setMessages] = useState<ContactMessage[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState("");
  const navigate = useNavigate();

  useEffect(() => {
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

    if (!authService.isAuthenticated()) {
      navigate("/admin/login");
    } else {
      fetchMessages();
    }
  }, [navigate]);

  return (
    <div className="min-h-screen bg-zinc-950 text-white p-6">
      <header className="max-w-7xl mx-auto flex justify-between items-center mb-10">
        <h1 className="text-3xl font-bold tracking-tight">Mensajes de Contacto</h1>
        <div className="flex gap-4">
          <Button 
            variant="outline" 
            onClick={() => navigate("/admin")}
            className="border-zinc-800 text-zinc-400 hover:text-white"
          >
            Volver a Galería
          </Button>
        </div>
      </header>

      <main className="max-w-7xl mx-auto">
        {isLoading ? (
          <div className="text-center py-20">Cargando mensajes...</div>
        ) : error ? (
          <div className="bg-red-900/20 border border-red-500/50 p-4 rounded text-red-200">
            {error}
          </div>
        ) : messages.length === 0 ? (
          <div className="text-center py-20 text-zinc-500">No hay mensajes de contacto.</div>
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
                      <span className="text-xs px-2 py-1 rounded bg-blue-900/50 text-blue-300">
                        Cita
                      </span>
                    )}
                  </CardTitle>
                </CardHeader>
                <CardContent className="p-4">
                  <p className="text-sm text-zinc-300 line-clamp-3 mb-4">
                    {msg.message}
                  </p>
                  <p className="text-xs text-zinc-500">
                    Recibido: {new Date(msg.createdAt).toLocaleDateString()}
                  </p>
                  <Button 
                    variant="outline" 
                    className="w-full mt-4 border-zinc-700 text-zinc-300 hover:bg-zinc-800"
                    onClick={() => navigate("/admin/messages/" + msg.id)}
                  >
                    Ver detalle
                  </Button>
                </CardContent>
              </Card>
            ))}
          </div>
        )}
      </main>
    </div>
  );
}
