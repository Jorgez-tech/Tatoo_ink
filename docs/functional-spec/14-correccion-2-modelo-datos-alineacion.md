# Corrección 2: Alineación de modelos existentes (IDs, entidades)

Fecha: 2026-03-17
Estatus: Propuesta de enmienda

## Problema

La especificación propone UUID para todos los IDs, pero el código actual utiliza `int`. Además, ignora completamente la entidad GalleryImage que ya existe, causando contradicciones lógicas.

## Decisión de diseño

### Mantener INT para IDs (no migrar a UUID)

**Razón:**
1. El código está en producción con Int
2. Migraciones requerirían reescribir todas las tablas
3. No hay beneficio funcional (UUID es para escalabilidad horizontal masiva que no necesitamos aquí)
4. INT es más eficiente para índices en SQLite

**Línea de cambio en especificación:**

Actualizar 05-modelo-datos-conceptual.md:

```diff
- id (uuid)
+ id (int - autoincrement)
- contactRequestId (uuid)
+ contactRequestId (int)
- authorUserId (uuid)
+ authorUserId (int)
- id (uuid)
+ id (int)
```

### Incluir GalleryImage en modelo

GalleryImage YA EXISTE en código:

```csharp
public class GalleryImage
{
    public int Id { get; set; }
    [Required]
    [MaxLength(500)]
    public string Src { get; set; } = string.Empty;
    [MaxLength(500)]
    public string? Fallback { get; set; }
    [Required]
    [MaxLength(200)]
    public string Alt { get; set; } = string.Empty;
    [MaxLength(100)]
    public string? Category { get; set; }
    [MaxLength(100)]
    public string? Photographer { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
```

**Agregar a 05-modelo-datos-conceptual.md:**

```
## GalleryImage

- id (int)
- src (URL de imagen)
- fallback (URL de fallback WebP -> JPG)
- alt (texto alternativo - REQUERIDO)
- category (categoría de trabajo - opcional)
- photographer (crédito - opcional)
- createdAt
```

### Alinear ContactRequest con ContactMessage actual

**ContactMessage ACTUAL en código:**

```csharp
public class ContactMessage
{
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    [Required]
    [EmailAddress]
    [MaxLength(100)]
    public string Email { get; set; }
    [Required]
    [MaxLength(20)]
    public string Phone { get; set; }
    [Required]
    [MaxLength(1000)]
    public string Message { get; set; }
    [Required]
    public bool WantsAppointment { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool EmailSent { get; set; } = false;
    public DateTime? EmailSentAt { get; set; }
}
```

**Actualizar 05-modelo-datos-conceptual.md ContactRequest:**

```diff
 ## ContactRequest

 - id (int)
 - name
 - email
 - phone (optional)
 - message
 - serviceInterest (optional)
 - status
 - source
 - createdAt
 - updatedAt
 - assignedToUserId (optional)
+
+## NOTA DE MAPEO
+
+- name: de Name de ContactMessage
+- phone: de Phone de ContactMessage
+- message: de Message de ContactMessage
+- status: NUEVO (no existe en ContactMessage hoy)
+- wantsAppointment: mapeado desde ContactMessage.WantsAppointment
+- emailSent: tracking de si se envió email
+- emailSentAt: timestamp del envío
```

## Próximos pasos

1. Revisar cada modelo existente (hay más que quizás se omitieron)
2. Actualizar 05-modelo-datos-conceptual.md con mapeos 1:1 código actual
3. Documentar migraciones de pequeños cambios (único nuevo campo: "status")
4. Generar DTOs de forma que hereden de entidades existentes

## No hacer

❌ No migrar de Int a Guid - costo muy alto, sin beneficio
❌ No renombrar entidades existentes - adaptarlas en lo que sea necesario
✅ Reutilizar lo que funciona
