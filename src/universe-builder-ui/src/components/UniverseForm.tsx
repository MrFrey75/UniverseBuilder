import React, { useState, useEffect } from 'react';
import { Universe, CreateUniverseDto, UpdateUniverseDto } from '../types/Universe';
import { UniverseService } from '../services/UniverseService';
import './UniverseForm.css';

interface UniverseFormProps {
  universe?: Universe | null;
  onSave: () => void;
  onCancel: () => void;
}

export const UniverseForm: React.FC<UniverseFormProps> = ({ universe, onSave, onCancel }) => {
  const [name, setName] = useState('');
  const [description, setDescription] = useState('');
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [validationErrors, setValidationErrors] = useState<{ name?: string; description?: string }>({});

  useEffect(() => {
    if (universe) {
      setName(universe.name);
      setDescription(universe.description);
    }
  }, [universe]);

  const validate = (): boolean => {
    const errors: { name?: string; description?: string } = {};

    if (!name.trim()) {
      errors.name = 'Universe name is required';
    } else if (name.length > 200) {
      errors.name = 'Name must be 200 characters or less';
    }

    if (description.length > 5000) {
      errors.description = 'Description must be 5000 characters or less';
    }

    setValidationErrors(errors);
    return Object.keys(errors).length === 0;
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    if (!validate()) {
      return;
    }

    setLoading(true);
    setError(null);

    try {
      const data = { name, description };

      if (universe) {
        await UniverseService.update(universe.id, data as UpdateUniverseDto);
      } else {
        await UniverseService.create(data as CreateUniverseDto);
      }

      onSave();
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Failed to save universe');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="universe-form-container">
      <div className="universe-form">
        <h2>{universe ? 'Edit Universe' : 'Create New Universe'}</h2>

        {error && <div className="form-error">{error}</div>}

        <form onSubmit={handleSubmit}>
          <div className="form-group">
            <label htmlFor="name">
              Name <span className="required">*</span>
            </label>
            <input
              id="name"
              type="text"
              value={name}
              onChange={(e) => setName(e.target.value)}
              className={validationErrors.name ? 'input-error' : ''}
              placeholder="Enter universe name"
              maxLength={200}
            />
            {validationErrors.name && (
              <span className="validation-error">{validationErrors.name}</span>
            )}
            <small className="char-count">{name.length}/200</small>
          </div>

          <div className="form-group">
            <label htmlFor="description">Description</label>
            <textarea
              id="description"
              value={description}
              onChange={(e) => setDescription(e.target.value)}
              className={validationErrors.description ? 'input-error' : ''}
              placeholder="Enter universe description"
              rows={6}
              maxLength={5000}
            />
            {validationErrors.description && (
              <span className="validation-error">{validationErrors.description}</span>
            )}
            <small className="char-count">{description.length}/5000</small>
          </div>

          <div className="form-actions">
            <button type="button" onClick={onCancel} className="btn-secondary" disabled={loading}>
              Cancel
            </button>
            <button type="submit" className="btn-primary" disabled={loading}>
              {loading ? 'Saving...' : 'Save Universe'}
            </button>
          </div>
        </form>
      </div>
    </div>
  );
};
