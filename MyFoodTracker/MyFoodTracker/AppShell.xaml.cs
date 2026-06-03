namespace MyFoodTracker;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(AddItemPage), typeof(AddItemPage));
        Routing.RegisterRoute(nameof(DetailPage), typeof(DetailPage));
    }
}