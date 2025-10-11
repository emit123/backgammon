using Android.Graphics;

namespace Backgammon
{
    public static class BoardGeometry
    {
        // Normalized coordinates (0..1)
        // 0–11 top row, right → left
        // 12–23 bottom row, left → right
        public static readonly PointF[] PipCenters = new PointF[]
        {
            // Top row (right → left)
            new PointF(0.94f, 0.12f),  // 0
            new PointF(0.865f, 0.12f), // 1
            new PointF(0.790f, 0.12f), // 2
            new PointF(0.715f, 0.12f), // 3
            new PointF(0.640f, 0.12f), // 4
            new PointF(0.565f, 0.12f), // 5
            new PointF(0.435f, 0.12f), // 6
            new PointF(0.360f, 0.12f), // 7
            new PointF(0.285f, 0.12f), // 8
            new PointF(0.210f, 0.12f), // 9
            new PointF(0.135f, 0.12f), // 10
            new PointF(0.060f, 0.12f), // 11

            // Bottom row (left → right)
            new PointF(0.060f, 0.88f), // 12
            new PointF(0.135f, 0.88f), // 13
            new PointF(0.210f, 0.88f), // 14
            new PointF(0.285f, 0.88f), // 15
            new PointF(0.360f, 0.88f), // 16
            new PointF(0.435f, 0.88f), // 17
            new PointF(0.565f, 0.88f), // 18
            new PointF(0.640f, 0.88f), // 19
            new PointF(0.715f, 0.88f), // 20
            new PointF(0.790f, 0.88f), // 21
            new PointF(0.865f, 0.88f), // 22
            new PointF(0.94f, 0.88f),  // 23
        };

        public static float StackOffsetPx(int checkerPx) => checkerPx * 0.62f;
        public static bool IsTop(int pipIndex) => pipIndex < 12;
    }
}
