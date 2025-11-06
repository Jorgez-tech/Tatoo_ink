import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "../ui/card";
import { Palette, Sparkles, Users, Shield } from "lucide-react";

const services = [
  {
    icon: Palette,
    title: "Tatuajes Personalizados",
    description: "Diseños únicos creados especialmente para ti. Trabajamos contigo para dar vida a tu visión."
  },
  {
    icon: Sparkles,
    title: "Cover-Up",
    description: "Transformamos tatuajes antiguos en nuevas obras de arte que amarás."
  },
  {
    icon: Users,
    title: "Artistas Especializados",
    description: "Equipo de profesionales con estilos diversos: realismo, tradicional, minimalista y más."
  },
  {
    icon: Shield,
    title: "Seguridad e Higiene",
    description: "Máximos estándares de esterilización y materiales de primera calidad."
  }
];

export function Services() {
  return (
    <section className="py-20 px-4 bg-gray-50">
      <div className="max-w-7xl mx-auto">
        <div className="text-center mb-16">
          <h2 className="mb-4">Nuestros Servicios</h2>
          <p className="text-gray-600 max-w-2xl mx-auto">
            Ofrecemos una experiencia completa de tatuaje con los más altos estándares de calidad y profesionalismo.
          </p>
        </div>

        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6">
          {services.map((service, index) => {
            const Icon = service.icon;
            return (
              <Card key={index} className="border-2 hover:border-black transition-colors">
                <CardHeader>
                  <div className="w-12 h-12 bg-black text-white rounded-lg flex items-center justify-center mb-4">
                    <Icon className="w-6 h-6" />
                  </div>
                  <CardTitle>{service.title}</CardTitle>
                </CardHeader>
                <CardContent>
                  <CardDescription>{service.description}</CardDescription>
                </CardContent>
              </Card>
            );
          })}
        </div>
      </div>
    </section>
  );
}
