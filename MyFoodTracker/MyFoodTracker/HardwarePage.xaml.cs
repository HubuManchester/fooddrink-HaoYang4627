using MyFoodTracker.Services;
using System.Diagnostics;

namespace MyFoodTracker;

public partial class HardwarePage : ContentPage
{
    public HardwarePage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        AccessibilityService.ApplyFontScale(this);
    }

    protected override void OnDisappearing()
    {
        SpeechService.Stop();
        base.OnDisappearing();
    }

    private async void OnTakePhoto(object sender, EventArgs e)
    {
        try
        {
            var photo = await MediaPicker.Default.CapturePhotoAsync();
            if (photo != null)
            {
                var stream = await photo.OpenReadAsync();
                PhotoPreview.Source = ImageSource.FromStream(() => stream);
                StatusLabel.Text = "Photo taken successfully";
            }
            else
            {
                StatusLabel.Text = "Photo capture cancelled";
            }
        }
        catch (Exception ex)
        {
            StatusLabel.Text = "Failed to take photo, please grant camera permission";
            Debug.WriteLine(ex);
        }
    }

    private async void OnGetLocation(object sender, EventArgs e)
    {
        try
        {
            var location = await Geolocation.Default.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Medium));
            if (location != null)
            {
                var placemarks = await Geocoding.Default.GetPlacemarksAsync(location);
                var place = placemarks?.FirstOrDefault();
                LocationLabel.Text = place != null ? $"{place.CountryName} / {place.AdminArea} / {place.Locality}" : $"Lat {location.Latitude:F5}, Lon {location.Longitude:F5}";
                StatusLabel.Text = "Location obtained";
            }
            else
            {
                LocationLabel.Text = "Location not available";
            }
        }
        catch (Exception ex)
        {
            LocationLabel.Text = "Unable to get location, check permissions";
            Debug.WriteLine(ex);
        }
    }

    private async void OnReadHelp(object sender, EventArgs e)
    {
        await SpeechService.SpeakAsync("Track your foods, nutrition, and location.");
    }

    private void OnStopSpeech(object sender, EventArgs e)
    {
        SpeechService.Stop();
        StatusLabel.Text = "Speech stopped";
    }

    private void OnVibrate(object sender, EventArgs e)
    {
        try
        {
            Vibration.Default.Vibrate(500);
            HapticFeedback.Default.Perform(HapticFeedbackType.Click);
            StatusLabel.Text = "Vibration & haptic feedback triggered";
        }
        catch (Exception)
        {
            StatusLabel.Text = "Vibration not supported";
        }
    }
}