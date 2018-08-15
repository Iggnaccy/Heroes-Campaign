using System;
using System.Collections.Generic;

public class Utils {
    public static int getEnumLength<T>()
    {
        return ((T[])Enum.GetValues(typeof(T))).Length;
    }

    public static T generateRandomEnum<T>(Random rng)
    {
        T[] values = (T[])Enum.GetValues(typeof(T));
        return (T)values.GetValue(rng.Next(values.Length));
    }

    public static string markowNameGenerator(List<string> vec, int n, int prefLen, int maxLength)
    {
        if (prefLen < n || maxLength < prefLen)
        {
            return "ERROR!";
        }
        Dictionary<string, List<char>> m = new Dictionary<string, List<char>>();
        foreach (string str in vec)
        {
            string sub = str.Substring(0, n);
            if (!m.ContainsKey(sub))
                m[sub] = new List<char>();
            if (n == str.Length)
                m[sub].Add('_');
            else
                m[sub].Add(str[n]);
            for (int i = n; i < str.Length; i++)
            {
                sub += str[i];
                sub = sub.Substring(1);
                if (!m.ContainsKey(sub))
                    m[sub] = new List<char>();
                if (i + 1 == str.Length)
                    m[sub].Add('_');
                else
                    m[sub].Add(str[i + 1]);
            }
        }
        string ret = vec[StaticValues.rng.Next() % vec.Count].Substring(0, prefLen);
        while (true)
        {
            string s = ret.Substring(ret.Length - n, n);
            char c = m[s][StaticValues.rng.Next() % m[s].Count];
            if (c == '_' || ret.Length >= maxLength)
                break;
            ret += c;
        }
        return ret;
    }
}
