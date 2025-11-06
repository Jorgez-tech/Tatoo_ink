import { ImageWithFallback } from "../ui/ImageWithFallback";
import { Award, Clock, Heart } from "lucide-react";

const stats = [
  { icon: Award, value: "10+", label: "Años de Experiencia" },
  { icon: Clock, value: "5000+", label: "Tatuajes Realizados" },
  { icon: Heart, value: "100%", label: "Clientes Satisfechos" }
];

export function About() {
  return (
    <section className="py-20 px-4 bg-gray-50">
      <div className="max-w-7xl mx-auto">
        <div className="grid grid-cols-1 lg:grid-cols-2 gap-12 items-center">
          <div>
            <h2 className="mb-6">Sobre Nosotros</h2>
            <p className="text-gray-600 mb-6">
              Somos un estudio de tatuajes dedicado a crear arte corporal excepcional. Nuestro equipo de artistas altamente capacitados trabaja en un ambiente profesional, limpio y acogedor.
            </p>
            <p className="text-gray-600 mb-8">
              Cada tatuaje es una colaboración entre el artista y el cliente. Nos tomamos el tiempo para entender tu visión y crear un diseño que supere tus expectativas. Utilizamos únicamente equipos esterilizados y tintas de la más alta calidad para garantizar tu seguridad y satisfacción.
            </p>

            <div className="grid grid-cols-3 gap-4">
              {stats.map((stat, index) => {
                const Icon = stat.icon;
                return (
                  <div key={index} className="text-center">
                    <div className="w-12 h-12 bg-black text-white rounded-full flex items-center justify-center mx-auto mb-2">
                      <Icon className="w-6 h-6" />
                    </div>
                    <div className="mb-1">{stat.value}</div>
                    <p className="text-gray-600">{stat.label}</p>
                  </div>
                );
              })}
            </div>
          </div>

          <div className="relative">
            <div className="aspect-square rounded-lg overflow-hidden">
              <ImageWithFallback
                src="https://images.unsplash.com/photo-1760877611905-0f885a3ce551?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&ixid=M3w3Nzg4Nzd8MHwxfHNlYXJjaHwxfHx0YXR0b28lMjBzdHVkaW8lMjBpbnRlcmlvcnxlbnwxfHx8fDE3NjIyNDQ1OTl8MA&ixlib=rb-4.1.0&q=80&w=1080&utm_source=figma&utm_medium=referral"
                alt="Tattoo Studio"
                className="w-full h-full object-cover"
              />
            </div>
            <div className="absolute -bottom-6 -right-6 w-48 h-48 bg-black rounded-lg -z-10"></div>
          </div>
        </div>
      </div>
    </section>
  );
}
