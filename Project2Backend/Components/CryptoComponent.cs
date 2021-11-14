using System.Security.Cryptography;

namespace Project2Backend.Components;


public class CryptoComponent
{
    private const int SaltSize = 32;
    private const int KeySize = 32;
    private const int Iterations = 10000;

    private readonly HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA512;

    public (string salt, string hashedPassword) HashPassword(string password)
    {
        using var algorithm = new Rfc2898DeriveBytes(password, SaltSize, Iterations, _hashAlgorithm);
        var salt = Convert.ToBase64String(algorithm.Salt);
        var hashedPassword = Convert.ToBase64String(algorithm.GetBytes(KeySize));

        return (salt, hashedPassword);
    }

    public string HashPassword(string valueToHash, string salt)
    {
        var saltBytes = Convert.FromBase64String(salt);

        using var algorithm = new Rfc2898DeriveBytes(valueToHash, saltBytes, Iterations, _hashAlgorithm);
        var hashedValue = Convert.ToBase64String(algorithm.GetBytes(KeySize));

        return hashedValue;
    }

    public bool Verify(string salt, string hashedPassword, string password)
    {
        using var algorithm = new Rfc2898DeriveBytes(password,
            Convert.FromBase64String(salt), Iterations, _hashAlgorithm);

        var passwordToVerify = algorithm.GetBytes(KeySize);
        var verified = passwordToVerify.SequenceEqual(Convert.FromBase64String(hashedPassword));

        return verified;
    }
}
