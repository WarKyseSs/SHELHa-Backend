namespace Infrastructure.Utils;

public static class RandomKey
{
    private static readonly int KeyLength = 15;
    private static string _key;

    public static string GenerateKey()
    {
        _key = "";
        Random random = new Random();
        
        for(int i=1;i<=KeyLength;i++)
        {
            _key += random.Next(1, 9);
        }

        return _key;
    }
}