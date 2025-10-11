using Android.App;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using Android.Widget;
using Android.Content;

namespace Backgammon
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true, ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Hide the action bar & make fullscreen
            RequestWindowFeature(Android.Views.WindowFeatures.NoTitle);
            Window.AddFlags(Android.Views.WindowManagerFlags.Fullscreen);
            SupportActionBar?.Hide();

            SetContentView(Resource.Layout.activity_main);

            // --- BUTTON HANDLERS ---
            var btnSingle = FindViewById<Button>(Resource.Id.btnSinglePlayer);
            var btnSettings = FindViewById<Button>(Resource.Id.btnSettings);
            var btnExit = FindViewById<Button>(Resource.Id.btnExit);

            // Go to GameActivity when "Single Player" is clicked
            btnSingle.Click += (s, e) =>
            {
                var intent = new Intent(this, typeof(GameActivity));
                StartActivity(intent);
            };

            // Settings placeholder
            btnSettings.Click += (s, e) =>
            {
                Toast.MakeText(this, "Settings not yet implemented", ToastLength.Short).Show();
            };

            // Exit button
            btnExit.Click += (s, e) =>
            {
                FinishAffinity(); // closes the app cleanly
            };
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
