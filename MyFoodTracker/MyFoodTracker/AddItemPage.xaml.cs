using MyFoodTracker.Models;
using MyFoodTracker.Services;

namespace MyFoodTracker;

public partial class AddItemPage : ContentPage
{
    private readonly IFoodService _service;
    public AddItemPage(IFoodService service)
    {
        InitializeComponent();
        _service = service;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        AccessibilityService.ApplyFontScale(this);
    }

    private async void OnSave(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NameEntry.Text))
        {
            ShowError("Name is required");
            return;
        }
        if (CategoryPicker.SelectedIndex < 0)
        {
            ShowError("Please select a category");
            return;
        }
        if (!double.TryParse(CaloriesEntry.Text, out double cal) || cal < 0)
        {
            ShowError("Calories must be a non-negative number");
            return;
        }
        if (!double.TryParse(ProteinEntry.Text, out double protein) || protein < 0)
        {
            ShowError("Protein must be a non-negative number");
            return;
        }
        if (!double.TryParse(CarbsEntry.Text, out double carbs) || carbs < 0)
        {
            ShowError("Carbs must be a non-negative number");
            return;
        }
        if (!double.TryParse(FatEntry.Text, out double fat) || fat < 0)
        {
            ShowError("Fat must be a non-negative number");
            return;
        }

        var newItem = new FoodItem
        {
            Name = NameEntry.Text.Trim(),
            Category = CategoryPicker.SelectedItem.ToString()!,
            Description = DescEditor.Text ?? "",
            Calories = cal,
            Protein = protein,
            Carbs = carbs,
            Fat = fat,
            AllergyNote = string.IsNullOrWhiteSpace(AllergyEntry.Text) ? "None" : AllergyEntry.Text.Trim(),
            Tags = $"{NameEntry.Text} {CategoryPicker.SelectedItem}"
        };
        await _service.AddAsync(newItem);
        await Shell.Current.GoToAsync("..");
    }

    private void ShowError(string msg)
    {
        ErrorLabel.Text = msg;
        ErrorLabel.IsVisible = true;
        try { Vibration.Default.Vibrate(200); } catch { }
    }
}