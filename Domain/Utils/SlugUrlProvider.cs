using System.Text;
using System.Text.RegularExpressions;

namespace Infrastructure.Utils;

public class SlugUrlProvider
{
    public string ToUrlSlug(string value)
    {
        //First to lower case
        value = value.ToLowerInvariant();

        //Remove all accents
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        var bytes = Encoding.GetEncoding("Cyrillic").GetBytes(value);
        value = Encoding.ASCII.GetString(bytes);

        //Replace spaces
        value = Regex.Replace(value, @"\s", "-", RegexOptions.Compiled);

        //Remove invalid chars
        value = Regex.Replace(value, @"[^a-z0-9\s-]", "",RegexOptions.Compiled);

        //Trim dashes from end
        value = value.Trim('-', ' ');

        //Replace double occurences of - or 
        value = Regex.Replace(value, @"([-]){2,}", "$1", RegexOptions.Compiled);

        return value ;
    }
}