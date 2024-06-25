# Out of Office Solution

This project is developed using **Clean Architecture** and **CQRS** to manage various functionalities related to employee leave requests, projects, and approval processes.

## General Functionality Overview

The "Out of Office" solution is designed to manage employee leave requests, project assignments, and approval processes. The system supports different roles with varying access levels and functionalities.

## Directories in the Solution

The solution contains the following main directories:

- **Employees**
- **Leave Requests**
- **Approval Requests**
- **Projects**
## Login Page
  ![LoginPage](https://github.com/Brajnn/Out-of-Office/assets/120382137/23415e96-7ee5-45a1-b237-b147fd36f8da)
## Employee

**Features:**
- List all employees with the ability to:
  - Sort table rows using column headers.
  - Filter table rows.
  - Search by name.
  - Add, update, and deactivate employees.
  ![Employee](https://github.com/Brajnn/Out-of-Office/assets/120382137/246b28b7-5665-4785-967d-7cc355a1c242)

## Leave Request

**Features:**

- List all leave requests with the ability to:
  - Sort table rows using column headers.
  - Filter table rows.
  - Search by request number.
  - Create and update leave requests.
    ![CreateLeaveRequest](https://github.com/Brajnn/Out-of-Office/assets/120382137/544ab25c-eb42-4782-bf77-b8304c7d46bb)

  - Submit or cancel leave requests, updating the status and creating related approval requests.

    ![AfterSubmit](https://github.com/Brajnn/Out-of-Office/assets/120382137/7ebdce7c-7d59-4b0b-b7e3-7d01652d8458)
  - After submit new Approval Request Created
    ![ApprovalRequest](https://github.com/Brajnn/Out-of-Office/assets/120382137/493a82ec-76b4-43ab-a52c-bf8e5a7db5d6)


## Approval Request

**Features:**

- List all approval requests with the ability to:
  - Sort table rows using column headers.
  - Filter table rows.
  - Search by request number.
  - View Details.
    
    ![ApprovalDetails](https://github.com/Brajnn/Out-of-Office/assets/120382137/7c48e2bb-7624-4235-b755-f853b405cc8c)
  - Approve or reject requests, updating the leave request status accordingly.
    ![AfterApprove](https://github.com/Brajnn/Out-of-Office/assets/120382137/5ec46468-b08b-447f-a689-6e4c26670bdb)
  - After Approval Request Out of Office Balace is changed.
    ![BalanceChanged](https://github.com/Brajnn/Out-of-Office/assets/120382137/98fb0f1a-dfd8-4001-8fcb-419886d3035c)

## Project

**Features:**

- List all projects with the ability to:
  - Sort table rows using column headers.
  - Filter table rows.
  - Search by project ID.
  - HR Manager and Employee can't change the status of project.
   ![Project](https://github.com/Brajnn/Out-of-Office/assets/120382137/c4b39758-0642-404a-940e-111294b992af)
  - But Project Manager can active/deactivate projects.
   ![Project2](https://github.com/Brajnn/Out-of-Office/assets/120382137/63b5f759-fc0c-44af-857d-a23627740403)


  - Create projects.
    ![CreateProject](https://github.com/Brajnn/Out-of-Office/assets/120382137/04f5e620-7153-404e-97f7-ab5b49929669)
  - Project Details.
    ![ProjectDetails](https://github.com/Brajnn/Out-of-Office/assets/120382137/64627150-2556-4c43-b64e-b218d6543dd9)

  - Assign employees to projects.
    ![AssignProject](https://github.com/Brajnn/Out-of-Office/assets/120382137/9583c48f-456b-46b4-8073-db28d132ea52)



## Roles in the System

**Employee:**

- Creates leave requests.
  ![CreateLeave](https://github.com/Brajnn/Out-of-Office/assets/120382137/2d3e839b-fa5b-4dea-9686-5b47aeee672b)

- Views own projects and leave requests.
  ![EmployeeView](https://github.com/Brajnn/Out-of-Office/assets/120382137/25216dd4-914d-45b9-8c72-f5270025bd3a)

**HR Manager:**

- Manages the list of employees.
- Approves or rejects requests.
- Views and manages all employees, leave requests, and approval requests.

**Project Manager:**

- Manages the list of projects.
- Approves or rejects requests.
- Views and manages projects and approval requests related to their projects.
- Assigns employees to projects.
  ![OnlyProjectManager](https://github.com/Brajnn/Out-of-Office/assets/120382137/fd52e090-5358-47d6-8f84-8058cbae2199)


**Administrator:**

- Manages all data within the system.

### Steps to Set Up and Run the Project
1. **Clone the Repository**

   Open a terminal and run the following command to clone the repository:

   ```bash
   git clone https://github.com/Brajnn/Out-of-Office.git
   cd Out-of-Office
2. **Update the Connection String**
Open the appsettings.json file located in the root directory of the project and update the connection string to match your SQL Server setup.
Replace yourServerName with your SQL Server instance name, and yourDatabaseName with the name of the database you want to use:
  ```bash
  {
    "ConnectionStrings": {
      "OutOfOfficeConnectionString": "Server=yourServerName;Database=yourDatabaseName;Trusted_Connection=True;"
    }
  ```
3. **Apply Database Migrations**
Open a terminal in the project directory and run the following command to apply the database migrations:
```bash
dotnet ef database update
```
5. **Configure Launch Settings**
Ensure the launch settings in Properties/launchSettings.json are configured correctly. This step is usually already set up, but you can verify it.
6. **Run the Application**
```bash
dotnet run
```
The application should now be running at http://localhost:5126 or https://localhost:7112.
