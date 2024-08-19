﻿namespace Tooling;

public static class BaseExtensions
{
    public static void Finally<T>(this T self, Action<T> action) => action(self);

    public static R Then<T, R>(this T self, Func<T, R> func) => func(self);

    public static string StringJoin(this IEnumerable<string> seq, string separator) =>
        string.Join(separator, seq);

    public static void ForEach<T>(this IEnumerable<T> seq, Action<T> action)
    {
        foreach (T item in seq)
            action(item);
    }
}