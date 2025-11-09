import { useState, useEffect } from "react";
import { Menu, X } from "lucide-react";
import { Button } from "../ui/button";
import { menuItems, navbarCtaText } from "@/config/navigation";
import { businessInfo } from "@/config/business-info";
import { useActiveSection } from "@/hooks/use-active-section";
import { cn } from "@/lib/utils";

export function Navbar() {
  const [isOpen, setIsOpen] = useState(false);
  const sectionIds = menuItems.map(item => item.href.replace('#', ''));
  const activeSection = useActiveSection(sectionIds);

  // Smooth scroll al hacer clic en enlaces
  const handleClick = (e: React.MouseEvent<HTMLAnchorElement>, href: string) => {
    e.preventDefault();
    const element = document.querySelector(href);
    if (element) {
      element.scrollIntoView({ behavior: 'smooth', block: 'start' });
      setIsOpen(false);
    }
  };

  const [scrolled, setScrolled] = useState(false);

  useEffect(() => {
    const handleScroll = () => {
      const isScrolled = window.scrollY > 10;
      if (isScrolled !== scrolled) {
        setScrolled(isScrolled);
      }
    };

    window.addEventListener('scroll', handleScroll);
    return () => window.removeEventListener('scroll', handleScroll);
  }, [scrolled]);

  return (
    <nav 
      className={cn(
        "fixed top-0 left-0 right-0 z-50 transition-all duration-300 border-b",
        scrolled 
          ? "bg-black/95 backdrop-blur-md border-white/10" 
          : "bg-black/80 backdrop-blur-sm border-transparent"
      )}
    >
      <div className="max-w-7xl mx-auto px-4">
        <div className="flex items-center justify-between h-16">
          <div className="text-white">{businessInfo.name}</div>

          {/* Desktop Menu */}
          <div className="hidden md:flex items-center gap-8">
            {menuItems.map((item, index) => {
              const isActive = activeSection === item.href.replace('#', '');
              return (
                <a
                  key={index}
                  href={item.href}
                  onClick={(e) => handleClick(e, item.href)}
                  className={`transition-colors ${
                    isActive
                      ? 'text-white font-semibold border-b-2 border-white'
                      : 'text-gray-300 hover:text-white'
                  }`}
                >
                  {item.label}
                </a>
              );
            })}
            <Button size="sm" className="bg-white text-black hover:bg-gray-200">
              {navbarCtaText}
            </Button>
          </div>

          {/* Mobile Menu Button */}
          <button
            className="md:hidden text-white"
            onClick={() => setIsOpen(!isOpen)}
          >
            {isOpen ? <X className="w-6 h-6" /> : <Menu className="w-6 h-6" />}
          </button>
        </div>

        {/* Mobile Menu */}
        <div 
          className={cn(
            "md:hidden overflow-hidden transition-all duration-300 ease-in-out",
            isOpen ? "max-h-96 py-4 border-t border-white/10" : "max-h-0"
          )}
        >
          <div className="md:hidden py-4 border-t border-white/10">
            <div className="flex flex-col gap-4">
              {menuItems.map((item, index) => {
                const isActive = activeSection === item.href.replace('#', '');
                return (
                  <a
                    key={index}
                    href={item.href}
                    onClick={(e) => handleClick(e, item.href)}
                    className={`transition-colors ${
                      isActive
                        ? 'text-white font-semibold'
                        : 'text-gray-300 hover:text-white'
                    }`}
                  >
                    {item.label}
                  </a>
                );
              })}
              <Button size="sm" className="bg-white text-black hover:bg-gray-200 w-full">
                {navbarCtaText}
              </Button>
            </div>
        </div>
      </div>
    </nav>
  );
}
