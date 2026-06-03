using MyFoodTracker.Models;

namespace MyFoodTracker.Services;

public class MockFoodService : IFoodService
{
    private List<FoodItem> _items;

    public MockFoodService()
    {
        _items =
        [
            new()
            {
                Id = 1,
                Name = "Berry Yogurt Bowl",
                Category = "Breakfast",
                Description = "Greek yogurt with berries",
                Calories = 340,
                Protein = 24,
                Carbs = 42,
                Fat = 8,
                AllergyNote = "Dairy",
                Tags = "healthy breakfast",
                ImageFileName = "berry_bowl.jpg"
            },
            new()
            {
                Id = 2,
                Name = "Chicken Rice Box",
                Category = "Lunch",
                Description = "Grilled chicken with brown rice",
                Calories = 520,
                Protein = 38,
                Carbs = 58,
                Fat = 14,
                AllergyNote = "None",
                Tags = "lunch protein",
                ImageFileName = "chicken_rice.jpg"
            },
            new()
            {
                Id = 3,
                Name = "Iced Americano",
                Category = "Drink",
                Description = "Black coffee",
                Calories = 15,
                Protein = 1,
                Carbs = 3,
                Fat = 0,
                AllergyNote = "Caffeine",
                Tags = "coffee drink",
                ImageFileName = "americano.jpg"
            }
        ];
    }

    public Task<List<FoodItem>> GetAllAsync() => Task.FromResult(_items.ToList());

    public Task<List<FoodItem>> SearchAsync(string? query)
    {
        if (string.IsNullOrWhiteSpace(query)) return Task.FromResult(_items.ToList());
        var lower = query.ToLower();
        var result = _items.Where(f => f.Name.ToLower().Contains(lower) ||
                                        f.Category.ToLower().Contains(lower) ||
                                        f.Tags.ToLower().Contains(lower)).ToList();
        return Task.FromResult(result);
    }

    public Task<FoodItem?> GetByIdAsync(int id) => Task.FromResult(_items.FirstOrDefault(f => f.Id == id));

    public Task AddAsync(FoodItem item)
    {
        var newId = _items.Count > 0 ? _items.Max(f => f.Id) + 1 : 1;
        item.Id = newId;
        _items.Add(item);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        var item = _items.FirstOrDefault(f => f.Id == id);
        if (item != null) _items.Remove(item);
        return Task.CompletedTask;
    }
}