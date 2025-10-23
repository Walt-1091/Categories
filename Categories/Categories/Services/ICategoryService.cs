using Categories.Models;

namespace Categories.Services
{
    public interface ICategoryService
    {
        string GetCategoryInfo(int categoryId);
        CategoryInfoResponse GetCategoryInfoDetailed(int categoryId);
        List<int> GetCategoriesByLevel(int level);
    }
}

