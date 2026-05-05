using System;
using System.Security.Cryptography;

var password = args.Length > 0 ? args[0] : "Admin123!";
var salt = RandomNumberGenerator.GetBytes(16);
var key = Rfc2898DeriveBytes.Pbkdf2(password, salt, 100_000, HashAlgorithmName.SHA256, 32);
Console.WriteLine($"pbkdf2$100000${Convert.ToBase64String(salt)}${Convert.ToBase64String(key)}");
