import React, { useState } from 'react';
import { Location, LocationTreeNode } from '../types/Location';
import './LocationTree.css';

interface LocationTreeProps {
  locations: Location[];
  selectedLocation?: Location | null;
  onSelect: (location: Location) => void;
  onCreateChild: (parentId?: string) => void;
  onDelete: (id: string) => void;
}

export const LocationTree: React.FC<LocationTreeProps> = ({
  locations,
  selectedLocation,
  onSelect,
  onCreateChild,
  onDelete
}) => {
  const [expandedNodes, setExpandedNodes] = useState<Set<string>>(new Set());

  const buildTree = (items: Location[], parentId?: string, level: number = 0): LocationTreeNode[] => {
    return items
      .filter(item => item.parentLocationId === (parentId || null))
      .map(item => ({
        ...item,
        level,
        children: buildTree(items, item.id, level + 1)
      }));
  };

  const toggleExpand = (id: string, e: React.MouseEvent) => {
    e.stopPropagation();
    const newExpanded = new Set(expandedNodes);
    if (newExpanded.has(id)) {
      newExpanded.delete(id);
    } else {
      newExpanded.add(id);
    }
    setExpandedNodes(newExpanded);
  };

  const handleDelete = (id: string, e: React.MouseEvent) => {
    e.stopPropagation();
    if (window.confirm('Are you sure you want to delete this location?')) {
      onDelete(id);
    }
  };

  const handleAddChild = (parentId: string, e: React.MouseEvent) => {
    e.stopPropagation();
    onCreateChild(parentId);
  };

  const renderNode = (node: LocationTreeNode) => {
    const hasChildren = node.children.length > 0;
    const isExpanded = expandedNodes.has(node.id);
    const isSelected = selectedLocation?.id === node.id;

    return (
      <div key={node.id} className="tree-node-container">
        <div
          className={`tree-node ${isSelected ? 'selected' : ''}`}
          style={{ paddingLeft: `${node.level * 20 + 10}px` }}
          onClick={() => onSelect(node)}
        >
          <span className="expand-icon" onClick={(e) => hasChildren && toggleExpand(node.id, e)}>
            {hasChildren ? (isExpanded ? '▼' : '▶') : '○'}
          </span>
          
          <span className="location-icon" title={node.type}>
            {getTypeIcon(node.type)}
          </span>
          
          <span className="location-name">{node.name}</span>
          
          <span className="location-type">{node.type}</span>
          
          <div className="node-actions">
            <button
              className="btn-add-child"
              onClick={(e) => handleAddChild(node.id, e)}
              title="Add child location"
            >
              +
            </button>
            <button
              className="btn-delete-node"
              onClick={(e) => handleDelete(node.id, e)}
              title="Delete location"
            >
              ×
            </button>
          </div>
        </div>
        
        {hasChildren && isExpanded && (
          <div className="tree-children">
            {node.children.map(child => renderNode(child))}
          </div>
        )}
      </div>
    );
  };

  const tree = buildTree(locations);

  return (
    <div className="location-tree">
      <div className="tree-header">
        <h3>Locations</h3>
        <button className="btn-add-root" onClick={() => onCreateChild()}>
          + Add Root Location
        </button>
      </div>
      
      {tree.length === 0 ? (
        <div className="tree-empty">
          <p>No locations yet. Click "Add Root Location" to get started.</p>
        </div>
      ) : (
        <div className="tree-content">
          {tree.map(node => renderNode(node))}
        </div>
      )}
    </div>
  );
};

function getTypeIcon(type: string): string {
  const icons: Record<string, string> = {
    'Planet': '🌍',
    'Continent': '🗺️',
    'Country': '🏛️',
    'Region': '🏞️',
    'City': '🏙️',
    'Town': '🏘️',
    'Village': '🏡',
    'Building': '🏢',
    'Room': '🚪',
    'Landmark': '⭐',
    'Mountain': '⛰️',
    'Forest': '🌲',
    'Desert': '🏜️',
    'Ocean': '🌊',
    'River': '〰️'
  };
  return icons[type] || '📍';
}
