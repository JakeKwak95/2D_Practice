public static class UtillityFunctions
{
    public static bool IsApproximatelySame(float a, float b, float tolerance = 0.01f)
    {
        return (a * (1 + tolerance) >= b && a * (1 - tolerance) <= b);
    }
}
