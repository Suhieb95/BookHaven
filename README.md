# BookHaven: Online Library Store

Welcome to **BookHaven**, an advanced online library store designed to simplify and streamline the borrowing and purchasing of books. With a clean, modern interface, this platform offers a smooth user experience for both customers and administrators to manage their books, sales, and customer interactions.


## Features

- **Book Inventory Management**: Track available books, including their genre, author, price, and availability.
- **Sales Management**: Keep records of book sales, with detailed information on each transaction.
- **Customer Accounts**: Users can register, log in, and view their borrowing history and current borrowed books.
- **Book Recommendations**: Suggest books based on customers' borrowing history and preferences.
- **Top Books Sold**: Suggest books based on Most sold books.
- **User Authentication**: Secure login and registration using JWT and cookie-based sessions.
- **Search & Filter**: Search books by title, author, genre, etc.
- **Stripe Payment Gateway Integration**: Seamlessly Make payments, and get your order processed.
  
## Tech Stack


### Frontend:
- **React**: For building the user interface.
- **TypeScript**: Ensuring type safety and maintainability across the app.
- **Tailwind CSS**: For a responsive, customizable design.
- **Ant Design**: For UI components that enhance the user experience.
- **React Redux**: For global state management and handling complex application state.
- **Axios**: For Handling API Calls.
- **Image Storage**: Cloudinary.

  
### Backend:
- **C# Web API**: RESTful APIs to handle all business logic.
- **SQL Server (MSSQL)**: For managing database and storing all related data for books, customers, and transactions.
- **Redis**: For Caching Data.
- **Authentication**: JWT (JSON Web Tokens), Cookie-based session management.

### Database Schema

#### Core Entities:
- **Books**: Information about books, including title, ISBN, published year, availability, and price.
- **Customers**: Details of customers, including name, email, password, and profile information.
- **Borrowings**: Records of borrowed books, including borrow and return dates.
- **Sales**: Book sales records, including transaction amounts and customer details.
- **Authors**: Authors of books, with birth dates and other details.
- **Genres**: Categories for organizing books based on their genres.

## Maintainers And Authors

This project is maintained by:

**Suhieb Sami (https://github.com/Suhieb95)** - Creator & Lead Developer
  
[![GitHub Profile](https://img.shields.io/badge/GitHub-Suhieb95-blue?logo=github)](https://github.com/Suhieb95)

## End of README
This README.md provides a structured overview of your project, including authentication details, which are key for any developer working with BookHaven repository. Adjust the links and any specific configurations as needed!
