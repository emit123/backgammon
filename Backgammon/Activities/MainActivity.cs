using Android.App;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using Android.Widget;
using Android.Content;
using Backgammon.Activities;

namespace Backgammon
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true,
        ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Fullscreen
            RequestWindowFeature(Android.Views.WindowFeatures.NoTitle);
            Window.AddFlags(Android.Views.WindowManagerFlags.Fullscreen);
            SupportActionBar?.Hide();

            SetContentView(Resource.Layout.activity_main);

            var btnSingle = FindViewById<Button>(Resource.Id.btnSinglePlayer);
            var btnSettings = FindViewById<Button>(Resource.Id.btnSettings);
            var btnExit = FindViewById<Button>(Resource.Id.btnExit);
            var btnInstructions = FindViewById<Button>(Resource.Id.btnInstructions);



            btnSingle.Click += (s, e) =>
            {
                var intent = new Intent(this, typeof(GameActivity));
                StartActivity(intent);
            };

            btnSettings.Click += (s, e) =>
            {
                var intent = new Intent(this, typeof(SettingsActivity));
                StartActivity(intent);
            };

            
            btnInstructions.Click += (s, e) =>
            {
                Intent intent = new Intent(this, typeof(InstructionsActivity));
                StartActivity(intent);
            };


            btnExit.Click += (s, e) =>
            {
                FinishAffinity();
            };

         
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
