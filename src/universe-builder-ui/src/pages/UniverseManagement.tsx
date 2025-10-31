import React, { useState } from 'react';
import { Universe } from '../types/Universe';
import { UniverseList } from '../components/UniverseList';
import { UniverseForm } from '../components/UniverseForm';
import { LocationManagement } from './LocationManagement';
import './UniverseManagement.css';

export const UniverseManagement: React.FC = () => {
  const [showForm, setShowForm] = useState(false);
  const [selectedUniverse, setSelectedUniverse] = useState<Universe | null>(null);
  const [refreshKey, setRefreshKey] = useState(0);
  const [viewingLocations, setViewingLocations] = useState(false);
  const [activeUniverse, setActiveUniverse] = useState<Universe | null>(null);

  const handleCreateNew = () => {
    setSelectedUniverse(null);
    setShowForm(true);
  };

  const handleSelectUniverse = (universe: Universe) => {
    setActiveUniverse(universe);
    setViewingLocations(true);
  };

  const handleEditUniverse = (universe: Universe) => {
    setSelectedUniverse(universe);
    setShowForm(true);
  };

  const handleBackToUniverses = () => {
    setViewingLocations(false);
    setActiveUniverse(null);
    setRefreshKey((prev) => prev + 1);
  };

  const handleSave = () => {
    setShowForm(false);
    setSelectedUniverse(null);
    setRefreshKey((prev) => prev + 1);
  };

  const handleCancel = () => {
    setShowForm(false);
    setSelectedUniverse(null);
  };

  if (viewingLocations && activeUniverse) {
    return (
      <div className="universe-management">
        <div className="universe-header">
          <button className="btn-back" onClick={handleBackToUniverses}>
            ← Back to Universes
          </button>
          <h2>{activeUniverse.name} - Locations</h2>
          <button className="btn-edit-universe" onClick={() => handleEditUniverse(activeUniverse)}>
            Edit Universe
          </button>
        </div>
        <LocationManagement universeId={activeUniverse.id} />
        
        {showForm && (
          <UniverseForm universe={selectedUniverse} onSave={handleSave} onCancel={handleCancel} />
        )}
      </div>
    );
  }

  return (
    <div className="universe-management">
      <UniverseList
        key={refreshKey}
        onSelectUniverse={handleSelectUniverse}
        onCreateNew={handleCreateNew}
      />

      {showForm && (
        <UniverseForm universe={selectedUniverse} onSave={handleSave} onCancel={handleCancel} />
      )}
    </div>
  );
};
