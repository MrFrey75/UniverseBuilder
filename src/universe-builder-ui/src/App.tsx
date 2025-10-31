import React from 'react';
import './App.css';
import { UniverseManagement } from './pages/UniverseManagement';

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <h1>🌍 UniverseBuilder</h1>
        <p>Create, organize, and maintain your fictional worlds</p>
      </header>
      <main>
        <UniverseManagement />
      </main>
    </div>
  );
}

export default App;
