import React from 'react';
import './App.css';
import { UniverseManagement } from './pages/UniverseManagement';

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <h1>🌍 Universe Builder</h1>
      </header>
      <main>
        <UniverseManagement />
      </main>
    </div>
  );
}

export default App;
