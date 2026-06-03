namespace MyFoodTracker.Models;

public class FoodItem
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double Calories { get; set; }
    public double Protein { get; set; }
    public double Carbs { get; set; }
    public double Fat { get; set; }
    public string AllergyNote { get; set; } = string.Empty;
    public string Tags { get; set; } = string.Empty;
    public string ImageFileName { get; set; } = string.Empty;   // New property for image

    public string CaloriesLabel => $"{Calories} kcal";
    public string MacroSummary => $"P:{Protein}g C:{Carbs}g F:{Fat}g";
    public string AccessibleSummary => $"{Name}, {Category}, {CaloriesLabel}. {MacroSummary}. {AllergyNote}";
}