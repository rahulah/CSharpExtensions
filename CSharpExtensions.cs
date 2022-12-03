namespace Proinfocus;

public static class CSharpExtensions
{
    public static bool IsValid<T>(this T? value) => (value is not null);

    public static void Dump<T>(this T value) => Console.Write(value);

    public static void DumpLine<T>(this T value) => Console.WriteLine(value);

    public static T When<T>(this T value, Func<T, bool> predicate, Action<T> action)
    {
        if (value is null || predicate(value))
            action.Invoke(value);

        return value;
    }

    public static void Loop(int from, int upto, Action<int> action)
    {
        for (int i = from; i < upto; i++)
            action.Invoke(i);
    }

    public static void LoopOver<T>(this IEnumerable<T> items, Action<T> action)
    {
        foreach (var item in items)
            action(item);
    }

    public static bool IsEmail(this string email)
    {
        if (string.IsNullOrWhiteSpace(email)) return false;
        const string validLetters = "abcdefghijklmnopqrstuvwxyz";
        const string validNumbers = "1234567890";
        const string validSymbols = "@.";
        var emailParts = email.ToLower().Split('@');
        if (emailParts.Length != 2) return false;
        if (!emailParts[1].Contains(".") || emailParts[1].EndsWith(".")) return false;
        if (!validLetters.Contains(emailParts[0][0])) return false;
        if (!(validLetters+validNumbers).Contains(emailParts[1][0])) return false;
        if (validSymbols.Contains(emailParts[0][emailParts[0].Length - 1])) return false;
        if (validSymbols.Contains(emailParts[1][emailParts[1].Length - 1])) return false;
        return true;
    }

    public static void Log(this IDictionary<string, string> errors, string name, string message)
        => errors.TryAdd(name, message);

    public static bool Range<T>(this T? value, int min, int max)
    {
        if (value is null) return false;
        var valueType = typeof(T);
        switch (valueType.Name) {
            case nameof(System.Single): return Single.Parse(value?.ToString()!) >= min && Single.Parse(value?.ToString()!) <= max;
            case nameof(System.Int32): return int.Parse(value?.ToString()!) >= min && int.Parse(value?.ToString()!) <= max;
            case nameof(System.Int64): return long.Parse(value?.ToString()!) >= min && long.Parse(value?.ToString()!) <= max;
            case nameof(System.Decimal): return decimal.Parse(value?.ToString()!) >= min && decimal.Parse(value?.ToString()!) <= max;
            case nameof(System.Double): return double.Parse(value?.ToString()!) >= min && double.Parse(value?.ToString()!) <= max;
            default: return value?.ToString()!.Length >= min && value?.ToString()!.Length <= max;
        }
    }
}