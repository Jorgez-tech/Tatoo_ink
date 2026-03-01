import { useEffect, useRef, useState } from 'react';
import { useAuth } from '../context/AuthContext';
import api, { API_BASE } from '../services/api';

interface GalleryImage {
    id: number;
    src: string;
    alt: string;
    category?: string;
    description?: string;
    createdAt: string;
}

interface EditForm { alt: string; category: string; description: string; }

export function GalleryPage() {
    const { user } = useAuth();
    const [images, setImages] = useState<GalleryImage[]>([]);
    const [loading, setLoading] = useState(true);
    const [uploading, setUploading] = useState(false);
    const [editTarget, setEditTarget] = useState<GalleryImage | null>(null);
    const [editForm, setEditForm] = useState<EditForm>({ alt: '', category: '', description: '' });
    const [alert, setAlert] = useState<{ type: 'success' | 'error'; msg: string } | null>(null);
    const [preview, setPreview] = useState<string | null>(null);
    const fileRef = useRef<HTMLInputElement>(null);

    const fetchImages = () => {
        if (!user?.artistId) return;
        setLoading(true);
        api.get<GalleryImage[]>(`/api/gallery/artist/${user.artistId}`)
            .then(r => setImages(r.data))
            .catch(() => showAlert('error', 'No se pudieron cargar las imágenes'))
            .finally(() => setLoading(false));
    };

    useEffect(fetchImages, [user]);

    const showAlert = (type: 'success' | 'error', msg: string) => {
        setAlert({ type, msg });
        setTimeout(() => setAlert(null), 4000);
    };

    const [altText, setAltText] = useState('');
    const [category, setCategory] = useState('');
    const [description, setDescription] = useState('');

    // ── Upload ──────────────────────────────────────────────────────────────
    const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const file = e.target.files?.[0];
        if (file) setPreview(URL.createObjectURL(file));
    };

    const handleUpload = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        const file = fileRef.current?.files?.[0];
        if (!file) { showAlert('error', 'Selecciona una imagen primero'); return; }
        if (!altText.trim()) { showAlert('error', 'El campo Descripción alt es requerido'); return; }

        // Construir FormData con PascalCase para ASP.NET model binding
        const data = new FormData();
        data.append('image', file);          // Coincide con IFormFile image en el controller
        data.append('Alt', altText);         // Coincide con CreateGalleryImageRequest.Alt
        data.append('Category', category);
        data.append('Description', description);

        setUploading(true);
        try {
            await api.post('/api/gallery', data);
            showAlert('success', '✅ Imagen subida correctamente');
            // Resetear form
            setAltText('');
            setCategory('');
            setDescription('');
            setPreview(null);
            if (fileRef.current) fileRef.current.value = '';
            fetchImages();
        } catch (err: unknown) {
            const errData = (err as { response?: { data?: unknown } })?.response?.data;
            const msg = typeof errData === 'object' && errData !== null && 'error' in errData
                ? String((errData as { error: string }).error)
                : 'Error al subir la imagen';
            showAlert('error', msg);
        } finally {
            setUploading(false);
        }
    };

    // ── Edit ─────────────────────────────────────────────────────────────────
    const openEdit = (img: GalleryImage) => {
        setEditTarget(img);
        setEditForm({ alt: img.alt, category: img.category ?? '', description: img.description ?? '' });
    };

    const handleEdit = async (e: React.FormEvent) => {
        e.preventDefault();
        if (!editTarget) return;
        try {
            await api.put(`/api/gallery/${editTarget.id}`, editForm);
            showAlert('success', '✅ Imagen actualizada');
            setEditTarget(null);
            fetchImages();
        } catch {
            showAlert('error', 'Error al actualizar');
        }
    };

    // ── Delete ────────────────────────────────────────────────────────────────
    const handleDelete = async (id: number) => {
        if (!confirm('¿Seguro que quieres eliminar esta imagen?')) return;
        try {
            await api.delete(`/api/gallery/${id}`);
            showAlert('success', '🗑️ Imagen eliminada');
            fetchImages();
        } catch {
            showAlert('error', 'Error al eliminar');
        }
    };

    return (
        <div>
            <div className="main-header">
                <div className="header-title">Mi Galería</div>
            </div>

            <div className="page-content animate-fade-in">
                {alert && (
                    <div className={`alert alert-${alert.type}`}>{alert.msg}</div>
                )}

                {/* Upload form */}
                <div className="card" style={{ marginBottom: '2rem' }}>
                    <h2 style={{ fontSize: '1rem', fontWeight: 700, marginBottom: '1rem' }}>📤 Subir nueva foto</h2>
                    <form onSubmit={handleUpload}>
                        <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '1rem' }}>
                            {/* Zona de drop */}
                            <div>
                                <div
                                    className="upload-zone"
                                    onClick={() => fileRef.current?.click()}
                                >
                                    {preview ? (
                                        <img src={preview} alt="preview" style={{ maxHeight: 140, margin: '0 auto', borderRadius: 8, objectFit: 'cover' }} />
                                    ) : (
                                        <>
                                            <div className="upload-zone-icon">📁</div>
                                            <div className="upload-zone-text">Clic para seleccionar imagen</div>
                                            <div className="upload-zone-hint">JPG, PNG, WEBP · máx 10 MB</div>
                                        </>
                                    )}
                                </div>
                                <input
                                    ref={fileRef}
                                    type="file"
                                    name="image"
                                    accept="image/*"
                                    required
                                    onChange={handleFileChange}
                                    style={{ display: 'none' }}
                                />
                            </div>

                            {/* Campos */}
                            <div>
                                <div className="form-group">
                                    <label className="form-label" htmlFor="upload-alt">Descripción alt *</label>
                                    <input
                                        id="upload-alt"
                                        className="form-input"
                                        placeholder="Ej: Tatuaje geométrico en brazo"
                                        value={altText}
                                        onChange={e => setAltText(e.target.value)}
                                        required
                                    />
                                </div>
                                <div className="form-group">
                                    <label className="form-label" htmlFor="upload-cat">Categoría</label>
                                    <input
                                        id="upload-cat"
                                        className="form-input"
                                        placeholder="Ej: Geometric, Black Ink..."
                                        value={category}
                                        onChange={e => setCategory(e.target.value)}
                                    />
                                </div>
                                <div className="form-group">
                                    <label className="form-label" htmlFor="upload-desc">Descripción del trabajo</label>
                                    <textarea
                                        id="upload-desc"
                                        className="form-textarea"
                                        placeholder="Cuéntanos sobre este trabajo..."
                                        rows={3}
                                        value={description}
                                        onChange={e => setDescription(e.target.value)}
                                    />
                                </div>
                                <button type="submit" className="btn btn-primary" disabled={uploading}>
                                    {uploading ? <><span className="spinner" style={{ width: 14, height: 14, borderWidth: 2 }} /> Subiendo...</> : '📤 Subir foto'}
                                </button>
                            </div>
                        </div>
                    </form>
                </div>

                {/* Grid de imágenes */}
                <div className="page-header">
                    <div>
                        <div className="page-title">Mis fotos</div>
                        <div className="page-subtitle">{images.length} imagen{images.length !== 1 ? 'es' : ''}</div>
                    </div>
                </div>

                {loading ? (
                    <div style={{ display: 'flex', justifyContent: 'center', padding: '3rem' }}>
                        <span className="spinner" style={{ width: 32, height: 32, borderWidth: 3 }} />
                    </div>
                ) : images.length === 0 ? (
                    <div className="empty-state">
                        <div className="empty-state-icon">🖼️</div>
                        <div className="empty-state-text">No tienes fotos todavía</div>
                        <div className="empty-state-hint">Usa el formulario de arriba para subir tu primera foto</div>
                    </div>
                ) : (
                    <div className="image-grid">
                        {images.map(img => (
                            <div key={img.id} className="image-card animate-fade-in">
                                <img src={`${API_BASE}${img.src}`} alt={img.alt}
                                    onError={e => { (e.target as HTMLImageElement).src = '/placeholder.png'; }}
                                />
                                <div className="image-card-body">
                                    <div className="image-card-alt">{img.alt}</div>
                                    {img.category && <div className="image-card-cat">#{img.category}</div>}
                                    {img.description && <div className="image-card-desc">{img.description}</div>}
                                </div>
                                <div className="image-card-actions">
                                    <button className="btn btn-secondary btn-sm" onClick={() => openEdit(img)}>✏️ Editar</button>
                                    <button className="btn btn-danger btn-sm" onClick={() => handleDelete(img.id)}>🗑️</button>
                                </div>
                            </div>
                        ))}
                    </div>
                )}
            </div>

            {/* Modal de edición */}
            {editTarget && (
                <div className="modal-overlay" onClick={e => e.target === e.currentTarget && setEditTarget(null)}>
                    <div className="modal">
                        <div className="modal-header">
                            <span className="modal-title">✏️ Editar imagen</span>
                            <button className="modal-close" onClick={() => setEditTarget(null)}>✕</button>
                        </div>
                        <form onSubmit={handleEdit}>
                            <div className="form-group">
                                <label className="form-label">Alt / título</label>
                                <input className="form-input" value={editForm.alt}
                                    onChange={e => setEditForm(f => ({ ...f, alt: e.target.value }))} required />
                            </div>
                            <div className="form-group">
                                <label className="form-label">Categoría</label>
                                <input className="form-input" value={editForm.category}
                                    onChange={e => setEditForm(f => ({ ...f, category: e.target.value }))} />
                            </div>
                            <div className="form-group">
                                <label className="form-label">Descripción</label>
                                <textarea className="form-textarea" value={editForm.description} rows={4}
                                    onChange={e => setEditForm(f => ({ ...f, description: e.target.value }))} />
                            </div>
                            <div style={{ display: 'flex', gap: '0.75rem', justifyContent: 'flex-end' }}>
                                <button type="button" className="btn btn-ghost" onClick={() => setEditTarget(null)}>Cancelar</button>
                                <button type="submit" className="btn btn-primary">Guardar cambios</button>
                            </div>
                        </form>
                    </div>
                </div>
            )}
        </div>
    );
}
