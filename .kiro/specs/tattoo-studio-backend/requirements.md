# Requirements Document

## Introduction

Este documento define los requisitos funcionales y no funcionales para el backend de una landing page de un estudio de tatuajes. El sistema permitirá a los clientes enviar mensajes de contacto y solicitar citas a través de un formulario web. El backend se implementará con ASP.NET Core Web API y Entity Framework Core, proporcionando una solución fullstack real y funcional que persiste datos en una base de datos y envía notificaciones por correo electrónico.

## Glossary

- **Sistema**: El backend API desarrollado en ASP.NET Core Web API
- **Cliente**: Usuario final que visita la landing page y envía un mensaje de contacto
- **Estudio**: El negocio de tatuajes que recibe y gestiona los mensajes de contacto
- **Mensaje de Contacto**: Registro que contiene información del cliente (nombre, correo, teléfono, mensaje) y opcionalmente una solicitud de cita
- **Proveedor de Correo**: Servicio externo (SendGrid o SMTP) utilizado para enviar notificaciones por correo electrónico
- **Base de Datos**: Sistema de persistencia donde se almacenan los mensajes de contacto
- **Formulario de Contacto**: Interfaz del frontend que captura la información del cliente
- **Solicitud de Cita**: Indicador booleano que señala si el cliente desea agendar una cita (sin fecha ni hora específica)

## Requirements

### Requirement 1

**User Story:** Como cliente del estudio de tatuajes, quiero enviar un mensaje de contacto con mi información personal, para que el estudio pueda comunicarse conmigo.

#### Acceptance Criteria

1. WHEN un cliente envía un formulario de contacto con nombre, correo electrónico, teléfono y mensaje, THEN THE Sistema SHALL validar que todos los campos requeridos estén presentes y sean válidos
2. WHEN la validación de datos es exitosa, THEN THE Sistema SHALL crear un nuevo Mensaje de Contacto con la información proporcionada
3. WHEN un cliente envía un correo electrónico, THEN THE Sistema SHALL validar que el formato del correo electrónico sea válido según el estándar RFC 5322
4. WHEN un cliente envía un teléfono, THEN THE Sistema SHALL aceptar el valor como texto sin validación de formato específico
5. IF algún campo requerido está vacío o el formato del correo es inválido, THEN THE Sistema SHALL rechazar la solicitud y retornar un mensaje de error descriptivo con código HTTP 400

### Requirement 2

**User Story:** Como cliente del estudio de tatuajes, quiero indicar si deseo agendar una cita, para que el artista pueda contactarme con su disponibilidad.

#### Acceptance Criteria

1. WHEN un cliente marca la opción "Quiero agendar una cita" en el formulario, THEN THE Sistema SHALL registrar el indicador de solicitud de cita como verdadero en el Mensaje de Contacto
2. WHEN un cliente no marca la opción de cita, THEN THE Sistema SHALL registrar el indicador de solicitud de cita como falso en el Mensaje de Contacto
3. THE Sistema SHALL procesar el mensaje de contacto de manera idéntica independientemente del valor del indicador de cita
4. THE Sistema SHALL incluir la información sobre la solicitud de cita en la notificación por correo electrónico enviada al Estudio

### Requirement 3

**User Story:** Como propietario del estudio de tatuajes, quiero que todos los mensajes de contacto se guarden en una base de datos, para poder consultar el historial de solicitudes y gestionar las respuestas.

#### Acceptance Criteria

1. WHEN el Sistema recibe un Mensaje de Contacto válido, THEN THE Sistema SHALL persistir el mensaje en la Base de Datos antes de enviar la notificación por correo
2. WHEN se persiste un mensaje, THEN THE Sistema SHALL generar automáticamente un identificador único para el registro
3. WHEN se persiste un mensaje, THEN THE Sistema SHALL registrar la fecha y hora exacta de creación del mensaje
4. IF la operación de persistencia falla, THEN THE Sistema SHALL retornar un error HTTP 500 y NO SHALL enviar la notificación por correo electrónico
5. WHEN la persistencia es exitosa, THEN THE Sistema SHALL continuar con el proceso de envío de correo electrónico

### Requirement 4

**User Story:** Como propietario del estudio de tatuajes, quiero recibir una notificación por correo electrónico cada vez que un cliente envía un mensaje, para poder responder rápidamente a las solicitudes.

#### Acceptance Criteria

