using Android.App;
using Android.OS;
using AndroidX.AppCompat.App;
using Android.Widget;
using Android.Content;
using Backgammon.Activities;
using AndroidX.AppCompat.Widget; // Toolbar

namespace Backgammon
{
    [Activity(Label = "@string/app_name",
              MainLauncher = true,
              ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            // Set up Toolbar
            var toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.topAppBar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Backgammon";

            var btnSingle = FindViewById<Button>(Resource.Id.btnSinglePlayer);
            var btnSettings = FindViewById<Button>(Resource.Id.btnSettings);
            var btnExit = FindViewById<Button>(Resource.Id.btnExit);
            var btnInstructions = FindViewById<Button>(Resource.Id.btnInstructions);

            btnSingle.Click += (s, e) => StartActivity(new Intent(this, typeof(GameActivity)));
            btnSettings.Click += (s, e) => StartActivity(new Intent(this, typeof(SettingsActivity)));
            btnInstructions.Click += (s, e) => StartActivity(new Intent(this, typeof(InstructionsActivity)));
            btnExit.Click += (s, e) => FinishAffinity();
        }

        public override bool OnCreateOptionsMenu(Android.Views.IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu1, menu); // Use your existing menu1.xml
            return true;
        }

        public override bool OnOptionsItemSelected(Android.Views.IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.settings: // adjust IDs to your menu1.xml
                    StartActivity(new Intent(this, typeof(SettingsActivity)));
                    return true;
                case Resource.Id.instruction:
                    StartActivity(new Intent(this, typeof(InstructionsActivity)));
                    return true;
                case Resource.Id.start_game:
                    StartActivity(new Intent(this, typeof(GameActivity)));
                    return true;

            }
            return base.OnOptionsItemSelected(item);
        }
    }
}