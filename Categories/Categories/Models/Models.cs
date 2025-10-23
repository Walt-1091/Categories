namespace Categories.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public int ParentCategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Keywords { get; set; } = string.Empty;
    }

    public class CategoryInfoResponse
    {
        public int ParentCategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Keywords { get; set; } = string.Empty;
        public string FormattedOutput { get; set; } = string.Empty;
    }

    public class CategoryLevelResponse
    {
        public int Level { get; set; }
        public List<int> CategoryIds { get; set; } = new();
        public int Count { get; set; }
    }

    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public string? Error { get; set; }
    }
}
