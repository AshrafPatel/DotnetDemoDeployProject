import './App.css';
import { Navigate, Routes, Route } from 'react-router-dom';
import ContactsPage from './pages/ContactsPages/ContactsPage';
import NewContactPage from './pages/NewContactPage/NewContactPage.js';
import Navbar from './components/NavBar/Navbar';


function App() {
  return (
    <div className="App">
      <Navbar />
      <Routes>
        <Route path="/" element={<Navigate to="/contacts" />} />
        <Route path="/contacts" element={<ContactsPage/>} />
        <Route path="/contacts/new" element={<NewContactPage/>} />
      </Routes>
    </div>
  );
}

export default App;
