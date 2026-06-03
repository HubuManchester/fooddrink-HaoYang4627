using MyFoodTracker.Models;
using MyFoodTracker.Services;

namespace MyFoodTracker;

[QueryProperty(nameof(ItemId), "id")]
public partial class DetailPage : ContentPage
{
    private readonly IFoodService _service;
    private FoodItem? _item;
    private int _id;
    public string ItemId { set => _id = int.Parse(value); }

    public DetailPage(IFoodService service)
    {
        InitializeComponent();
        _service = service;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        _item = await _service.GetByIdAsync(_id);
        if (_item != null)
        {
            NameLabel.Text = _item.Name;
            CategoryLabel.Text = _item.Category;
            CaloriesLabel.Text = _item.CaloriesLabel;
            MacroLabel.Text = _item.MacroSummary;
            DescLabel.Text = _item.Description;
            AllergyLabel.Text = _item.AllergyNote;

            // Load image from Resources/Images
            if (!string.IsNullOrWhiteSpace(_item.ImageFileName))
            {
                try
                {
                    FoodImage.Source = ImageSource.FromFile(_item.ImageFileName);
                }
                catch
                {
                    FoodImage.Source = "placeholder.png";
                }
            }
            else
            {
                FoodImage.Source = "placeholder.png";
            }
        }
        AccessibilityService.ApplyFontScale(this);
    }

    protected override void OnDisappearing()
    {
        SpeechService.Stop();
        base.OnDisappearing();
    }

    private async void OnSpeak(object sender, EventArgs e)
    {
        if (_item != null) await SpeechService.SpeakAsync(_item.AccessibleSummary);
    }

    private void OnStop(object sender, EventArgs e) => SpeechService.Stop();

    private void OnVibrate(object sender, EventArgs e)
    {
        try { Vibration.Default.Vibrate(500); } catch { }
    }

    private async void OnDelete(object sender, EventArgs e)
    {
        if (_item == null) return;
        bool confirm = await DisplayAlert("Confirm Delete", $"Delete '{_item.Name}' ?", "Yes", "No");
        if (!confirm) return;
        await _service.DeleteAsync(_id);
        await Shell.Current.GoToAsync("..");
    }
}