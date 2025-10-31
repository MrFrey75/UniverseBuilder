import React, { useState, useEffect } from 'react';
import { Location } from '../types/Location';
import { LocationService } from '../services/LocationService';
import { LocationTree } from '../components/LocationTree';
import { LocationDetail } from '../components/LocationDetail';
import { LocationForm } from '../components/LocationForm';
import './LocationManagement.css';

interface LocationManagementProps {
  universeId: string;
}

export const LocationManagement: React.FC<LocationManagementProps> = ({ universeId }) => {
  const [locations, setLocations] = useState<Location[]>([]);
  const [selectedLocation, setSelectedLocation] = useState<Location | null>(null);
  const [locationPath, setLocationPath] = useState<Location[]>([]);
  const [showForm, setShowForm] = useState(false);
  const [editingLocation, setEditingLocation] = useState<Location | null>(null);
  const [parentForNew, setParentForNew] = useState<Location | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    loadLocations();
  }, [universeId]);

  useEffect(() => {
    if (selectedLocation) {
      loadLocationPath();
    }
  }, [selectedLocation]);

  const loadLocations = async () => {
    try {
      setLoading(true);
      setError(null);
      const data = await LocationService.getAllByUniverse(universeId);
      setLocations(data);
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Failed to load locations');
    } finally {
      setLoading(false);
    }
  };

  const loadLocationPath = async () => {
    if (!selectedLocation) return;
    try {
      const path = await LocationService.getPath(selectedLocation.id);
      setLocationPath(path);
    } catch (err) {
      console.error('Failed to load path:', err);
    }
  };

  const handleSelectLocation = (location: Location) => {
    setSelectedLocation(location);
  };

  const handleCreateNew = (parentId?: string) => {
    const parent = parentId ? locations.find(l => l.id === parentId) : null;
    setParentForNew(parent || null);
    setEditingLocation(null);
    setShowForm(true);
  };

  const handleEdit = () => {
    if (selectedLocation) {
      setEditingLocation(selectedLocation);
      setParentForNew(null);
      setShowForm(true);
    }
  };

  const handleSave = async () => {
    setShowForm(false);
    setEditingLocation(null);
    setParentForNew(null);
    await loadLocations();
    
    // Refresh selected location if it was edited
    if (selectedLocation) {
      try {
        const updated = await LocationService.getById(selectedLocation.id);
        setSelectedLocation(updated);
      } catch (err) {
        setSelectedLocation(null);
      }
    }
  };

  const handleCancel = () => {
    setShowForm(false);
    setEditingLocation(null);
    setParentForNew(null);
  };

  const handleDelete = async (id: string) => {
    try {
      await LocationService.delete(id);
      if (selectedLocation?.id === id) {
        setSelectedLocation(null);
      }
      await loadLocations();
    } catch (err) {
      alert(err instanceof Error ? err.message : 'Failed to delete location');
    }
  };

  if (loading) {
    return (
      <div className="location-management">
        <div className="loading-container">
          <p>Loading locations...</p>
        </div>
      </div>
    );
  }

  if (error) {
    return (
      <div className="location-management">
        <div className="error-container">
          <p>Error: {error}</p>
          <button onClick={loadLocations}>Retry</button>
        </div>
      </div>
    );
  }

  return (
    <div className="location-management">
      <div className="management-layout">
        <div className="tree-panel">
          <LocationTree
            locations={locations}
            selectedLocation={selectedLocation}
            onSelect={handleSelectLocation}
            onCreateChild={handleCreateNew}
            onDelete={handleDelete}
          />
        </div>

        <div className="detail-panel">
          <LocationDetail
            location={selectedLocation}
            path={locationPath}
            onEdit={handleEdit}
          />
        </div>
      </div>

      {showForm && (
        <LocationForm
          universeId={universeId}
          location={editingLocation}
          parentLocation={parentForNew}
          onSave={handleSave}
          onCancel={handleCancel}
        />
      )}
    </div>
  );
};
