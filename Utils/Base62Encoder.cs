using System.Text;

namespace URL_Shortener.Utils;

public static class Base62Encoder
{
    private const string Base62Chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

    public static string Encode(long number)
    {
        if (number == 0) return Base62Chars[0].ToString();

        var encoded = new StringBuilder();
        while (number > 0)
        {
            var remainder = (int)(number % 62);
            encoded.Insert(0, Base62Chars[remainder]);
            number = number / 62;
        }

        return encoded.ToString();
    }

    public static long GenerateUniqueNumber(string input)
    {
        // Generate a hash-based unique number from the input string
        return Math.Abs(input.GetHashCode());
    }
}