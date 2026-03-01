import { useEffect, useState } from 'react';
import { useAuth } from '../context/AuthContext';
import api, { API_BASE } from '../services/api';

export function ProfilePage() {
    const { user } = useAuth();
    const [form, setForm] = useState({ displayName: '', bio: '', instagramUrl: '' });
    const [avatar, setAvatar] = useState<File | null>(null);
    const [avatarPreview, setAvatarPreview] = useState<string | null>(null);
    const [loading, setLoading] = useState(true);
    const [saving, setSaving] = useState(false);
    const [alert, setAlert] = useState<{ type: 'success' | 'error'; msg: string } | null>(null);

    useEffect(() => {
        if (!user?.artistId) { setLoading(false); return; }
        api.get(`/api/artists/${user.artistId}`)
            .then(r => {
                const d = r.data;
                setForm({ displayName: d.displayName ?? '', bio: d.bio ?? '', instagramUrl: d.instagramUrl ?? '' });
                if (d.avatarUrl) setAvatarPreview(`${API_BASE}${d.avatarUrl}`);
            })
            .catch(console.error)
            .finally(() => setLoading(false));
    }, [user]);

    const showAlert = (type: 'success' | 'error', msg: string) => {
        setAlert({ type, msg });
        setTimeout(() => setAlert(null), 4000);
    };

    const handleAvatarChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const file = e.target.files?.[0];
        if (file) {
            setAvatar(file);
            setAvatarPreview(URL.createObjectURL(file));
        }
    };

    const handleSave = async (e: React.FormEvent) => {
        e.preventDefault();
        if (!user?.artistId) return;
        setSaving(true);
        try {
            const data = new FormData();
            data.append('displayName', form.displayName);
            data.append('bio', form.bio);
            data.append('instagramUrl', form.instagramUrl);
            if (avatar) data.append('avatar', avatar);

            await api.put(`/api/artists/${user.artistId}`, data, {
                headers: { 'Content-Type': 'multipart/form-data' },
            });
            showAlert('success', '✅ Perfil actualizado correctamente');
        } catch {
            showAlert('error', 'Error al guardar el perfil');
        } finally {
            setSaving(false);
        }
    };

    if (loading) return (
        <div>
            <div className="main-header"><div className="header-title">Mi Perfil</div></div>
            <div className="page-content" style={{ display: 'flex', justifyContent: 'center', paddingTop: '4rem' }}>
                <span className="spinner" style={{ width: 32, height: 32, borderWidth: 3 }} />
            </div>
        </div>
    );

    return (
        <div>
            <div className="main-header"><div className="header-title">Mi Perfil</div></div>

            <div className="page-content animate-fade-in">
                {alert && <div className={`alert alert-${alert.type}`}>{alert.msg}</div>}

                <div className="card" style={{ maxWidth: 600 }}>
                    <h2 style={{ fontSize: '1rem', fontWeight: 700, marginBottom: '1.5rem' }}>👤 Información del artista</h2>

                    <form onSubmit={handleSave}>
                        {/* Avatar */}
                        <div className="form-group" style={{ alignItems: 'flex-start' }}>
                            <label className="form-label">Foto de perfil</label>
                            <div style={{ display: 'flex', alignItems: 'center', gap: '1rem' }}>
                                <div style={{
                                    width: 72, height: 72, borderRadius: '50%',
                                    background: 'var(--muted)', overflow: 'hidden',
                                    display: 'flex', alignItems: 'center', justifyContent: 'center',
                                    fontSize: '1.8rem', flexShrink: 0,
                                    border: '2px solid var(--border)'
                                }}>
                                    {avatarPreview
                                        ? <img src={avatarPreview} alt="avatar" style={{ width: '100%', height: '100%', objectFit: 'cover' }} />
                                        : '👤'
                                    }
                                </div>
                                <label className="btn btn-ghost btn-sm" style={{ cursor: 'pointer' }}>
                                    📷 Cambiar foto
                                    <input type="file" accept="image/*" onChange={handleAvatarChange} style={{ display: 'none' }} />
                                </label>
                            </div>
                        </div>

                        <div className="form-group">
                            <label className="form-label" htmlFor="displayName">Nombre artístico</label>
                            <input id="displayName" className="form-input" value={form.displayName}
                                onChange={e => setForm(f => ({ ...f, displayName: e.target.value }))}
                                placeholder="Tu nombre como artista" required />
                        </div>

                        <div className="form-group">
                            <label className="form-label" htmlFor="bio">Biografía</label>
                            <textarea id="bio" className="form-textarea" rows={5} value={form.bio}
                                onChange={e => setForm(f => ({ ...f, bio: e.target.value }))}
                                placeholder="Cuéntanos sobre tu estilo y experiencia..." />
                        </div>

                        <div className="form-group">
                            <label className="form-label" htmlFor="instagram">Instagram URL</label>
                            <input id="instagram" className="form-input" value={form.instagramUrl}
                                onChange={e => setForm(f => ({ ...f, instagramUrl: e.target.value }))}
                                placeholder="https://instagram.com/tuusuario" type="url" />
                        </div>

                        <div style={{ display: 'flex', gap: '0.75rem' }}>
                            <button type="submit" className="btn btn-primary" disabled={saving}>
                                {saving ? <><span className="spinner" style={{ width: 14, height: 14, borderWidth: 2 }} /> Guardando...</> : '💾 Guardar perfil'}
                            </button>
                        </div>
                    </form>
                </div>

                {/* Info de cuenta */}
                <div className="card" style={{ maxWidth: 600, marginTop: '1.5rem' }}>
                    <h2 style={{ fontSize: '1rem', fontWeight: 700, marginBottom: '1rem' }}>🔑 Información de cuenta</h2>
                    <div style={{ display: 'flex', flexDirection: 'column', gap: '0.5rem' }}>
                        <div style={{ display: 'flex', justifyContent: 'space-between', padding: '0.5rem 0', borderBottom: '1px solid var(--border)' }}>
                            <span style={{ fontSize: '0.875rem', color: 'var(--muted-foreground)' }}>Email</span>
                            <span style={{ fontSize: '0.875rem', fontWeight: 500 }}>{user?.email}</span>
                        </div>
                        <div style={{ display: 'flex', justifyContent: 'space-between', padding: '0.5rem 0' }}>
                            <span style={{ fontSize: '0.875rem', color: 'var(--muted-foreground)' }}>Rol</span>
                            <span className="badge badge-red">{user?.role}</span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}
