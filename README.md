Elderly Care Support System

**Overview**
Elderly Care Support System is designed to assist caregivers, healthcare professionals, and families in providing better care and support for elderly individuals. The system helps track appointments, daily activities, and overall well-being of seniors, ensuring they receive the necessary attention and care.

Tech Stack
- Backend:  
  - ASP.NET Web API (with .NET 8)
  - SQL Server (for database management)
  - Dapper (for data access and ORM functionality)
  - FluentValidation (for validating user inputs and data)
  - Hangfire (for background task management and scheduling)

- Authentication:  
  - JWT with KeyCloak

- Notifications:  
  - Email with SendGrid

Features
  - Caregiver & Family Support: A platform for caregivers and family members to share updates, communicate, and coordinate care.
  - Health Monitoring: Track vital signs, medical history, and medications.
  - Appointment Scheduling: Manage medical appointments, reminders for prescriptions, and other essential tasks.
  - Emergency Alerts: Notify caregivers in case of emergencies or unusual activity patterns.
  - Daily Activities Tracker: Track daily activities such as meals, exercises, and rest periods.
  - Reports & Insights: Provide reports on health progress, medication adherence, and care routines.

 Installation
  - To run the Elderly Care Support System locally, follow the steps below:

 Prerequisites
  - .NET 8 SDK (for backend setup)
  - SQL Server (for database)
  - NPM or Yarn (for package management)

 Clone the Repository
  ```bash
  git clone https://github.com/yourusername/elderly-care-support.git
  cd elderly-care-support
  ```

 Backend Setup
  1. Install dependencies:
     ```bash
     dotnet restore
     ```
  
  2. Configure the database:
     - Set up SQL Server.
     - Configure the connection string in the `appsettings.json` file.
  
  3. Run the backend API:
     ```bash
     dotnet run
     ```

 Accessing the Application
 Once the server is running, you can access the Elderly Care Support System through `http://localhost: 5000` (backend).

 Usage
  1. Sign up/Login: Users (caregivers, family members) can create an account or log in.
  2. Add Elderly Individuals: Add senior members to the system, including their personal details, medical history, and care plan.
  3. Monitor Health: Track medical data such as blood pressure, glucose levels, and medications.
  4. Schedule Appointments: Manage doctor visits and medication schedules.
  5. Set Alerts: Configure reminders and emergency notifications.

 Contributing
 We welcome contributions to the Elderly Care Support System! If you'd like to help improve the project:
  1. Fork the repository.
  2. Create a new branch for your changes.
  3. Submit a pull request describing your changes.

Contact
For any issues or inquiries, please contact:
- Email: guhan000714@gmail.com
