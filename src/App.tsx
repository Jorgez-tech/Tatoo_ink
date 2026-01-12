import { Navbar } from "./components/layout/Navbar";
import { Hero } from "./components/sections/Hero";
import { Services } from "./components/sections/Services";
import { Gallery } from "./components/sections/Gallery";
import { About } from "./components/sections/About";
import { Contact } from "./components/sections/Contact";
import { Footer } from "./components/layout/Footer";
import { LazyLoad } from "./components/ui/LazyLoad";

export default function App() {
  return (
    <div className="min-h-screen">
      <Navbar />
      <Hero />
      <div id="servicios">
        <Services />
      </div>
      <div id="galeria">
        {/* Lazy load Gallery to delay API call and heavy image rendering until in view */}
        <LazyLoad minHeight="500px">
          <Gallery />
        </LazyLoad>
      </div>
      <div id="nosotros">
        <About />
      </div>
      <div id="contacto">
        {/* Contact section is at the bottom, safe to lazy load */}
        <LazyLoad minHeight="400px">
          <Contact />
        </LazyLoad>
      </div>
      <Footer />
    </div>
  );
}
