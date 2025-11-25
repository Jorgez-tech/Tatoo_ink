# Implementation Plan

- [x] 1. Configurar proyecto ASP.NET Core Web API

  - Crear nuevo proyecto con .NET 8.0
  - Configurar estructura de carpetas (Controllers, Services, Models, Data)
  - Instalar paquetes NuGet: Entity Framework Core, FluentValidation, Serilog, SendGrid (o System.Net.Mail)
  - Configurar appsettings.json con secciones para ConnectionStrings, EmailSettings, CorsSettings
  - _Requirements: 7.1, 7.2, 7.3, 7.4_

- [x] 2. Implementar modelos de datos y DTOs

  - Crear entidad ContactMessage con todas las propiedades (Id, Name, Email, Phone, Message, WantsAppointment, CreatedAt, EmailSent, EmailSentAt)
  - Crear ContactRequestDto para recibir datos del frontend
  - Crear ContactResponseDto para respuestas al cliente
  - Crear ServiceResult para comunicación entre capas
  - _Requirements: 1.1, 2.1, 2.2, 3.2, 3.3_

- [x] 3. Configurar Entity Framework Core y base de datos

  - Crear ApplicationDbContext con DbSet<ContactMessage>
  - Configurar entidad ContactMessage con Fluent API (longitudes máximas, índices, valores por defecto)
  - Configurar cadena de conexión desde IConfiguration
  - Crear migración inicial
  - _Requirements: 3.1, 3.2, 3.3, 7.1_

- [ ]\* 3.1 Escribir prueba de propiedad para generación de identificadores únicos

  - **Property 7: Unique identifier generation**
  - **Validates: Requirements 3.2**

- [ ]\* 3.2 Escribir prueba de propiedad para registro de timestamp

  - **Property 8: Timestamp recording**
  - **Validates: Requirements 3.3**

- [x] 4. Implementar validación con FluentValidation

  - Crear ContactRequestValidator con reglas para todos los campos
  - Validar Name: NotEmpty, MaxLength(100)
  - Validar Email: NotEmpty, EmailAddress (RFC 5322)
  - Validar Phone: NotEmpty, MaxLength(20)
  - Validar Message: NotEmpty, MaxLength(1000)
  - Validar WantsAppointment: NotNull
  - Registrar validador en Program.cs
  - _Requirements: 1.1, 1.3, 1.4, 1.5_

- [ ]\* 4.1 Escribir prueba de propiedad para aceptación de entrada válida

  - **Property 1: Valid input acceptance**
  - **Validates: Requirements 1.1, 1.2, 1.4**

- [ ]\* 4.2 Escribir prueba de propiedad para validación de formato de email

  - **Property 2: Email format validation**
  - **Validates: Requirements 1.3**

- [ ]\* 4.3 Escribir prueba de propiedad para rechazo de entrada inválida

  - **Property 3: Invalid input rejection**
  - **Validates: Requirements 1.5, 8.1**

- [x] 5. Implementar servicio de correo electrónico

  - Crear interfaz IEmailService con método SendContactNotificationAsync
  - Implementar SendGridEmailService usando SendGrid SDK
  - Implementar SmtpEmailService usando System.Net.Mail como alternativa
  - Crear plantilla HTML para el correo con todos los campos del mensaje
  - Incluir indicador visual para solicitudes de cita
  - Leer configuración de correo desde IConfiguration (credenciales, email del estudio)
  - Implementar logging de errores de envío
  - Registrar servicio en Program.cs según configuración
  - _Requirements: 4.1, 4.2, 4.3, 4.4, 7.2, 7.3_

- [ ]\* 5.1 Escribir prueba de propiedad para contenido completo del email

  - **Property 10: Complete email content**
  - **Validates: Requirements 2.4, 4.2**

- [ ]\* 5.2 Escribir pruebas unitarias para servicio de email

  - Probar formato de email con diferentes datos de entrada
  - Probar manejo de errores de envío
  - Probar configuración de destinatario

