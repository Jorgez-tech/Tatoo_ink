import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "../ui/card";
import { services } from "@/config/services";
import { servicesContent } from "@/config/content";

export function Services() {
  return (
    <section className="py-20 px-4 bg-gray-50">
      <div className="max-w-7xl mx-auto">
        <div className="text-center mb-16">
          <h2 className="mb-4">{servicesContent.title}</h2>
          <p className="text-gray-600 max-w-2xl mx-auto">
            {servicesContent.description}
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
