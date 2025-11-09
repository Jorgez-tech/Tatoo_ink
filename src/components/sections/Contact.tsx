import { Button } from "../ui/button";
import { Input } from "../ui/input";
import { Textarea } from "../ui/textarea";
import { Label } from "../ui/label";
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "../ui/card";
import { useState } from "react";
import { useForm } from "react-hook-form";
import { contactInfo } from "@/config/business-info";
import { contactContent } from "@/config/content";
import type { ContactFormData } from "@/types";

type FormStatus = "idle" | "loading" | "success" | "error";

export function Contact() {
  const [status, setStatus] = useState<FormStatus>("idle");
  const [errorMessage, setErrorMessage] = useState<string>("");

  const {
    register,
    handleSubmit,
    formState: { errors },
    reset,
  } = useForm<ContactFormData>({
    defaultValues: {
      name: "",
      email: "",
      phone: "",
      message: "",
    },
  });

  const onSubmit = async (data: ContactFormData) => {
    setStatus("loading");
    setErrorMessage("");

    try {
      // Importar funciones de API
      const { USE_MOCK_API, mockApiCall, getApiUrl } = await import("@/config/api");

      let response: Response;

      if (USE_MOCK_API) {
        // Modo mock para desarrollo sin backend
        response = await mockApiCall("contact", data);
      } else {
        // Llamada real al backend ASP.NET Core
        // El backend esperará un POST a /api/contact con el siguiente formato:
        response = await fetch(getApiUrl("contact"), {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(data),
        });
      }

      if (!response.ok) {
        throw new Error(`Error ${response.status}: ${response.statusText}`);
      }

      setStatus("success");
      reset();
      
      // Opcional: mostrar mensaje de éxito por unos segundos
      setTimeout(() => {
        setStatus("idle");
      }, 3000);
    } catch (error) {
      setStatus("error");
      setErrorMessage(
        error instanceof Error
          ? error.message
          : "Error al enviar el mensaje. Por favor, intenta nuevamente."
      );
    }
  };

  return (
    <section className="py-12 sm:py-16 md:py-20 px-4 bg-white">
      <div className="max-w-7xl mx-auto">
        <div className="text-center mb-12 sm:mb-16">
          <h2 className="text-3xl sm:text-4xl md:text-5xl font-bold mb-3 sm:mb-4 animate-fade-in-up">{contactContent.title}</h2>
          <p className="text-sm sm:text-base md:text-lg text-gray-600 max-w-2xl mx-auto animate-fade-in-up animation-delay-100">
            {contactContent.description}
          </p>
        </div>

        <div className="grid grid-cols-1 lg:grid-cols-3 gap-6 sm:gap-8">
          <div className="lg:col-span-2">
            <Card>
              <CardHeader>
                <CardTitle>{contactContent.formTitle}</CardTitle>
                <CardDescription>
                  {contactContent.formDescription}
                </CardDescription>
              </CardHeader>
              <CardContent>
                <form onSubmit={handleSubmit(onSubmit)} className="space-y-6">
                  {/* Mensaje de éxito */}
                  {status === "success" && (
                    <div className="p-4 bg-green-50 border border-green-200 rounded-lg text-green-800">
                      {contactContent.successMessage}
                    </div>
                  )}

                  {/* Mensaje de error */}
                  {status === "error" && (
                    <div className="p-4 bg-red-50 border border-red-200 rounded-lg text-red-800">
                      {errorMessage || "Error al enviar el mensaje. Por favor, intenta nuevamente."}
                    </div>
                  )}

                  <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                    <div className="space-y-2">
                      <Label htmlFor="name">{contactContent.formFields.name.label}</Label>
                      <Input
                        id="name"
                        placeholder={contactContent.formFields.name.placeholder}
                        {...register("name", {
                          required: "El nombre es obligatorio",
                          minLength: {
                            value: 2,
                            message: "El nombre debe tener al menos 2 caracteres",
                          },
                        })}
                        aria-invalid={errors.name ? "true" : "false"}
                      />
                      {errors.name && (
                        <p className="text-sm text-red-600" role="alert">
                          {errors.name.message}
                        </p>
                      )}
                    </div>
                    <div className="space-y-2">
                      <Label htmlFor="email">{contactContent.formFields.email.label}</Label>
                      <Input
                        id="email"
                        type="email"
                        placeholder={contactContent.formFields.email.placeholder}
                        {...register("email", {
                          required: "El email es obligatorio",
                          pattern: {
                            value: /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i,
                            message: "Email inválido",
                          },
                        })}
                        aria-invalid={errors.email ? "true" : "false"}
                      />
                      {errors.email && (
                        <p className="text-sm text-red-600" role="alert">
                          {errors.email.message}
                        </p>
                      )}
                    </div>
                  </div>

                  <div className="space-y-2">
                    <Label htmlFor="phone">{contactContent.formFields.phone.label}</Label>
                    <Input
                      id="phone"
                      type="tel"
                      placeholder={contactContent.formFields.phone.placeholder}
                      {...register("phone", {
                        pattern: {
                          value: /^[\d\s\-+()]+$/,
                          message: "Formato de teléfono inválido",
                        },
                      })}
                      aria-invalid={errors.phone ? "true" : "false"}
                    />
                    {errors.phone && (
                      <p className="text-sm text-red-600" role="alert">
                        {errors.phone.message}
                      </p>
                    )}
                  </div>

                  <div className="space-y-2">
                    <Label htmlFor="message">{contactContent.formFields.message.label}</Label>
                    <Textarea
                      id="message"
                      placeholder={contactContent.formFields.message.placeholder}
                      rows={5}
                      {...register("message", {
                        required: "El mensaje es obligatorio",
                        minLength: {
                          value: 10,
                          message: "El mensaje debe tener al menos 10 caracteres",
                        },
                      })}
                      aria-invalid={errors.message ? "true" : "false"}
                    />
                    {errors.message && (
                      <p className="text-sm text-red-600" role="alert">
                        {errors.message.message}
                      </p>
                    )}
                  </div>

                  <Button
                    type="submit"
                    className="w-full"
                    disabled={status === "loading"}
                  >
                    {status === "loading" ? "Enviando..." : contactContent.submitButton}
                  </Button>
                </form>
              </CardContent>
            </Card>
          </div>

          <div className="space-y-4">
            {contactInfo.map((info, index) => {
              const Icon = info.icon;
              return (
                <Card key={index}>
                  <CardContent className="pt-6">
                    <div className="flex items-start gap-4">
                      <div className="w-10 h-10 bg-black text-white rounded-lg flex items-center justify-center flex-shrink-0">
                        <Icon className="w-5 h-5" />
                      </div>
                      <div>
                        <p className="text-gray-600 mb-1">{info.title}</p>
                        <p>{info.value}</p>
                      </div>
                    </div>
                  </CardContent>
                </Card>
              );
            })}
          </div>
        </div>
      </div>
    </section>
  );
}
