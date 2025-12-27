export const TaskStatus = {
  Pending: 'Pending',
  InProgress: 'InProgress',
  Completed: 'Completed',
  Cancelled: 'Cancelled',
} as const;

export type TaskStatus = typeof TaskStatus[keyof typeof TaskStatus];

export const TaskPriority = {
  Low: 'Low',
  Medium: 'Medium',
  High: 'High',
  Urgent: 'Urgent',
} as const;

export type TaskPriority = typeof TaskPriority[keyof typeof TaskPriority];

export const AppointmentStatus = {
  Pending: 'Pending',
  Approved: 'Approved',
  Rejected: 'Rejected',
  Completed: 'Completed',
  Cancelled: 'Cancelled',
} as const;

export type AppointmentStatus = typeof AppointmentStatus[keyof typeof AppointmentStatus];

export interface TaskItem {
  id: number;
  title: string;
  description: string;
  status: TaskStatus;
  priority: TaskPriority;
  createdAt: string;
  completedAt?: string;
  dueDate?: string;
}

export interface AppointmentRequest {
  id: number;
  patientName: string;
  email: string;
  phone: string;
  preferredDate: string;
  preferredTime: string;
  reason: string;
  status: AppointmentStatus;
  createdAt: string;
  adminNotes?: string;
}

export interface DashboardStats {
  totalTasks: number;
  pendingTasks: number;
  inProgressTasks: number;
  completedTasks: number;
  totalAppointments: number;
  pendingAppointments: number;
  approvedAppointments: number;
  todayAppointments: number;
}

export interface PaginatedResult<T> {
  items: T[];
  totalCount: number;
  pageNumber: number;
  pageSize: number;
  totalPages: number;
}

export interface CreateTaskDto {
  title: string;
  description: string;
  priority: string;
  dueDate?: string;
}

export interface UpdateTaskDto {
  title?: string;
  description?: string;
  status?: string;
  priority?: string;
  dueDate?: string;
}

export interface CreateAppointmentDto {
  patientName: string;
  email: string;
  phone: string;
  preferredDate: string;
  preferredTime: string;
  reason: string;
}

export interface UpdateAppointmentStatusDto {
  status: string;
  adminNotes?: string;
}
