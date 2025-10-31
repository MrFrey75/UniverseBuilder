import React, { useState } from 'react';
import { Universe } from '../types/Universe';
import { UniverseList } from '../components/UniverseList';
import { UniverseForm } from '../components/UniverseForm';
import './UniverseManagement.css';

export const UniverseManagement: React.FC = () => {
  const [showForm, setShowForm] = useState(false);
  const [selectedUniverse, setSelectedUniverse] = useState<Universe | null>(null);
  const [refreshKey, setRefreshKey] = useState(0);

  const handleCreateNew = () => {
    setSelectedUniverse(null);
    setShowForm(true);
  };

  const handleSelectUniverse = (universe: Universe) => {
    setSelectedUniverse(universe);
    setShowForm(true);
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
