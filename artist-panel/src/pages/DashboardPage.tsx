import { useEffect, useState } from 'react';
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

export function DashboardPage() {
    const { user } = useAuth();
    const [images, setImages] = useState<GalleryImage[]>([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        if (!user?.artistId) { setLoading(false); return; }
        api.get<GalleryImage[]>(`/api/gallery/artist/${user.artistId}`)
            .then(r => setImages(r.data))
            .catch(console.error)
            .finally(() => setLoading(false));
    }, [user]);

    const recent = images.slice(0, 3);
    const categories = [...new Set(images.map(i => i.category).filter(Boolean))];

    return (
        <div>
            <div className="main-header">
                <div>
                    <div className="header-title">Dashboard</div>
                </div>
                <span style={{ fontSize: '0.85rem', color: 'var(--muted-foreground)' }}>
                    Bienvenido, {user?.displayName ?? user?.email} 👋
                </span>
            </div>

            <div className="page-content animate-fade-in">
                {/* Stats */}
                <div className="stats-grid">
                    <div className="stat-card">
                        <div className="stat-value">{loading ? '—' : images.length}</div>
                        <div className="stat-label">📸 Fotos publicadas</div>
                    </div>
                    <div className="stat-card">
                        <div className="stat-value">{loading ? '—' : categories.length}</div>
                        <div className="stat-label">🏷️ Categorías</div>
                    </div>
                    <div className="stat-card">
                        <div className="stat-value" style={{ fontSize: '1.1rem', color: 'var(--ink-red)', textTransform: 'capitalize' }}>
                            {user?.role ?? '—'}
                        </div>
                        <div className="stat-label">🎭 Tu rol</div>
                    </div>
                </div>

                {/* Recent uploads */}
                <div className="card">
                    <h2 style={{ fontSize: '1rem', fontWeight: 700, marginBottom: '1rem' }}>
                        🕐 Últimas fotos subidas
                    </h2>
                    {loading ? (
                        <div style={{ display: 'flex', justifyContent: 'center', padding: '2rem' }}>
                            <span className="spinner" />
                        </div>
                    ) : recent.length === 0 ? (
                        <div className="empty-state" style={{ padding: '2rem' }}>
                            <div className="empty-state-icon">🖼️</div>
                            <div className="empty-state-text">Aún no has subido fotos</div>
                            <div className="empty-state-hint">Ve a <strong>Mi Galería</strong> para empezar</div>
                        </div>
                    ) : (
                        <div className="image-grid">
                            {recent.map(img => (
                                <div key={img.id} className="image-card">
                                    <img
                                        src={`${API_BASE}${img.src}`}
                                        alt={img.alt}
                                        onError={e => { (e.target as HTMLImageElement).src = '/placeholder.png'; }}
                                    />
                                    <div className="image-card-body">
                                        <div className="image-card-alt">{img.alt}</div>
                                        {img.category && <div className="image-card-cat">#{img.category}</div>}
                                    </div>
                                </div>
                            ))}
                        </div>
                    )}
                </div>
            </div>
        </div>
    );
}
