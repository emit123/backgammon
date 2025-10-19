using Android.App;
using Android.OS;
using Android.Widget;
using Android.Content;

namespace Backgammon
{
    [Activity(Label = "Settings", Theme = "@style/AppTheme",
        ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape)]
    public class SettingsActivity : Activity
    {
        ImageButton btnReturn;
        Switch switchSound;
        Switch switchMusic;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Load the layout
            SetContentView(Resource.Layout.settings_activity);

            // Find all UI elements
            btnReturn = FindViewById<ImageButton>(Resource.Id.btnReturn);
            switchSound = FindViewById<Switch>(Resource.Id.switchSound);
            switchMusic = FindViewById<Switch>(Resource.Id.switchMusic);

            // Handle the return button (go back to main menu)
            btnReturn.Click += (s, e) =>
            {
                var intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
                Finish();
            };
        }
    }
}
