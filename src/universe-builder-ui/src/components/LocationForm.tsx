import React, { useState, useEffect } from 'react';
import { Location, CreateLocationDto, UpdateLocationDto } from '../types/Location';
import { LocationService } from '../services/LocationService';
import './LocationForm.css';

interface LocationFormProps {
  universeId: string;
  location?: Location | null;
  parentLocation?: Location | null;
  onSave: () => void;
  onCancel: () => void;
}

const LOCATION_TYPES = [
  'Planet', 'Continent', 'Country', 'Region', 'State', 'City',
  'Town', 'Village', 'Building', 'Room', 'Landmark', 'Mountain',
  'Forest', 'Desert', 'Ocean', 'River', 'Lake', 'Island', 'Other'
];

export const LocationForm: React.FC<LocationFormProps> = ({
  universeId,
  location,
  parentLocation,
  onSave,
  onCancel
}) => {
  const [name, setName] = useState('');
  const [type, setType] = useState('City');
  const [description, setDescription] = useState('');
  const [climate, setClimate] = useState('');
  const [population, setPopulation] = useState('');
  const [area, setArea] = useState('');
  const [tags, setTags] = useState('');
  
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    if (location) {
      setName(location.name);
      setType(location.type);
      setDescription(location.description);
      setClimate(location.climate);
      setPopulation(location.population?.toString() || '');
      setArea(location.area?.toString() || '');
      setTags(location.tags.join(', '));
    }
  }, [location]);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    if (!name.trim()) {
      setError('Location name is required');
      return;
    }

    if (!type.trim()) {
      setError('Location type is required');
      return;
    }

    setLoading(true);
    setError(null);

    try {
      const tagArray = tags.split(',').map(t => t.trim()).filter(t => t);
      
      if (location) {
        const updateData: UpdateLocationDto = {
          parentLocationId: location.parentLocationId,
          name,
          type,
          description,
          climate,
          population: population ? parseInt(population) : undefined,
          area: area ? parseFloat(area) : undefined,
          customProperties: {},
          tags: tagArray
        };
        await LocationService.update(location.id, updateData);
      } else {
        const createData: CreateLocationDto = {
          universeId,
          parentLocationId: parentLocation?.id,
          name,
          type,
          description,
          climate,
          population: population ? parseInt(population) : undefined,
          area: area ? parseFloat(area) : undefined,
          customProperties: {},
          tags: tagArray
        };
        await LocationService.create(createData);
      }

      onSave();
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Failed to save location');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="location-form-container">
      <div className="location-form">
        <h2>{location ? 'Edit Location' : 'Create New Location'}</h2>
        
        {parentLocation && (
          <div className="parent-info">
            Creating under: <strong>{parentLocation.name}</strong> ({parentLocation.type})
          </div>
        )}

        {error && <div className="form-error">{error}</div>}

        <form onSubmit={handleSubmit}>
          <div className="form-row">
            <div className="form-group">
              <label htmlFor="name">
                Name <span className="required">*</span>
              </label>
              <input
                id="name"
                type="text"
                value={name}
                onChange={(e) => setName(e.target.value)}
                placeholder="Enter location name"
                maxLength={200}
                required
              />
              <small className="char-count">{name.length}/200</small>
            </div>

            <div className="form-group">
              <label htmlFor="type">
                Type <span className="required">*</span>
              </label>
              <select
                id="type"
                value={type}
                onChange={(e) => setType(e.target.value)}
                required
              >
                {LOCATION_TYPES.map(t => (
                  <option key={t} value={t}>{t}</option>
                ))}
              </select>
            </div>
          </div>

          <div className="form-group">
            <label htmlFor="description">Description</label>
            <textarea
              id="description"
              value={description}
              onChange={(e) => setDescription(e.target.value)}
              placeholder="Enter location description"
              rows={4}
              maxLength={5000}
            />
            <small className="char-count">{description.length}/5000</small>
          </div>

          <div className="form-row">
            <div className="form-group">
              <label htmlFor="climate">Climate</label>
              <input
                id="climate"
                type="text"
                value={climate}
                onChange={(e) => setClimate(e.target.value)}
                placeholder="e.g., Temperate, Tropical"
                maxLength={200}
              />
            </div>

            <div className="form-group">
              <label htmlFor="population">Population</label>
              <input
                id="population"
                type="number"
                value={population}
                onChange={(e) => setPopulation(e.target.value)}
                placeholder="Enter population"
                min="0"
              />
            </div>
          </div>

          <div className="form-row">
            <div className="form-group">
              <label htmlFor="area">Area (km²)</label>
              <input
                id="area"
                type="number"
                value={area}
                onChange={(e) => setArea(e.target.value)}
                placeholder="Enter area"
                min="0"
                step="0.01"
              />
            </div>

            <div className="form-group">
              <label htmlFor="tags">Tags</label>
              <input
                id="tags"
                type="text"
                value={tags}
                onChange={(e) => setTags(e.target.value)}
                placeholder="Comma-separated tags"
              />
            </div>
          </div>

          <div className="form-actions">
            <button type="button" onClick={onCancel} className="btn-secondary" disabled={loading}>
              Cancel
            </button>
            <button type="submit" className="btn-primary" disabled={loading}>
              {loading ? 'Saving...' : 'Save Location'}
            </button>
          </div>
        </form>
      </div>
    </div>
  );
};
