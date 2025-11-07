import { ImageWithFallback } from "../ui/ImageWithFallback";
import { businessInfo } from "@/config/business-info";
import { aboutContent } from "@/config/content";
import { aboutImage } from "@/config/images";

export function About() {
  return (
    <section className="py-20 px-4 bg-gray-50">
      <div className="max-w-7xl mx-auto">
        <div className="grid grid-cols-1 lg:grid-cols-2 gap-12 items-center">
          <div>
            <h2 className="mb-6">{aboutContent.title}</h2>
            {aboutContent.paragraphs.map((paragraph, index) => (
              <p key={index} className={`text-gray-600 ${index === aboutContent.paragraphs.length - 1 ? 'mb-8' : 'mb-6'}`}>
                {paragraph}
              </p>
            ))}

            <div className="grid grid-cols-3 gap-4">
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

          <div className="relative">
            <div className="aspect-square rounded-lg overflow-hidden">
              <ImageWithFallback
                src={aboutImage.src}
                alt={aboutImage.alt}
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
