using MyFoodTracker.Services;

namespace MyFoodTracker;

public partial class SettingsPage : ContentPage
{
    public SettingsPage()
    {
        InitializeComponent();
        ThemePicker.SelectedIndex = 0;
        LargeTextSwitch.IsToggled = AccessibilityService.LargeTextEnabled;
    }
    protected override void OnAppearing() => AccessibilityService.ApplyFontScale(this);

    private void OnThemeChanged(object sender, EventArgs e)
    {
        Application.Current!.UserAppTheme = ThemePicker.SelectedIndex switch
        {
            1 => AppTheme.Light,
            2 => AppTheme.Dark,
            _ => AppTheme.Unspecified
        };
    }
    private void OnLargeTextToggled(object sender, ToggledEventArgs e)
    {
        AccessibilityService.LargeTextEnabled = e.Value;
        AccessibilityService.ApplyFontScale(this);
    }
}