1. WHEN un Mensaje de Contacto se persiste exitosamente en la Base de Datos, THEN THE Sistema SHALL enviar una notificación por correo electrónico al Estudio
2. WHEN se envía el correo, THEN THE Sistema SHALL incluir el nombre del cliente, correo electrónico, teléfono, mensaje y estado de solicitud de cita en el cuerpo del correo
3. WHEN se envía el correo, THEN THE Sistema SHALL utilizar el Proveedor de Correo configurado (SendGrid o SMTP)
4. IF el envío de correo falla, THEN THE Sistema SHALL registrar el error pero SHALL retornar una respuesta exitosa al cliente ya que el mensaje fue persistido
5. WHEN el correo se envía exitosamente, THEN THE Sistema SHALL retornar una respuesta HTTP 200 con un mensaje de confirmación al cliente

### Requirement 5

**User Story:** Como desarrollador del sistema, quiero que el backend esté organizado en capas arquitectónicas claras, para facilitar el mantenimiento y la extensibilidad del código.

#### Acceptance Criteria

1. THE Sistema SHALL implementar una capa de presentación (Controladores API) que reciba las solicitudes HTTP y retorne respuestas
2. THE Sistema SHALL implementar una capa de lógica de negocio (Servicios) que procese las reglas de validación, persistencia y envío de correos
3. THE Sistema SHALL implementar una capa de acceso a datos (DbContext/Repositorio) que gestione las operaciones con la Base de Datos
4. WHEN se modifica la lógica de envío de correos, THEN las capas de presentación y acceso a datos SHALL permanecer sin cambios
5. WHEN se modifica el proveedor de Base de Datos, THEN las capas de presentación y lógica de negocio SHALL permanecer sin cambios

### Requirement 6

**User Story:** Como administrador del sistema, quiero que la API esté protegida contra solicitudes maliciosas, para garantizar la seguridad y estabilidad del servicio.

#### Acceptance Criteria

1. WHEN el Sistema recibe una solicitud, THEN THE Sistema SHALL validar que el tamaño del payload no exceda 10 KB
2. WHEN el Sistema recibe múltiples solicitudes desde la misma dirección IP, THEN THE Sistema SHALL aplicar rate limiting para prevenir abuso (máximo 10 solicitudes por minuto)
3. WHEN el Sistema detecta caracteres potencialmente peligrosos en los campos de texto, THEN THE Sistema SHALL sanitizar la entrada para prevenir inyección de código
4. THE Sistema SHALL implementar CORS (Cross-Origin Resource Sharing) para permitir solicitudes únicamente desde el dominio del frontend configurado
5. THE Sistema SHALL registrar todas las solicitudes entrantes con timestamp, IP de origen y resultado de la operación

### Requirement 7

**User Story:** Como desarrollador del sistema, quiero que la configuración del backend sea flexible y externalizada, para poder desplegar en diferentes entornos sin modificar el código.

#### Acceptance Criteria

1. THE Sistema SHALL leer la cadena de conexión a la Base de Datos desde variables de entorno o archivo de configuración
2. THE Sistema SHALL leer las credenciales del Proveedor de Correo desde variables de entorno o archivo de configuración
3. THE Sistema SHALL leer la dirección de correo electrónico del Estudio desde variables de entorno o archivo de configuración
4. THE Sistema SHALL leer la configuración de CORS desde variables de entorno o archivo de configuración
5. WHEN una configuración requerida no está presente, THEN THE Sistema SHALL fallar al iniciar y registrar un mensaje de error descriptivo

### Requirement 8

**User Story:** Como desarrollador del sistema, quiero que el backend incluya manejo de errores robusto, para proporcionar respuestas claras y facilitar la depuración.

#### Acceptance Criteria

1. WHEN ocurre un error de validación, THEN THE Sistema SHALL retornar un código HTTP 400 con detalles específicos del error
2. WHEN ocurre un error en la Base de Datos, THEN THE Sistema SHALL retornar un código HTTP 500 con un mensaje genérico sin exponer detalles internos
3. WHEN ocurre un error en el Proveedor de Correo, THEN THE Sistema SHALL registrar el error completo en los logs pero SHALL retornar HTTP 200 al cliente
4. THE Sistema SHALL implementar un middleware global de manejo de excepciones que capture errores no controlados
5. THE Sistema SHALL registrar todos los errores con nivel de severidad, timestamp, stack trace y contexto de la solicitud