- [x] 6. Implementar capa de lógica de negocio (ContactService)

  - Crear interfaz IContactService con método ProcessContactMessageAsync
  - Implementar ContactService con inyección de ApplicationDbContext e IEmailService
  - Implementar flujo: validar → mapear DTO a entidad → persistir → enviar email
  - Manejar errores de persistencia (retornar error, no enviar email)
  - Manejar errores de email (registrar error, retornar éxito)
  - Implementar logging en cada paso del proceso
  - Registrar servicio en Program.cs
  - _Requirements: 2.3, 3.1, 3.4, 3.5, 4.1, 4.4, 4.5, 5.2_

- [ ]\* 6.1 Escribir prueba de propiedad para preservación del flag de cita

  - **Property 4: Appointment flag preservation**
  - **Validates: Requirements 2.1, 2.2**

- [ ]\* 6.2 Escribir prueba de propiedad para independencia de procesamiento

  - **Property 5: Processing independence from appointment flag**
  - **Validates: Requirements 2.3**

- [ ]\* 6.3 Escribir prueba de propiedad para persistencia antes de notificación

  - **Property 6: Persistence before notification**
  - **Validates: Requirements 3.1, 3.5, 4.1**

- [ ]\* 6.4 Escribir prueba de propiedad para manejo de fallo de persistencia

  - **Property 9: Persistence failure handling**
  - **Validates: Requirements 3.4**

- [ ]\* 6.5 Escribir prueba de propiedad para resiliencia ante fallo de email

  - **Property 11: Email failure resilience**
  - **Validates: Requirements 4.4, 8.3**

- [ ]\* 6.6 Escribir prueba de propiedad para formato de respuesta exitosa

  - **Property 12: Success response format**
  - **Validates: Requirements 4.5**

- [ ]\* 6.7 Escribir pruebas unitarias para ContactService

  - Probar flujo completo con mocks de DbContext y EmailService
  - Probar manejo de excepciones de base de datos
  - Probar manejo de excepciones de email
  - Probar mapeo correcto de DTO a entidad

- [x] 7. Implementar controlador API (ContactController)

  - Crear ContactController con inyección de IContactService
  - Implementar endpoint POST /api/contact
  - Validar modelo con ModelState (FluentValidation automático)
  - Retornar 400 con errores de validación si ModelState es inválido
  - Invocar ContactService.ProcessContactMessageAsync
  - Retornar 200 con ContactResponseDto en caso de éxito
  - Retornar 500 con mensaje genérico en caso de error de servicio
  - Implementar logging de solicitudes
  - _Requirements: 1.5, 4.5, 5.1, 8.1_

- [ ]\* 7.1 Escribir pruebas unitarias para ContactController

  - Probar respuesta 200 con entrada válida
  - Probar respuesta 400 con entrada inválida
  - Probar respuesta 500 cuando servicio falla
  - Probar que se invoca el servicio correctamente

- [ ] 8. Implementar middleware de manejo global de excepciones

  - Crear GlobalExceptionMiddleware
  - Capturar todas las excepciones no controladas
  - Registrar error completo con stack trace y contexto
  - Retornar respuesta HTTP 500 con mensaje genérico
  - Asegurar que no se expongan detalles internos al cliente
  - Registrar middleware en Program.cs
  - _Requirements: 8.2, 8.4, 8.5_

- [ ]\* 8.1 Escribir prueba de propiedad para formato de respuesta de error de base de datos

  - **Property 18: Database error response format**
  - **Validates: Requirements 8.2**

- [ ]\* 8.2 Escribir prueba de propiedad para completitud de logging de errores

  - **Property 19: Error logging completeness**
  - **Validates: Requirements 8.5**

- [ ] 9. Configurar logging con Serilog

  - Instalar paquetes Serilog.AspNetCore, Serilog.Sinks.Console, Serilog.Sinks.File
  - Configurar Serilog en Program.cs
  - Configurar sink a consola para desarrollo
  - Configurar sink a archivo con rolling (logs/log-.txt)
  - Configurar niveles de log por namespace
  - Configurar formato de log con timestamp, nivel, mensaje, propiedades
  - _Requirements: 6.5, 8.5_

