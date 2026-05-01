using System;
public static class Utils
{
    private static Random s_random = new Random();

    public static float GetRandomInRange(float min, float max)
    {
        float range = max - min;

        return (float)s_random.NextDouble() * range + min;
    }
}
