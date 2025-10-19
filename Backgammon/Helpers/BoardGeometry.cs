using Android.Graphics;

namespace Backgammon
{
    public static class BoardGeometry
    {
        // Normalized coordinates (0..1)
        // 0–11 top row (right → left), 12–23 bottom row (left → right)
        public static readonly PointF[] PipCenters = new PointF[]
        {
            // Top row
             new PointF(0.9201f, 0.10f),
            new PointF(0.8505f, 0.10f),
            new PointF(0.78f, 0.10f),
            new PointF(0.70f, 0.10f),
            new PointF(0.630f, 0.10f),
            new PointF(0.555f, 0.10f),
            new PointF(0.455f, 0.10f),
            new PointF(0.380f, 0.10f),
            new PointF(0.305f, 0.10f),
            new PointF(0.230f, 0.10f),
            new PointF(0.155f, 0.10f),
            new PointF(0.080f, 0.10f),

            // Bottom row
            new PointF(0.080f, 0.9f),
            new PointF(0.155f, 0.9f),
            new PointF(0.230f, 0.9f),
            new PointF(0.305f, 0.9f),
            new PointF(0.380f, 0.9f),
            new PointF(0.455f, 0.9f),
            new PointF(0.555f, 0.9f),
            new PointF(0.630f, 0.9f),
            new PointF(0.70f, 0.9f),
            new PointF(0.78f, 0.9f),
            new PointF(0.8505f, 0.9f),
            new PointF(0.9201f, 0.9f)
        };

        public static float StackOffsetPx(int checkerPx) => checkerPx * 0.62f;
        public static bool IsTop(int pipIndex) => pipIndex < 12;
    }

    public static class UIPositions
    {
        // Dice on the left side (centered vertically)
        public static readonly PointF Dice1 = new PointF(0.17f, 0.52f);
        public static readonly PointF Dice2 = new PointF(0.26f, 0.52f);

        // Label and buttons on the right side
        public static readonly PointF TurnLabel = new PointF(0.51f, 0.45f);
        public static readonly PointF RollDice = new PointF(0.77f, 0.52f);
        public static readonly PointF EndTurn = new PointF(0.90f, 0.52f);
    }


}
