/**
 * Configuración de API
 * Preparado para integración con backend ASP.NET Core
 */

/**
 * URL base de la API
 * En desarrollo: usar variable de entorno VITE_API_BASE_URL
 * En producción: configurar según el deployment
 * 
 * Para desarrollo sin backend, dejar vacío para usar modo mock
 */
export const API_BASE_URL = import.meta.env.VITE_API_BASE_URL || '';

/**
 * Modo mock para desarrollo
 * Si no hay API_BASE_URL, se usa modo mock que simula la respuesta del servidor
 */
export const USE_MOCK_API = !API_BASE_URL || import.meta.env.VITE_USE_MOCK_API === 'true';

/**
 * Endpoints de la API
 */
export const API_ENDPOINTS = {
  contact: '/api/contact',
  // Agregar más endpoints según se necesiten
} as const;

/**
 * Función helper para construir URLs de la API
 */
export const getApiUrl = (endpoint: keyof typeof API_ENDPOINTS): string => {
  return `${API_BASE_URL}${API_ENDPOINTS[endpoint]}`;
};

/**
 * Función mock para simular respuesta del servidor en desarrollo
 */
export const mockApiCall = async (
  _endpoint: keyof typeof API_ENDPOINTS,
  _data: unknown
): Promise<Response> => {
  // Simular delay de red
  await new Promise((resolve) => setTimeout(resolve, 1000));

  // Simular respuesta exitosa
  return new Response(JSON.stringify({ success: true, message: "Mensaje recibido correctamente" }), {
    status: 200,
    headers: { "Content-Type": "application/json" },
  });
};

