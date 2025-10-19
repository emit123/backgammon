using Android.App;
using Android.OS;
using Android.Widget;
using Android.Content;

namespace Backgammon.Activities
{
    [Activity(Label = "Instructions")]
    public class InstructionsActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.instructions_activity);

            // Find the return button (make sure the button ID matches the one in your drawable/layout)
            Button btnReturn = FindViewById<Button>(Resource.Id.btnReturn);

            // Go back to the main menu when clicked
            btnReturn.Click += (sender, e) =>
            {
                Intent intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
                Finish();
            };
        }
    }
}
