namespace Categories.Services
{
    using Categories.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CategoryService : ICategoryService
    {
        private readonly Dictionary<int, Category> _categoryLookup;
        private readonly Func<int, Category?> _getCategory;
        private readonly Func<Category, bool> _hasKeywords;
        private readonly Func<Category, bool> _isRoot;
        private readonly Func<int, IEnumerable<Category>> _getChildren;

        public CategoryService(List<Category> categories)
        {
            _categoryLookup = categories.ToDictionary(c => c.CategoryId);
            _getCategory = id => _categoryLookup.GetValueOrDefault(id);
            _hasKeywords = cat => !string.IsNullOrWhiteSpace(cat?.Keywords);
            _isRoot = cat => cat?.ParentCategoryId == -1;
            _getChildren = parentId => _categoryLookup.Values.Where(c => c.ParentCategoryId == parentId);
        }

        public string GetCategoryInfo(int categoryId)
        {
            var category = _getCategory(categoryId) ??
                throw new KeyNotFoundException($"Category with ID {categoryId} not found.");

            var keywords = FindKeywordRecursive(category);
            return FormatCategoryOutput(category, keywords);
        }

        public CategoryInfoResponse GetCategoryInfoDetailed(int categoryId)
        {
            var category = _getCategory(categoryId) ??
                throw new KeyNotFoundException($"Category with ID {categoryId} not found.");

            var keywords = FindKeywordRecursive(category);

            return new CategoryInfoResponse
            {
                ParentCategoryId = category.ParentCategoryId,
                Name = category.Name,
                Keywords = keywords,
                FormattedOutput = FormatCategoryOutput(category, keywords)
            };
        }

        public List<int> GetCategoriesByLevel(int level)
        {
            var validateLevel = new Func<int, int>(lvl =>
                lvl < 1 ? throw new ArgumentException("Level must be greater than 0.") : lvl);

            validateLevel(level);

            var levelMap = BuildLevelMap(level);

            return levelMap
                .Where(kvp => kvp.Value == level)
                .Select(kvp => kvp.Key)
                .OrderBy(id => id)
                .ToList();
        }

        public List<Category> GetAllCategories() => _categoryLookup.Values.ToList();

        private string FindKeywordRecursive(Category category)
        {
            return _hasKeywords(category)
                ? category.Keywords
                : _isRoot(category)
                    ? string.Empty
                    : FindKeywordRecursive(_getCategory(category.ParentCategoryId)!);
        }

        private string FormatCategoryOutput(Category cat, string keywords) =>
            $"ParentCategoryID={cat.ParentCategoryId}, Name={cat.Name}, Keywords={keywords}";

        private Dictionary<int, int> BuildLevelMap(int maxLevel)
        {
            var rootCategories = _categoryLookup.Values.Where(c => _isRoot(c));
            var initialMap = rootCategories.ToDictionary(c => c.CategoryId, c => 1);

            return Enumerable
                .Range(2, Math.Max(0, maxLevel - 1))
                .Aggregate(initialMap, (map, currentLevel) =>
                    ExpandLevelMap(map, currentLevel));
        }

        private Dictionary<int, int> ExpandLevelMap(Dictionary<int, int> levelMap, int currentLevel)
        {
            var parentIds = levelMap
                .Where(kvp => kvp.Value == currentLevel - 1)
                .Select(kvp => kvp.Key);

            var newEntries = parentIds
                .SelectMany(_getChildren)
                .ToDictionary(c => c.CategoryId, c => currentLevel);

            return levelMap
                .Concat(newEntries)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }
    }
}
