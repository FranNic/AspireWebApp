# AspireWebApp

Simple WebApp using Blazor and Aspire for educational purposes.

## Overview
AspireWebApp leverages Blazor for front-end components, enabling interactive web applications with C#. Entity Framework Core is used for data access with migrations to manage the database schema. Key functionalities include a Pomodoro timer and a Todo list.

## Key Features
### Pomodoro Timer
- **Files**:
  - `Pomodoro.razor` handles the UI for the Pomodoro timer, including start, stop, and reset buttons.
  - `PomodoroTimer.cs` defines the timer logic, including session management (work, break, long break).
  - `PomodoroState.cs` manages the state of the Pomodoro timer, handling timer initialization, session changes, and state updates.Implements a Pomodoro timer using Blazor components and C# classes. 

### Todo List
- **Files**:
  - `TodoItemForm.razor`
  - `TodoDbContextModelSnapshot.cs`
- **Description**: Provides a form for adding and editing Todo items with validation, and manages the database schema using Entity Framework Core.

## API and Blazor Communication
- **Files**:
  - `TodoApiClient.cs`
  - `Program.cs` (both `AspireWebApp.Web` and `Todo.API`)
- **Description**: The `TodoApiClient.cs` handles HTTP communication between the Blazor front-end and the API. It includes methods for retrieving, adding, updating, and deleting Todo items. The `Program.cs` files set up the HTTP client services and configure the API endpoints.


## Getting Started
1. Clone the repository: `git clone https://github.com/FranNic/AspireWebApp.git`
2. Navigate to the project directory: `cd AspireWebApp`
3. Follow the setup instructions in the `SETUP.md` file (if available).

## Contact
For any inquiries or support, please contact FranNic at `nicosia.francisco@gmail.com`.
