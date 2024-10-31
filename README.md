# swan-lake

MarktguruApi is a C# project that provides an API for managing products.
It includes features such as product creation, validation, and logging.

## Table of Contents

* [Installation](#Installation)
* [Usage](#Usage)
* [Docker](#Docker)
* [Project](#Structure)
* [Endpoints](#Endpoints)

## Installation

Clone the repository:

```bash
git clone https://github.com/yourusername/MarktguruApi.git
```

Navigate to the project directory:

```bash
cd /src/MarktguruApi
```

Restore the dependencies:

```bash
dotnet restore
```

## Usage

Build the project:

```bash
dotnet build
```

Run the project:

```bash
dotnet run
```

## Docker

You can also build and run the project using Docker.

Navigate to the project directory:

```bash
cd /src
```

Build the Docker image:

```bash
docker build -t marktguruapi -f MarktguruApi/Dockerfile .
```

Run the Docker container:

```bash
docker run -p 5000:8080 marktguruapi
```

## Project Structure

* src/MarktguruApi/: Contains the main source code for the API.
* Controllers/: Contains the API controllers.
* Models/: Contains the data models.
* Validation/: Contains the validation logic.
* Behaviors/: Contains the MediatR pipeline behaviors.
* Utils/: Contains utility classes and global exception handling.
* MediatR/: Contains the MediatR query, command and handler classes.
* Repositories/: Contains the repository classes.

## Endpoints

### Authentication endpoint

Authentication is required to access parts of the api.

To authenticate, you can use the following credentials:

* Username: Test
* Password: test

You can use the following endpoint to authenticate:

* POST /token: Authenticate a user.

### Products endpoint

There are several endpoints available for managing products:

* GET /api/products: Get all products.
* GET /api/products/{id}: Get a product by ID.
* POST /api/products: Create a new product.
* PUT /api/products/{id}: Update a product by ID.
* DELETE /api/products/{id}: Delete a product by ID.
* GET /api/products/paginated: Get paginated products.

### Health endpoint

Additionally, there is a endpoint for health checks:

* GET /health: Check the health of the API.

## Postman collection

You can find a Postman collection within the docs folder.
