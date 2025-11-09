import { Instagram, Facebook, Twitter } from "lucide-react";
import { businessInfo } from "@/config/business-info";
import { footerContent } from "@/config/content";
import { menuItems } from "@/config/navigation";

export function Footer() {
  const currentYear = new Date().getFullYear();

  return (
    <footer className="bg-black text-white py-8 sm:py-10 md:py-12 px-4">
      <div className="max-w-7xl mx-auto">
        <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-6 sm:gap-8 mb-6 sm:mb-8">
          <div>
            <h3 className="text-xl sm:text-2xl font-bold mb-3 sm:mb-4">{businessInfo.name}</h3>
            <p className="text-sm sm:text-base text-gray-400">
              {footerContent.description}
            </p>
          </div>

          <div>
            <h4 className="text-lg sm:text-xl font-semibold mb-3 sm:mb-4">{footerContent.quickLinksTitle}</h4>
            <ul className="space-y-2">
              {menuItems.filter(item => item.href !== "#").map((item, index) => (
                <li key={index}>
                  <a href={item.href} className="text-gray-400 hover:text-white transition-colors">
                    {item.label}
                  </a>
                </li>
              ))}
            </ul>
          </div>

          <div>
            <h4 className="text-lg sm:text-xl font-semibold mb-3 sm:mb-4">{footerContent.socialTitle}</h4>
            <div className="flex gap-3 sm:gap-4">
              {businessInfo.social.instagram && (
                <a
                  href={businessInfo.social.instagram}
                  className="w-10 h-10 bg-white/10 rounded-full flex items-center justify-center hover:bg-white/20 transition-colors"
                  aria-label="Instagram"
                >
                  <Instagram className="w-5 h-5" />
                </a>
              )}
              {businessInfo.social.facebook && (
                <a
                  href={businessInfo.social.facebook}
                  className="w-10 h-10 bg-white/10 rounded-full flex items-center justify-center hover:bg-white/20 transition-colors"
                  aria-label="Facebook"
                >
                  <Facebook className="w-5 h-5" />
                </a>
              )}
              {businessInfo.social.twitter && (
                <a
                  href={businessInfo.social.twitter}
                  className="w-10 h-10 bg-white/10 rounded-full flex items-center justify-center hover:bg-white/20 transition-colors"
                  aria-label="Twitter"
                >
                  <Twitter className="w-5 h-5" />
                </a>
              )}
            </div>
          </div>
        </div>

        <div className="border-t border-gray-800 pt-6 sm:pt-8 text-center text-gray-400">
          <p className="text-sm sm:text-base">&copy; {currentYear} {businessInfo.name}. Todos los derechos reservados.</p>
        </div>
      </div>
    </footer>
  );
}
