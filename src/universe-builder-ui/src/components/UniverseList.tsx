import React, { useEffect, useState } from 'react';
import { Universe } from '../types/Universe';
import { UniverseService } from '../services/UniverseService';
import './UniverseList.css';

interface UniverseListProps {
  onSelectUniverse: (universe: Universe) => void;
  onCreateNew: () => void;
}

export const UniverseList: React.FC<UniverseListProps> = ({ onSelectUniverse, onCreateNew }) => {
  const [universes, setUniverses] = useState<Universe[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    loadUniverses();
  }, []);

  const loadUniverses = async () => {
    try {
      setLoading(true);
      setError(null);
      const data = await UniverseService.getAll();
      setUniverses(data);
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Failed to load universes');
    } finally {
      setLoading(false);
    }
  };

  const handleDelete = async (id: string, e: React.MouseEvent) => {
    e.stopPropagation();
    if (window.confirm('Are you sure you want to delete this universe?')) {
      try {
        await UniverseService.delete(id);
        await loadUniverses();
      } catch (err) {
        setError(err instanceof Error ? err.message : 'Failed to delete universe');
      }
    }
  };

  if (loading) {
    return <div className="loading">Loading universes...</div>;
  }

  if (error) {
    return (
      <div className="error">
        <p>Error: {error}</p>
        <button onClick={loadUniverses}>Retry</button>
      </div>
    );
  }

  return (
    <div className="universe-list">
      <div className="list-header">
        <h2>My Universes</h2>
        <button className="btn-primary" onClick={onCreateNew}>
          Create New Universe
        </button>
      </div>

      {universes.length === 0 ? (
        <div className="empty-state">
          <p>No universes found. Create your first universe to get started!</p>
        </div>
      ) : (
        <div className="universe-grid">
          {universes.map((universe) => (
            <div
              key={universe.id}
              className="universe-card"
            >
              <div className="card-header">
                <h3>{universe.name}</h3>
                <div className="card-actions">
                  <button
                    className="btn-view"
                    onClick={(e) => {
                      e.stopPropagation();
                      onSelectUniverse(universe);
                    }}
                    title="View locations"
                  >
                    View
                  </button>
                  <button
                    className="btn-delete"
                    onClick={(e) => handleDelete(universe.id, e)}
                    title="Delete universe"
                  >
                    ×
                  </button>
                </div>
              </div>
              <p className="card-description">{universe.description || 'No description'}</p>
              <div className="card-footer">
                <small>Created: {new Date(universe.createdDate).toLocaleDateString()}</small>
                <small>Modified: {new Date(universe.modifiedDate).toLocaleDateString()}</small>
              </div>
            </div>
          ))}
        </div>
      )}
    </div>
  );
};
