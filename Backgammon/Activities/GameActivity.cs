using Android.App;
using Android.OS;
using Android.Widget;
using Android.Util;
using Android.Graphics;
using System.Collections.Generic;

namespace Backgammon
{
    [Activity(Label = "Game", Theme = "@style/AppTheme",
        ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape)]
    public class GameActivity : Activity
    {
        FrameLayout checkerLayer;
        float layerW, layerH; // measured size

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.game_activity);

            checkerLayer = FindViewById<FrameLayout>(Resource.Id.checkerLayer);

            // Wait until layout is ready before placing checkers
            checkerLayer.Post(() =>
            {
                layerW = checkerLayer.Width;
                layerH = checkerLayer.Height;
                PlaceStartingCheckers();
            });
        }

        // Standard backgammon starting setup (indices 0–23)
        void PlaceStartingCheckers()
        {
            var setup = new Dictionary<int, (int count, bool isWhite)>
            {
                { 0,  (2, false) },
                { 11, (5, true)  },
                { 16, (3, true)  },
                { 18, (5, false) },
                { 23, (2, true)  },
                { 7,  (5, false) },
                { 12, (5, true)  },
                { 5,  (3, false) }
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

            // Checker size
            int sizePx = (int)TypedValue.ApplyDimension(
                ComplexUnitType.Dip, 48, Resources.DisplayMetrics);

            var lp = new FrameLayout.LayoutParams(sizePx, sizePx);

            // Normalized board coordinates
            PointF center = BoardGeometry.PipCenters[pipIndex];

            // Adjust horizontal range to actual board area (you can fine-tune these)
            float boardLeft = 0.15f;  // left edge of playable board
            float boardRight = 0.85f; // right edge of playable board
            float boardWidth = boardRight - boardLeft;

            float xPx = (boardLeft + center.X * boardWidth) * layerW;
            float yPx = center.Y * layerH;

            float stackOffset = BoardGeometry.StackOffsetPx(sizePx);

            // Top points stack downward; bottom points upward
            if (BoardGeometry.IsTop(pipIndex))
                yPx += stackIndex * stackOffset;
            else
                yPx -= stackIndex * stackOffset;

            lp.LeftMargin = (int)(xPx - sizePx / 2f);
            lp.TopMargin = (int)(yPx - sizePx / 2f);

            checkerLayer.AddView(checker, lp);
        }
    }
}
