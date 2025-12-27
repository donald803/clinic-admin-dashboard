import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Layout from './components/Layout';
import Dashboard from './pages/Dashboard';
import Tasks from './pages/Tasks';
import Appointments from './pages/Appointments';
import RequestAppointment from './pages/RequestAppointment';

function App() {
  return (
    <Router>
      <Layout>
        <Routes>
          <Route path="/" element={<Dashboard />} />
          <Route path="/tasks" element={<Tasks />} />
          <Route path="/appointments" element={<Appointments />} />
          <Route path="/request-appointment" element={<RequestAppointment />} />
        </Routes>
      </Layout>
    </Router>
  );
}

export default App;
