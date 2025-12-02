/**
 * Configuración de API para integración con backend ASP.NET Core
 */

/**
 * URL base de la API
 * Desarrollo: http://localhost:5000 o https://localhost:7001
 * Producción: configurar según el deployment
 */
export const API_BASE_URL = import.meta.env.VITE_API_BASE_URL || 'http://localhost:5177';

/**
 * Endpoints de la API
 */
export const API_ENDPOINTS = {
  contact: '/api/contact',
  gallery: '/api/gallery',
} as const;

/**
 * Función helper para construir URLs completas de la API
 */
export const getApiUrl = (endpoint: keyof typeof API_ENDPOINTS): string => {
  return `${API_BASE_URL}${API_ENDPOINTS[endpoint]}`;
};

