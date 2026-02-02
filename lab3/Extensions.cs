using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab3;

public static class Extensions
{

    // Інвертування рядка
    public static string ReverseString(this string value)
    {
        char[] chars = value.ToCharArray();
        Array.Reverse(chars);
        return new string(chars);
    }

    // Підрахунок символів (використовує LINQ)
    public static int CountChar(this string value, char symbol)
    {
        return value.Count(c => c == symbol);
    }


    // Підрахунок значень (використовує LINQ)
    public static int CountOf<T>(this T[] array, T value)
        where T : IEquatable<T>
    {
        return array.Count(item => item.Equals(value));
    }

    // Унікальні елементи
    public static T[] Unique<T>(this T[] array)
        where T : IEquatable<T>
    {
        var result = new List<T>();

        foreach (var item in array)
            if (!result.Contains(item))
                result.Add(item);

        return result.ToArray();
    }
}