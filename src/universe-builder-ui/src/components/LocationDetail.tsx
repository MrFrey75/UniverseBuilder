import React from 'react';
import { Location } from '../types/Location';
import './LocationDetail.css';

interface LocationDetailProps {
  location: Location | null;
  path: Location[];
  onEdit: () => void;
}

export const LocationDetail: React.FC<LocationDetailProps> = ({ location, path, onEdit }) => {
  if (!location) {
    return (
      <div className="location-detail">
        <div className="detail-empty">
          <p>Select a location from the tree to view details</p>
        </div>
      </div>
    );
  }

  return (
    <div className="location-detail">
      <div className="detail-header">
        <h2>{location.name}</h2>
        <button className="btn-edit" onClick={onEdit}>
          Edit
        </button>
      </div>

      {path.length > 0 && (
        <div className="breadcrumb">
          {path.map((loc, index) => (
            <React.Fragment key={loc.id}>
              {index > 0 && <span className="breadcrumb-separator">›</span>}
              <span className="breadcrumb-item">{loc.name}</span>
            </React.Fragment>
          ))}
        </div>
      )}

      <div className="detail-content">
        <div className="detail-section">
          <div className="detail-field">
            <label>Type:</label>
            <span className="type-badge">{location.type}</span>
          </div>

          {location.description && (
            <div className="detail-field full-width">
              <label>Description:</label>
              <p className="description-text">{location.description}</p>
            </div>
          )}

          {location.climate && (
            <div className="detail-field">
              <label>Climate:</label>
              <span>{location.climate}</span>
            </div>
          )}

          {location.population !== null && location.population !== undefined && (
            <div className="detail-field">
              <label>Population:</label>
              <span>{location.population.toLocaleString()}</span>
            </div>
          )}

          {location.area !== null && location.area !== undefined && (
            <div className="detail-field">
              <label>Area:</label>
              <span>{location.area.toLocaleString()} km²</span>
            </div>
          )}

          {location.coordinates && (
            <div className="detail-field full-width">
              <label>Coordinates:</label>
              <span>
                Lat: {location.coordinates.latitude.toFixed(4)},
                Long: {location.coordinates.longitude.toFixed(4)}
                {location.coordinates.altitude && `, Alt: ${location.coordinates.altitude}m`}
              </span>
            </div>
          )}

          {location.tags && location.tags.length > 0 && (
            <div className="detail-field full-width">
              <label>Tags:</label>
              <div className="tags-container">
                {location.tags.map((tag, index) => (
                  <span key={index} className="tag">{tag}</span>
                ))}
              </div>
            </div>
          )}

          {Object.keys(location.customProperties).length > 0 && (
            <div className="detail-field full-width">
              <label>Custom Properties:</label>
              <div className="custom-properties">
                {Object.entries(location.customProperties).map(([key, value]) => (
                  <div key={key} className="property-row">
                    <span className="property-key">{key}:</span>
                    <span className="property-value">{value}</span>
                  </div>
                ))}
              </div>
            </div>
          )}
        </div>

        <div className="detail-footer">
          <small>Created: {new Date(location.createdDate).toLocaleString()}</small>
          <small>Modified: {new Date(location.modifiedDate).toLocaleString()}</small>
        </div>
      </div>
    </div>
  );
};
