using System;

public class Utils {

    public static T generateRandomEnum<T>(Random rng)
    {
        T[] values = (T[])Enum.GetValues(typeof(T));
        return (T)values.GetValue(rng.Next(values.Length));
    }
}
