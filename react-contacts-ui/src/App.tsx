import './App.css';
import { Navigate, Routes, Route } from 'react-router-dom';
import ContactsPage from './pages/ContactsPages/ContactsPage.tsx';
import NewContactPage from './pages/NewContactPage/NewContactPage.tsx';
import Navbar from './components/NavBar/Navbar.tsx';


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
