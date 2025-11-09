import { ImageWithFallback } from "../ui/ImageWithFallback";
import { businessInfo } from "@/config/business-info";
import { aboutContent } from "@/config/content";
import { aboutImage } from "@/config/images";

export function About() {
  return (
    <section className="py-12 sm:py-16 md:py-20 px-4 bg-gray-50">
      <div className="max-w-7xl mx-auto">
        <div className="grid grid-cols-1 lg:grid-cols-2 gap-8 sm:gap-10 lg:gap-12 items-center">
          <div className="order-2 lg:order-1">
            <h2 className="text-3xl sm:text-4xl md:text-5xl font-bold mb-4 sm:mb-6 animate-fade-in-up">{aboutContent.title}</h2>
            {aboutContent.paragraphs.map((paragraph, index) => (
              <p key={index} className={`text-sm sm:text-base md:text-lg text-gray-600 ${index === aboutContent.paragraphs.length - 1 ? 'mb-6 sm:mb-8' : 'mb-4 sm:mb-6'} animate-fade-in-up animation-delay-${(index + 1) * 100}`}>
                {paragraph}
              </p>
            ))}

            <div className="grid grid-cols-3 gap-3 sm:gap-4">
              {businessInfo.stats.map((stat, index) => {
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

          <div className="relative order-1 lg:order-2">
            <div className="aspect-square rounded-lg overflow-hidden">
              <ImageWithFallback
                src={aboutImage.src}
                alt={aboutImage.alt}
                className="w-full h-full object-cover"
              />
            </div>
            <div className="hidden sm:block absolute -bottom-4 sm:-bottom-6 -right-4 sm:-right-6 w-32 h-32 sm:w-48 sm:h-48 bg-black rounded-lg -z-10"></div>
          </div>
        </div>
      </div>
    </section>
  );
}
