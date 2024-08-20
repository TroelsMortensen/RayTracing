namespace Tooling;

public static class BaseExtensions
{
    public static void Finally<T>(this T self, Action<T> action) => action(self);

    // ReSharper disable once InconsistentNaming
    public static R Then<T, R>(this T self, Func<T, R> func) => func(self);

    public static IEnumerable<R> Then<T, R>(this IEnumerable<T> seq, Func<T, R> func) =>
        seq.Select(func);

    public static IEnumerable<R> Then<T, R>(this IEnumerable<T> seq, Func<T, int, R> func) =>
        seq.Select(func);

    public static string StringJoin(this IEnumerable<string> seq, string separator) =>
        string.Join(separator, seq);

    public static void ForEach<T>(this IEnumerable<T> seq, Action<T> action)
    {
        foreach (T item in seq)
            action(item);
    }
}