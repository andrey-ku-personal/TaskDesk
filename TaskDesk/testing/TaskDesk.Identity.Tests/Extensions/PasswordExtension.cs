using Bogus.DataSets;

namespace TaskDesk.Identity.Tests.Extensions;

public static class PasswordExtension
{
    public static string GeneratePassword(
        this Internet internet,
        int minLength,
        int maxLength,
        bool includeUppercase = true,
        bool includeNumber = true,
        bool includeSymbol = true)
    {
        if (internet == null) throw new ArgumentNullException(nameof(internet));
        if (minLength < 1) throw new ArgumentOutOfRangeException(nameof(minLength));
        if (maxLength < minLength) throw new ArgumentOutOfRangeException(nameof(maxLength));

        var r = internet.Random;
        var s = "";

        s += r.Char('a', 'z').ToString();
        if (s.Length < maxLength && includeUppercase) s += r.Char('A', 'Z').ToString();
        if (s.Length < maxLength && includeNumber) s += r.Char('0', '9').ToString();
        if (s.Length < maxLength && includeSymbol) s += r.Char('!', '/').ToString();
        if (s.Length < minLength) s += r.String2(minLength - s.Length);
        if (s.Length < maxLength) s += r.String2(r.Number(0, maxLength - s.Length));

        var chars = s.ToArray();
        var charsShuffled = r.Shuffle(chars).ToArray();

        return new string(charsShuffled);
    }
}
