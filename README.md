# Adventure Microservices Project

Adventure is a microservices-based project designed to provide scalable and modular solutions for various functionalities. It follows the microservices architecture pattern, where each service is responsible for a specific aspect of the system.

## Folder Structure

- **ApiGateway:** This folder contains the API Gateway service, which serves as the entry point for client requests. It handles routing and forwards requests to the appropriate microservices.
  
- **IdentityService:** This folder contains the Identity Service, responsible for managing user authentication and authorization. It provides endpoints for user registration, login, and token generation.
  
- **Microservices:** This folder contains the individual microservices that make up the Adventure system. Each microservice focuses on a specific domain or functionality, such as user management, product catalog, order processing, etc.

## Getting Started

### Prerequisites
- .NET Core SDK 8
- Visual Studio or Visual Studio Code
- MSSQL Server

### Setting Up the Database
- Adventure project uses MSSQL Server for data storage. Ensure that you have an instance of MSSQL Server running.
- Run the SQL scripts provided in the `Database Scripts` folder to create the necessary databases and tables.

### Running the Microservices
- Open each microservice solution in your preferred IDE (Visual Studio or Visual Studio Code).
- Build and run each microservice project. Ensure that they are running on different ports to avoid conflicts.

### Configuring the API Gateway
- Open the API Gateway solution.
- Configure routing rules in the API Gateway to route incoming requests to the appropriate microservices based on the request path.

### Accessing the Identity Service
- The Identity Service provides endpoints for user authentication. Use these endpoints to register new users, login, and obtain authentication tokens.

## Contributing

Contributions to the Adventure project are welcome. If you find any bugs or have suggestions for improvements, please submit an issue or a pull request on the project repository.

## License

Adventure project is licensed under the MIT License. See the `LICENSE` file for details.

## Contact

For any inquiries or support, please contact the Adventure development team at adventure@example.com.

---

**Enjoy exploring the Adventure microservices project!**
