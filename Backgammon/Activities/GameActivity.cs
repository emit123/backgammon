using Android.App;
using Android.OS;
using Android.Widget;
using Android.Util;
using Android.Graphics;
using Android.Views;
using System.Collections.Generic;
using Android.Content;

namespace Backgammon
{
    [Activity(Label = "Game", Theme = "@style/AppTheme",
        ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape)]
    public class GameActivity : Activity
    {
        FrameLayout checkerLayer;
        float layerW, layerH;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.game_activity);

            checkerLayer = FindViewById<FrameLayout>(Resource.Id.checkerLayer);
            checkerLayer.LayoutDirection = Android.Views.LayoutDirection.Ltr;

            // Wait for layout to measure before positioning items
            checkerLayer.Post(() =>
            {
                layerW = checkerLayer.Width;
                layerH = checkerLayer.Height;

                PlaceStartingCheckers();
                AddTurnUI(); // 👈 draw the buttons, dice, and label
            });
        }

        // ---------- BOARD CHECKER SETUP ----------
        void PlaceStartingCheckers()
        {
            // Standard backgammon setup
            var setup = new Dictionary<int, (int count, bool isWhite)>
            {
                { 0,  (2, false) },  // Black 2 (top right)
                { 11, (5, false)  },  // White 5 (top left)
                { 16, (3, false)  },  // White 3 (bottom left mid)
                { 18, (5, false) },  // Black 5 (bottom right mid)
                { 23, (2, true)  },  // White 2 (bottom right)
                { 7,  (3, true) },  // Black 5 (top left mid)
                { 12, (5, true)  },  // White 5 (bottom left)
                { 5,  (5, true) }   // Black 3 (top right mid)
            };

            foreach (var kvp in setup)
            {
                int pipIndex = kvp.Key;
                int count = kvp.Value.count;
                bool isWhite = kvp.Value.isWhite;

                for (int i = 0; i < count; i++)
                    AddChecker(pipIndex, isWhite, i);
            }
        }

        void AddChecker(int pipIndex, bool isWhite, int stackIndex)
        {
            var checker = new ImageView(this);
            checker.SetImageResource(isWhite ? Resource.Drawable.white_checker
                                             : Resource.Drawable.black_checker);

            int sizePx = (int)TypedValue.ApplyDimension(
                ComplexUnitType.Dip, 42, Resources.DisplayMetrics);

            var lp = new FrameLayout.LayoutParams(sizePx, sizePx)
            {
                Gravity = GravityFlags.Left | GravityFlags.Top
            };

            PointF center = BoardGeometry.PipCenters[pipIndex];
            float xPx = center.X * layerW;
            float yPx = center.Y * layerH;

            float stackOffset = BoardGeometry.StackOffsetPx(sizePx);

            // Top points stack downward; bottom points upward
            if (BoardGeometry.IsTop(pipIndex))
                yPx += stackIndex * stackOffset;
            else
                yPx -= stackIndex * stackOffset;

            lp.LeftMargin = (int)(xPx - sizePx / 2f);
            lp.TopMargin = (int)(yPx - sizePx / 2f);

            checker.SetScaleType(ImageView.ScaleType.FitXy);
            checkerLayer.AddView(checker, lp);
        }

        // ---------- TURN UI CREATION ----------
        void AddTurnUI()
        {
            // === Turn label ===
            var lblTurn = new TextView(this);
            lblTurn.Text = "Black's turn";
            lblTurn.SetTextColor(Color.Black);
            lblTurn.TextSize = 26;
            lblTurn.Typeface = Typeface.DefaultBold;

            var lpLabel = LayoutAt(UIPositions.TurnLabel, 0, 0);
            checkerLayer.AddView(lblTurn, lpLabel);

            // === Buttons (Roll / End Turn) ===
            int buttonSize = (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, 110, Resources.DisplayMetrics);
            AddImageButton(Resource.Drawable.roll_dice, UIPositions.RollDice, buttonSize);
            AddImageButton(Resource.Drawable.end_turn, UIPositions.EndTurn, buttonSize);

            // === Dice (demo values 2 and 5) ===
            int diceSize = (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, 60, Resources.DisplayMetrics);
            AddImage(Resource.Drawable._6, UIPositions.Dice1, diceSize);
            AddImage(Resource.Drawable._5, UIPositions.Dice2, diceSize);
        }

        // ---------- HELPER METHODS ----------
        FrameLayout.LayoutParams LayoutAt(PointF pos, int width, int height)
        {
            var lp = new FrameLayout.LayoutParams(
                width == 0 ? ViewGroup.LayoutParams.WrapContent : width,
                height == 0 ? ViewGroup.LayoutParams.WrapContent : height)
            {
                Gravity = GravityFlags.Left | GravityFlags.Top
            };

            lp.LeftMargin = (int)(pos.X * layerW);
            lp.TopMargin = (int)(pos.Y * layerH);
            return lp;
        }

        void AddImageButton(int drawableId, PointF pos, int sizePx)
        {
            var button = new ImageButton(this);
            button.SetImageResource(drawableId);
            button.SetBackgroundColor(Color.Transparent);
            button.SetScaleType(ImageView.ScaleType.FitXy);

            var lp = new FrameLayout.LayoutParams(sizePx, sizePx)
            {
                Gravity = GravityFlags.Left | GravityFlags.Top
            };

            lp.LeftMargin = (int)(pos.X * layerW - sizePx / 2f);
            lp.TopMargin = (int)(pos.Y * layerH - sizePx / 2f);

            checkerLayer.AddView(button, lp);
        }

        void AddImage(int drawableId, PointF pos, int sizePx)
        {
            var image = new ImageView(this);
            image.SetImageResource(drawableId);
            image.SetScaleType(ImageView.ScaleType.FitXy);

            var lp = new FrameLayout.LayoutParams(sizePx, sizePx)
            {
                Gravity = GravityFlags.Left | GravityFlags.Top
            };

            lp.LeftMargin = (int)(pos.X * layerW - sizePx / 2f);
            lp.TopMargin = (int)(pos.Y * layerH - sizePx / 2f);

            checkerLayer.AddView(image, lp);
        }

        //Menu
        public override bool OnCreateOptionsMenu(Android.Views.IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu1, menu);

            return true;

        }
        public override bool OnOptionsItemSelected(Android.Views.IMenuItem item)

        {

            if (item.ItemId == Resource.Id.MainMenu)
            {
                var intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
                Finish();

                return true;

            }

            else if (item.ItemId == Resource.Id.Settings)
            {
                var intent = new Intent(this, typeof(SettingsActivity));
                StartActivity(intent);
                Finish();

                return true;
            }
            else if (item.ItemId == Resource.Id.Restart)
            {
                var intent = new Intent(this, typeof(GameActivity));
                StartActivity(intent);
                Finish();

                return true;
            }

            return base.OnOptionsItemSelected(item);

        }
        

    }

}
