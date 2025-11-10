import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "../ui/card";
import { services } from "@/config/services";
import { servicesContent } from "@/config/content";

/**
 * Sección que lista los servicios destacados del estudio con iconografía.
 *
 * Itera sobre la configuración de servicios para renderizar tarjetas uniformes
 * con título, descripción y un ícono representativo dentro de una grilla
 * responsive.
 *
 * @component
 * @example
 * return (
 *   <Services />
 * );
 */
export function Services() {
  return (
    <section className="py-12 sm:py-16 md:py-20 px-4 bg-gray-50">
      <div className="max-w-7xl mx-auto">
        <div className="text-center mb-12 sm:mb-16">
          <h2 className="text-3xl sm:text-4xl md:text-5xl font-bold mb-3 sm:mb-4 animate-fade-in-up">{servicesContent.title}</h2>
          <p className="text-sm sm:text-base md:text-lg text-gray-600 max-w-2xl mx-auto animate-fade-in-up animation-delay-100">
            {servicesContent.description}
          </p>
        </div>

        <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-4 sm:gap-6">
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
