using System;

namespace Autorization_Microservice.Secure;
using Autorization_Microservice.Models;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Numerics;
using System.Text;

public static class SecurePsw
{
    public static void CreateHashSalt(UserModel user, string password) 
    {
        byte[] mySalt;
        user.Hash = HashPassword(password, out mySalt);
        user.Salt = mySalt;
    }

    public static byte[] HashPassword(string password, out byte[] salt)
    {
        const int keySize = 20;
        const int iterations = 350000;
        HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

        salt = RandomNumberGenerator.GetBytes(keySize);

        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            iterations,
            hashAlgorithm,
            keySize
            );

        return hash;
    }

    public static bool VerifyPassword(string password, byte[] hash, byte[] salt)
    {
        const int keySize = 20;
        const int iterations = 350000;
        HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

        var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);

        return hashToCompare.SequenceEqual(hash);
    }
}
