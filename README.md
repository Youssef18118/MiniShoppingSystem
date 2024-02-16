MiniShoppingSystem (Book System App)
This .Net Core project uses Repository pattern along with MVC pattern to handle backend functionalities. It aims to provide a small-scale Book system application to control users, books, genres, and orders.

Features
Authentication and User Management
Implemented validation for register and login fields.
Provided Profile section for users to change their login credentials.
Order Management
Added "My Orders" section for users after they make a purchase along with "All Orders" section for the admin to keep track of all orders made.
Implemented Cart section for user to add to cart any selected books and deciding to purchase at any time.
Book Management
Constructed a clean view for all books available in the system for users and the admin.
Enabled admin to add, delete, edit, and view details of available books and genres.
LinkedIn link for the video of testing the application
Will be announced soon 🔍.

Required Packages
Microsoft.EntityFrameworkCore: Integral for managing data persistence in the booking system, facilitating interactions with the database.
Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore: Essential for handling any exceptions or errors that may occur during database operations.
Microsoft.AspNetCore.Identity.EntityFrameworkCore: Crucial for implementing user authentication and authorization features within the booking system.
Microsoft.AspNetCore.Identity.UI: Provides pre-built user interface components for user authentication and management for the booking system's frontend.
Microsoft.EntityFrameworkCore.SqlServer: Specifically tailored for SQL Server databases to optimize performance and compatibility with SQL Server.
Microsoft.EntityFrameworkCore.Tools: Streamlines database-related tasks such as migrations and scaffolding.
Microsoft.VisualStudio.Web.CodeGeneration.Design: Provide templates for controllers and views.
Installation requirements
Clone the repo using git clone https://github.com/Youssef18118/MiniShoppingSystem.git.
Navigate to the project directory.
Edit Connection string in appsettings.json with your SQL server authentication credentials.
Add migration to DB using package manager console. You can find it in: Tools -> NuGet Package manager -> package manager console.
Install required packages in manage NuGet packages. You can find it in: Project -> manage NuGet packages.
Run the project.
