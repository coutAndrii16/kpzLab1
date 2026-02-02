using System;
using System.Collections.Generic;

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

    // Підрахунок символів
    public static int CountChar(this string value, char symbol)
    {
        int count = 0;
        foreach (var c in value)
            if (c == symbol)
                count++;
        return count;
    }


    // Підрахунок значень (узагальнений)
    public static int CountOf<T>(this T[] array, T value)
        where T : IEquatable<T>
    {
        int count = 0;
        foreach (var item in array)
            if (item.Equals(value))
                count++;
        return count;
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