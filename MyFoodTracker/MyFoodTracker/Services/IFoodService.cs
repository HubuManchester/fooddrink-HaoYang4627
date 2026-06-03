using MyFoodTracker.Models;

namespace MyFoodTracker.Services;

public interface IFoodService
{
    Task<List<FoodItem>> GetAllAsync();
    Task<List<FoodItem>> SearchAsync(string? query);
    Task<FoodItem?> GetByIdAsync(int id);
    Task AddAsync(FoodItem item);
    Task DeleteAsync(int id);
}