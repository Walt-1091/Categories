Solution
Technology: .NET Core 9 Web API with Swagger/OpenAPI
Approach:

Delegates and functional programming (minimal if statements)
Recursive keyword inheritance traversal
LINQ aggregation for hierarchy levels

✅ Test Results
Problem 1: Category Info with Keyword Inheritance
InputOutputStatus201ParentCategoryID=200, Name=Computer, Keywords=Teaching✅202ParentCategoryID=201, Name=Operating System, Keywords=Teaching✅
Endpoints:

GET /api/categories/{id} - JSON response
GET /api/categories/{id}/formatted - String format

Problem 2: Categories by Level
InputOutputStatus2[101, 102, 201]✅3[103, 109, 202]✅
Endpoint: GET /api/categories/level/{level}
🏗️ Architecture
Categories/
├── Program.cs              # Entry point + DI config
├── Models/Category.cs      # Data models
├── Services/
│   ├── ICategoryService.cs
│   └── CategoryService.cs  # Business logic
└── Controllers/
    └── CategoriesController.cs
Key Features:

Dependency injection
RESTful API design
Proper error handling (404, 400)
Interactive Swagger documentation
