# ClinicOps - Clinic Admin Dashboard

A full-stack clinic administration dashboard built with React, TypeScript, Tailwind CSS, ASP.NET Core, Entity Framework Core, and SQLite.

## Features

### Admin Dashboard
- Real-time statistics display
- Overview of tasks and appointments
- Quick action links

### Task Tracker
- Full CRUD operations (Create, Read, Update, Delete)
- Task status management (Pending, In Progress, Completed, Cancelled)
- Priority levels (Low, Medium, High, Urgent)
- Filtering by status and priority
- Pagination support
- Due date tracking

### Appointment Request Flow
- Public appointment request form
- Admin appointment list view
- Status management (Pending, Approved, Rejected, Completed, Cancelled)
- Admin notes capability
- Filtering and pagination

## Technology Stack

### Frontend
- **React 18** with TypeScript
- **Vite** for build tooling
- **Tailwind CSS** for styling
- **React Router** for navigation
- **Axios** for API calls

### Backend
- **ASP.NET Core 10** Web API
- **Entity Framework Core 9** for ORM
- **SQLite** database
- RESTful API architecture
- CORS enabled for frontend communication

## Prerequisites

- Node.js 20+ and npm
- .NET SDK 10+

## Getting Started

### Backend Setup

1. Navigate to the backend directory:
```bash
cd backend/ClinicOps.Api
```

2. Restore dependencies:
```bash
dotnet restore
```

3. Run the backend API:
```bash
dotnet run
```

The API will be available at `http://localhost:5000` (or the port shown in the terminal).

### Frontend Setup

1. Navigate to the frontend directory:
```bash
cd frontend
```

2. Install dependencies:
```bash
npm install
```

3. Start the development server:
```bash
npm run dev
```

The frontend will be available at `http://localhost:5173`.

## API Endpoints

### Dashboard
- `GET /api/dashboard/stats` - Get dashboard statistics

### Tasks
- `GET /api/tasks` - Get all tasks (with filtering and pagination)
- `GET /api/tasks/{id}` - Get a specific task
- `POST /api/tasks` - Create a new task
- `PUT /api/tasks/{id}` - Update a task
- `DELETE /api/tasks/{id}` - Delete a task

### Appointments
- `GET /api/appointments` - Get all appointments (with filtering and pagination)
- `GET /api/appointments/{id}` - Get a specific appointment
- `POST /api/appointments` - Create a new appointment request
- `PATCH /api/appointments/{id}/status` - Update appointment status

## Database

The application uses SQLite for data storage. The database file (`clinicops.db`) is automatically created when you first run the backend API.

## Project Structure

```
clinic-admin-dashboard/
├── backend/
│   └── ClinicOps.Api/
│       ├── Controllers/      # API controllers
│       ├── Data/            # DbContext
│       ├── DTOs/            # Data transfer objects
│       └── Models/          # Entity models
├── frontend/
│   └── src/
│       ├── components/      # React components
│       ├── pages/          # Page components
│       ├── services/       # API service layer
│       └── types/          # TypeScript type definitions
└── README.md
```

## Features in Detail

### Non-Clinical Focus
All data and features are designed for administrative purposes only, with no clinical or medical information handling.

### Professional UI
- Clean, modern interface
- Responsive design
- Intuitive navigation
- Color-coded status indicators
- Modal dialogs for forms

### REST API Design
- Standard HTTP methods
- JSON request/response format
- Proper status codes
- Pagination support
- Query parameter filtering

## Development

### Building for Production

**Frontend:**
```bash
cd frontend
npm run build
```

**Backend:**
```bash
cd backend/ClinicOps.Api
dotnet publish -c Release
```

## License

This project is for educational and demonstration purposes.