# Estado Actual del Proyecto (Save State)

> **Fase:** Bloque 1 - Autenticaciï¿½n Backend
> **Rama Actual:** feat/backend-auth

## Estrategia de Autenticaciï¿½n y Manejo de Errores (Aprobada)

Se ha estandarizado el enfoque tï¿½cnico para iniciar el desarrollo del Backend sin afectar implementaciones anteriores. La premisa es mantener separaciï¿½n de responsabilidades, sin sobreingenierï¿½a.

### 1. Validaciones y Excepciones Globales
- **Formato Estï¿½ndar:** Implementar respuestas bajo el estï¿½ndar `ProblemDetails` (RFC 7807) para evitar roturas en el panel frontend.
- **Validaciï¿½n Limpia (DTOs):** Las reglas de formato en los DTOs rebotarï¿½n automï¿½ticamente a `400 Bad Request` o `422 Unprocessable Entity`.
- **Middleware Central:** Implementar un `GlobalExceptionHandlerMiddleware` en ASP.NET Core. Este atraparï¿½ todas las excepciones (`UnauthorizedException`, `NotFoundException`) de negocio y no controladas. 
- **Regla en Controladores:** Queda prohibido el uso de bloques `try-catch` redundantes en los Controllers.

### 2. Algoritmo del Flujo de Login y Seed (BE-01, BE-02, BE-03)
1. **Punto de Entrada (Seed):** Al inicializar la app, verificar en BD (`SQLite`) si existe el usuario Admin. Si no, insertarlo con un PassHash por defecto.
2. **Login Request:** El endpoint recibe DTO con `email` y `password`. 
3. **Flujo de Servicio:** 
   - Localizar el usuario en la BD.
   - Comparar PassHash almacenado. En caso de fallo, lanzar excepciï¿½n de negocio -> El Middleware lo capturarï¿½ lanzando un `401`.
   - Generar el Token JWT firmado e inyectar el ROL en los claims.
4. **Respuesta:** Retornar token al cliente.

---

## Siguientes Pasos (Next Execution)

> **Instrucciï¿½n para el Modelo:** En la prï¿½xima instrucciï¿½n, retomar el trabajo directamente desde esta lista de tareas verificando los *checkboxes*.

- [x] Tarea 1: Generar el cï¿½digo C# del `GlobalExceptionHandlerMiddleware` y la estructura de Respuesta de Errores (`ProblemDetails`).
- [x] Tarea 2: Implementar la entidad `User`, la configuraciï¿½n de Entity Framework y el script/seed inicial (BE-01, BE-02).
- [x] Tarea 3: Generar el DTO y Controlador de Login con generaciï¿½n estructural de Token JWT (BE-03).
- [x] Tarea 4: Implementar CRUD de GalerÃ­a y protecciÃ³n de endpoints (BE-05 a BE-11).
- [x] Tarea 5: Configurar rutas del Panel Admin en Frontend e implementar Login (FE-01 a FE-04).

- [x] Tarea 6: Construir UI Administrativa del Dashboard de la Galería (FE-05 a FE-08) y soportar cookies HttpOnly de JWT.
- [x] Tarea 7: Validación de Backwards-Compatibility y Fixes Menores de Frontend.
- [x] Tarea 8: Implementar UI de Bandeja de Mensajes de Contacto (FE-12, FE-13)
- [x] Tarea 9: Agregar pruebas de flujo para login y CRUD en panel (FE-14)
