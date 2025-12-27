import axios from 'axios';
import type {
  DashboardStats,
  TaskItem,
  AppointmentRequest,
  PaginatedResult,
  CreateTaskDto,
  UpdateTaskDto,
  CreateAppointmentDto,
  UpdateAppointmentStatusDto,
} from '../types';

const API_BASE_URL = 'http://localhost:5000/api';

const api = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

export const dashboardService = {
  getStats: async (): Promise<DashboardStats> => {
    const response = await api.get<DashboardStats>('/dashboard/stats');
    return response.data;
  },
};

export const tasksService = {
  getTasks: async (
    status?: string,
    priority?: string,
    pageNumber = 1,
    pageSize = 10
  ): Promise<PaginatedResult<TaskItem>> => {
    const params = new URLSearchParams();
    if (status) params.append('status', status);
    if (priority) params.append('priority', priority);
    params.append('pageNumber', pageNumber.toString());
    params.append('pageSize', pageSize.toString());
    
    const response = await api.get<PaginatedResult<TaskItem>>(
      `/tasks?${params.toString()}`
    );
    return response.data;
  },

  getTask: async (id: number): Promise<TaskItem> => {
    const response = await api.get<TaskItem>(`/tasks/${id}`);
    return response.data;
  },

  createTask: async (task: CreateTaskDto): Promise<TaskItem> => {
    const response = await api.post<TaskItem>('/tasks', task);
    return response.data;
  },

  updateTask: async (id: number, task: UpdateTaskDto): Promise<void> => {
    await api.put(`/tasks/${id}`, task);
  },

  deleteTask: async (id: number): Promise<void> => {
    await api.delete(`/tasks/${id}`);
  },
};

export const appointmentsService = {
  getAppointments: async (
    status?: string,
    pageNumber = 1,
    pageSize = 10
  ): Promise<PaginatedResult<AppointmentRequest>> => {
    const params = new URLSearchParams();
    if (status) params.append('status', status);
    params.append('pageNumber', pageNumber.toString());
    params.append('pageSize', pageSize.toString());
    
    const response = await api.get<PaginatedResult<AppointmentRequest>>(
      `/appointments?${params.toString()}`
    );
    return response.data;
  },

  getAppointment: async (id: number): Promise<AppointmentRequest> => {
    const response = await api.get<AppointmentRequest>(`/appointments/${id}`);
    return response.data;
  },

  createAppointment: async (
    appointment: CreateAppointmentDto
  ): Promise<AppointmentRequest> => {
    const response = await api.post<AppointmentRequest>(
      '/appointments',
      appointment
    );
    return response.data;
  },

  updateAppointmentStatus: async (
    id: number,
    update: UpdateAppointmentStatusDto
  ): Promise<void> => {
    await api.patch(`/appointments/${id}/status`, update);
  },
};
