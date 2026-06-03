using MyFoodTracker.Models;
using MyFoodTracker.Services;

namespace MyFoodTracker;

public partial class MainPage : ContentPage
{
    private readonly IFoodService _service;
    public MainPage(IFoodService service)
    {
        InitializeComponent();
        _service = service;
        LoadData();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadData(SearchBar.Text);
        AccessibilityService.ApplyFontScale(this);
    }

    private async Task LoadData(string? query = null)
    {
        var items = await _service.SearchAsync(query);
        FoodList.ItemsSource = items;
    }

    private async void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        await LoadData(e.NewTextValue);
    }

    private async void OnRefresh(object sender, EventArgs e)
    {
        await LoadData(SearchBar.Text);
        ((RefreshView)sender).IsRefreshing = false;
    }

    private async void OnAddClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AddItemPage));
    }

    private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is FoodItem selected)
        {
            await Shell.Current.GoToAsync($"{nameof(DetailPage)}?id={selected.Id}");
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}