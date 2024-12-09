## Project Title: Banking Control Panel


## Project Overview
 
The goal is to build a simple banking control panel tool using ASP.NET Core. This tool will be API-only and include
 
features like:
User Registration & Login with JWT authentication (roles: Admin/User).
Admin CRUD operations for managing client data (add, edit, search, sort with pagination).
Client data management with fields like email, personal ID, mobile number, address, and accounts.
Caching for filters and pagination to improve efficiency.
 
## Installation

Install Prerequisites:
.NET SDK: Download here
Visual Studio Code or Visual Studio IDE
 
## Usage
 
How the tool is used: Admins manage client data (add, edit, filter, sort, search) while users log in securely.
How libraries are used: For validation (email, mobile, etc.) and JWT for secure login.
How filters and settings are saved.
Testing with Postman and tracking changes using GitHub or other version control tools.
 
## Features
 
1. User Registration and Login with JWT Authentication
Allow users to register with roles (Admin or User).
Allow users to log in securely using JWT tokens for authentication.

2. CRUD Operations for Clients (Admin Only)
Admin users can:
Add, Edit, View client data.
Filter and sort client data.
Use pagination for displaying client lists.

3. Save Filter and Paging Settings
Save the parameters used for filtering or pagination in cache or database.
Provide the last 3 search parameters as suggestions.

4. Client Data Fields
Clients should have the following fields:

Email
First name
Last name
Personal ID (exactly 11 characters)
Profile photo
Mobile number (with correct format including country code)
Sex (Male/Female)

Address (with Subfields: Country, City, Street, Zip Code)
Accounts (at least one account required)

5. Validations
Email is valid and required.
First name/Last name must have less than 60 characters.
Mobile number follows the correct format with a country code.
Personal ID is exactly 11 characters.

 
## Technologies Used
 
- **Backend**: ASP.NET Core Web Api
- **Frontend**: ASP.NET Core MVC
- **Database**: SQL Server
 
## How to Run the Application
Install Prerequisites:

Make sure .NET SDK is installed
Clone the Repository
Copy code
git clone <repository-url>
cd <project-folder>
Restore Dependencies

Libraries Used:
ASP.NET Core
Entity Framework Core (for database access)
JWT (for authentication)

install necessary packages
Run the Application
