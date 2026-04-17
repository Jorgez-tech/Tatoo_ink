# Estado Actual del Proyecto (Save State)

> **Fase:** Bloque 1 - Autenticación Backend
> **Rama Actual:** feat/backend-auth

## Estrategia de Autenticación y Manejo de Errores (Aprobada)

Se ha estandarizado el enfoque técnico para iniciar el desarrollo del Backend sin afectar implementaciones anteriores. La premisa es mantener separación de responsabilidades, sin sobreingeniería.

### 1. Validaciones y Excepciones Globales
- **Formato Estándar:** Implementar respuestas bajo el estándar `ProblemDetails` (RFC 7807) para evitar roturas en el panel frontend.
- **Validación Limpia (DTOs):** Las reglas de formato en los DTOs rebotarón automáticamente a `400 Bad Request` o `422 Unprocessable Entity`.
- **Middleware Central:** Implementar un `GlobalExceptionHandlerMiddleware` en ASP.NET Core. Este atrapará todas las excepciones (`UnauthorizedException`, `NotFoundException`) de negocio y no controladas. 
- **Regla en Controladores:** Queda prohibido el uso de bloques `try-catch` redundantes en los Controllers.

### 2. Algoritmo del Flujo de Login y Seed (BE-01, BE-02, BE-03)
1. **Punto de Entrada (Seed):** Al inicializar la app, verificar en BD (`SQLite`) si existe el usuario Admin. Si no, insertarlo con un PassHash por defecto.
2. **Login Request:** El endpoint recibe DTO con `email` y `password`. 
3. **Flujo de Servicio:** 
   - Localizar el usuario en la BD.
   - Comparar PassHash almacenado. En caso de fallo, lanzar excepción de negocio -> El Middleware lo capturará lanzando un `401`.
   - Generar el Token JWT firmado e inyectar el ROL en los claims.
4. **Respuesta:** Retornar token al cliente.

---

## Siguientes Pasos (Next Execution)

> **Instrucción para el Modelo:** En la próxima instrucción, retomar el trabajo directamente desde esta lista de tareas verificando los *checkboxes*.

- [x] Tarea 1: Generar el código C# del `GlobalExceptionHandlerMiddleware` y la estructura de Respuesta de Errores (`ProblemDetails`).
- [x] Tarea 2: Implementar la entidad `User`, la configuración de Entity Framework y el script/seed inicial (BE-01, BE-02).
- [x] Tarea 3: Generar el DTO y Controlador de Login con generación estructural de Token JWT (BE-03).
- [x] Tarea 4: Implementar CRUD de Galería y protección de endpoints (BE-05 a BE-11).
- [x] Tarea 5: Configurar rutas del Panel Admin en Frontend e implementar Login (FE-01 a FE-04).
