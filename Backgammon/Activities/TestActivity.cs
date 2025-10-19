using Android.App;
using Android.OS;
using Android.Widget;
using Android.Util;
using Android.Graphics;
using Android.Views;

namespace Backgammon
{
    [Activity(Label = "Test Board", Theme = "@style/AppTheme",
        ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape)]
    public class TestActivity : Activity
    {
        FrameLayout checkerLayer;
        float layerW, layerH;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.test_activity);

            checkerLayer = FindViewById<FrameLayout>(Resource.Id.checkerLayer);
            checkerLayer.LayoutDirection = Android.Views.LayoutDirection.Ltr;

            checkerLayer.Post(() =>
            {
                layerW = checkerLayer.Width;
                layerH = checkerLayer.Height;
                PlaceTestCheckers();
            });
        }

        void PlaceTestCheckers()
        {
            for (int pip = 0; pip < BoardGeometry.PipCenters.Length; pip++)
            {
                // Two checkers per point
                AddChecker(pip, true, 0);
                AddChecker(pip, false, 1);
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

            if (BoardGeometry.IsTop(pipIndex))
                yPx += stackIndex * stackOffset;
            else
                yPx -= stackIndex * stackOffset;

            lp.LeftMargin = (int)(xPx - sizePx / 2f);
            lp.TopMargin = (int)(yPx - sizePx / 2f);

            checker.SetScaleType(ImageView.ScaleType.FitXy);
            checkerLayer.AddView(checker, lp);
        }
    }
}
