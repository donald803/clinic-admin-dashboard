import { useEffect, useState } from 'react';
import { dashboardService } from '../services/api';
import type { DashboardStats } from '../types';

export default function Dashboard() {
  const [stats, setStats] = useState<DashboardStats | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    loadStats();
  }, []);

  const loadStats = async () => {
    try {
      const data = await dashboardService.getStats();
      setStats(data);
    } catch (error) {
      console.error('Failed to load stats:', error);
    } finally {
      setLoading(false);
    }
  };

  if (loading) {
    return (
      <div className="flex justify-center items-center h-64">
        <div className="text-lg text-gray-600">Loading...</div>
      </div>
    );
  }

  if (!stats) {
    return (
      <div className="flex justify-center items-center h-64">
        <div className="text-lg text-red-600">Failed to load dashboard data</div>
      </div>
    );
  }

  const statCards = [
    {
      title: 'Total Tasks',
      value: stats.totalTasks,
      color: 'bg-blue-100 text-blue-800',
    },
    {
      title: 'Pending Tasks',
      value: stats.pendingTasks,
      color: 'bg-yellow-100 text-yellow-800',
    },
    {
      title: 'In Progress Tasks',
      value: stats.inProgressTasks,
      color: 'bg-purple-100 text-purple-800',
    },
    {
      title: 'Completed Tasks',
      value: stats.completedTasks,
      color: 'bg-green-100 text-green-800',
    },
    {
      title: 'Total Appointments',
      value: stats.totalAppointments,
      color: 'bg-indigo-100 text-indigo-800',
    },
    {
      title: 'Pending Appointments',
      value: stats.pendingAppointments,
      color: 'bg-orange-100 text-orange-800',
    },
    {
      title: 'Approved Appointments',
      value: stats.approvedAppointments,
      color: 'bg-teal-100 text-teal-800',
    },
    {
      title: 'Today\'s Appointments',
      value: stats.todayAppointments,
      color: 'bg-pink-100 text-pink-800',
    },
  ];

  return (
    <div className="px-4 sm:px-0">
      <h1 className="text-3xl font-bold text-gray-900 mb-6">Admin Dashboard</h1>
      
      <div className="grid grid-cols-1 gap-5 sm:grid-cols-2 lg:grid-cols-4">
        {statCards.map((stat) => (
          <div
            key={stat.title}
            className="bg-white overflow-hidden shadow rounded-lg"
          >
            <div className="px-4 py-5 sm:p-6">
              <dt className="text-sm font-medium text-gray-500 truncate">
                {stat.title}
              </dt>
              <dd className="mt-1 text-3xl font-semibold text-gray-900">
                {stat.value}
              </dd>
            </div>
            <div className={`px-4 py-2 sm:px-6 ${stat.color}`}>
              <div className="text-sm font-medium">
                {stat.title.includes('Task') ? 'Tasks' : 'Appointments'}
              </div>
            </div>
          </div>
        ))}
      </div>

      <div className="mt-8 bg-white shadow rounded-lg p-6">
        <h2 className="text-xl font-semibold text-gray-900 mb-4">
          Quick Actions
        </h2>
        <div className="grid grid-cols-1 gap-4 sm:grid-cols-3">
          <a
            href="/tasks"
            className="inline-flex items-center justify-center px-4 py-2 border border-transparent text-sm font-medium rounded-md text-white bg-blue-600 hover:bg-blue-700"
          >
            Manage Tasks
          </a>
          <a
            href="/appointments"
            className="inline-flex items-center justify-center px-4 py-2 border border-transparent text-sm font-medium rounded-md text-white bg-green-600 hover:bg-green-700"
          >
            View Appointments
          </a>
          <a
            href="/request-appointment"
            className="inline-flex items-center justify-center px-4 py-2 border border-gray-300 text-sm font-medium rounded-md text-gray-700 bg-white hover:bg-gray-50"
          >
            Request Appointment
          </a>
        </div>
      </div>
    </div>
  );
}
