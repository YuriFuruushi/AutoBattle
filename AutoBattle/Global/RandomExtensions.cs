using System;

static class RandomExtensions
{
    //Simple and fast shuffle array
    public static void Shuffle<T>(this Random rng, T[] array)
    {
        int n = array.Length;
        while (n > 1)
        {
            int k = rng.Next(n--);
            T temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
    }

    //Easy Random Integer
    public static int GetRandomInt(int min, int max)
    {
        var rand = new Random();
        int index = rand.Next(min, max);
        return index;
    }

    //Easy Random Float
    public static float GetRandomFloat(float min, float max)
    {
        System.Random random = new System.Random();
        double val = (random.NextDouble() * (max - min) + min);
        val = Math.Round(val, 1);
        return (float)val;
    }
}