- [ ] 10. Implementar seguridad y validaciones adicionales

  - Configurar middleware de validación de tamaño de payload (10 KB máximo)
  - Implementar rate limiting con AspNetCoreRateLimit (10 req/min por IP)
  - Implementar sanitización de entrada para prevenir XSS e inyección SQL
  - Configurar CORS para permitir solo dominio del frontend
  - Leer configuración de CORS desde appsettings.json
  - Registrar todos los middlewares en orden correcto en Program.cs
  - _Requirements: 6.1, 6.2, 6.3, 6.4, 7.4_

- [ ]\* 10.1 Escribir prueba de propiedad para validación de tamaño de payload

  - **Property 13: Payload size validation**
  - **Validates: Requirements 6.1**

- [ ]\* 10.2 Escribir prueba de propiedad para rate limiting

  - **Property 14: Rate limiting enforcement**
  - **Validates: Requirements 6.2**

- [ ]\* 10.3 Escribir prueba de propiedad para sanitización de entrada

  - **Property 15: Input sanitization**
  - **Validates: Requirements 6.3**

- [ ]\* 10.4 Escribir prueba de propiedad para completitud de logging de solicitudes

  - **Property 16: Request logging completeness**
  - **Validates: Requirements 6.5**

- [ ] 11. Implementar validación de configuración al inicio

  - Crear clase ConfigurationValidator
  - Validar presencia de cadena de conexión a base de datos
  - Validar presencia de credenciales de email
  - Validar presencia de email del estudio
  - Validar presencia de configuración de CORS
  - Lanzar excepción descriptiva si falta alguna configuración
  - Invocar validación en Program.cs antes de Build()
  - _Requirements: 7.1, 7.2, 7.3, 7.4, 7.5_

- [ ]\* 11.1 Escribir prueba de propiedad para validación de configuración al inicio

  - **Property 17: Configuration validation at startup**
  - **Validates: Requirements 7.5**

- [ ] 12. Crear documentación de configuración y despliegue

  - Crear README.md con instrucciones de instalación y ejecución
  - Documentar variables de entorno requeridas
  - Documentar estructura de appsettings.json
  - Crear appsettings.Development.json de ejemplo
  - Crear appsettings.Production.json de ejemplo (sin valores sensibles)
  - Documentar proceso de migraciones de base de datos
  - Documentar configuración de SendGrid o SMTP
  - Documentar configuración de CORS para diferentes entornos
  - _Requirements: 7.1, 7.2, 7.3, 7.4_

- [ ] 13. Checkpoint - Verificar que todas las pruebas pasen

  - Asegurar que todas las pruebas pasen, preguntar al usuario si surgen dudas.

- [ ]\* 14. Crear pruebas de integración end-to-end

  - Configurar WebApplicationFactory para pruebas de integración
  - Configurar base de datos en memoria (SQLite o SQL Server LocalDB)
  - Configurar mock de servicio de email
  - Escribir prueba de flujo completo: solicitud HTTP → persistencia → email
  - Escribir prueba de manejo de errores de base de datos
  - Escribir prueba de manejo de errores de email
  - Escribir prueba de validación de entrada
  - Escribir prueba de CORS
  - Escribir prueba de rate limiting

- [ ]\* 15. Crear colección de Postman para pruebas manuales

  - Crear colección con solicitud de contacto válida
  - Crear solicitudes con diferentes errores de validación
  - Crear solicitud con payload grande (>10 KB)
  - Crear múltiples solicitudes para probar rate limiting
  - Crear solicitud desde origen no permitido (CORS)
  - Documentar variables de entorno para diferentes ambientes
  - Exportar colección a archivo JSON

- [ ] 16. Checkpoint final - Verificar sistema completo
  - Asegurar que todas las pruebas pasen, preguntar al usuario si surgen dudas.
