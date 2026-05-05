import { useEffect, useState } from "react";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { Textarea } from "@/components/ui/textarea";
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import { defaultBusinessSettings } from "@/config/business-info";
import { businessSettingsService } from "@/services/business-settings";
import type { BusinessSettings } from "@/types";

export default function BusinessSettingsPage() {
  const [settings, setSettings] = useState<BusinessSettings>(defaultBusinessSettings);
  const [isLoading, setIsLoading] = useState(true);
  const [isSaving, setIsSaving] = useState(false);
  const [error, setError] = useState("");
  const [success, setSuccess] = useState("");

  useEffect(() => {
    document.title = "Configuración — Admin | Ink Studio";
    const loadSettings = async () => {
      try {
        const data = await businessSettingsService.getInternal();
        setSettings(data);
      } catch {
        setError("No se pudieron cargar los datos del negocio.");
      } finally {
        setIsLoading(false);
      }
    };
    loadSettings();
  }, []);

  const updateField = (field: keyof BusinessSettings, value: string) => {
    setSettings(prev => ({ ...prev, [field]: value }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError("");
    setSuccess("");
    setIsSaving(true);

    try {
      await businessSettingsService.updateInternal(settings);
      setSuccess("Información del negocio actualizada correctamente.");
    } catch (err) {
      setError(err instanceof Error ? err.message : "No se pudieron guardar los cambios.");
    } finally {
      setIsSaving(false);
    }
  };

  if (isLoading) {
    return <div className="min-h-screen bg-zinc-950 text-white p-6">Cargando configuración...</div>;
  }

  return (
    <div>
      <div className="max-w-4xl">
        <div className="mb-8">
          <h1 className="text-2xl font-bold tracking-tight">Configuración del Negocio</h1>
          <p className="text-zinc-400 text-sm mt-1">Edita la información pública del estudio</p>
        </div>

        <Card className="bg-zinc-900 border-zinc-800">
          <CardHeader>
            <CardTitle className="text-white">Datos públicos del estudio</CardTitle>
          </CardHeader>
          <CardContent>
            <form className="space-y-5" onSubmit={handleSubmit}>
              {error && (
                <div className="p-3 text-sm bg-red-900/40 border border-red-500/50 rounded text-red-100">{error}</div>
              )}
              {success && (
                <div className="p-3 text-sm bg-green-900/30 border border-green-500/50 rounded text-green-100">{success}</div>
              )}

              <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                <div className="space-y-2">
                  <Label htmlFor="businessName">Nombre del negocio</Label>
                  <Input
                    id="businessName"
                    value={settings.businessName}
                    onChange={(e) => updateField("businessName", e.target.value)}
                    className="bg-zinc-800 border-zinc-700 text-white"
                    required
                  />
                </div>
                <div className="space-y-2">
                  <Label htmlFor="businessTagline">Tagline</Label>
                  <Input
                    id="businessTagline"
                    value={settings.businessTagline}
                    onChange={(e) => updateField("businessTagline", e.target.value)}
                    className="bg-zinc-800 border-zinc-700 text-white"
                    required
                  />
                </div>
              </div>

              <div className="space-y-2">
                <Label htmlFor="businessDescription">Descripción</Label>
                <Textarea
                  id="businessDescription"
                  value={settings.businessDescription}
                  onChange={(e) => updateField("businessDescription", e.target.value)}
                  className="bg-zinc-800 border-zinc-700 text-white"
                  rows={4}
                  required
                />
              </div>

              <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                <div className="space-y-2">
                  <Label htmlFor="phoneNumber">Teléfono</Label>
                  <Input
                    id="phoneNumber"
                    value={settings.phoneNumber}
                    onChange={(e) => updateField("phoneNumber", e.target.value)}
                    className="bg-zinc-800 border-zinc-700 text-white"
                    required
                  />
                </div>
                <div className="space-y-2">
                  <Label htmlFor="emailAddress">Email</Label>
                  <Input
                    id="emailAddress"
                    type="email"
                    value={settings.emailAddress}
                    onChange={(e) => updateField("emailAddress", e.target.value)}
                    className="bg-zinc-800 border-zinc-700 text-white"
                    required
                  />
                </div>
              </div>

              <div className="space-y-2">
                <Label htmlFor="address">Dirección</Label>
                <Input
                  id="address"
                  value={settings.address}
                  onChange={(e) => updateField("address", e.target.value)}
                  className="bg-zinc-800 border-zinc-700 text-white"
                  required
                />
              </div>

              <div className="space-y-2">
                <Label htmlFor="schedule">Horario</Label>
                <Input
                  id="schedule"
                  value={settings.schedule}
                  onChange={(e) => updateField("schedule", e.target.value)}
                  className="bg-zinc-800 border-zinc-700 text-white"
                  required
                />
              </div>

              <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
                <div className="space-y-2">
                  <Label htmlFor="instagramUrl">Instagram</Label>
                  <Input
                    id="instagramUrl"
                    value={settings.instagramUrl ?? ""}
                    onChange={(e) => updateField("instagramUrl", e.target.value)}
                    className="bg-zinc-800 border-zinc-700 text-white"
                  />
                </div>
                <div className="space-y-2">
                  <Label htmlFor="facebookUrl">Facebook</Label>
                  <Input
                    id="facebookUrl"
                    value={settings.facebookUrl ?? ""}
                    onChange={(e) => updateField("facebookUrl", e.target.value)}
                    className="bg-zinc-800 border-zinc-700 text-white"
                  />
                </div>
                <div className="space-y-2">
                  <Label htmlFor="twitterUrl">Twitter/X</Label>
                  <Input
                    id="twitterUrl"
                    value={settings.twitterUrl ?? ""}
                    onChange={(e) => updateField("twitterUrl", e.target.value)}
                    className="bg-zinc-800 border-zinc-700 text-white"
                  />
                </div>
              </div>

              <Button type="submit" className="bg-zinc-100 text-zinc-950 hover:bg-zinc-300" disabled={isSaving}>
                {isSaving ? "Guardando..." : "Guardar cambios"}
              </Button>
            </form>
          </CardContent>
        </Card>
      </div>
    </div>
  );
}
