using FCG_MS_Users.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace FCG_MS_Users.Domain.ValueObjects;
/// <summary>
/// Represents a secure password with validation
/// </summary>
public sealed class Password
{
    public string HasedValue { get; }

    /// <summary>
    /// Used for EF Core
    /// </summary>
    private Password() { }
    public Password(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("Password cannot be empty");
        }

        if (!IsSecuredPassword(value))
        {
            throw new DomainException("Password must be at least 8 characters long and contain numbers, letters and special characters");
        }

        HasedValue = value;
    }

    private static bool IsSecuredPassword(string password)
    {
        if (password.Length < 8)
        {
            return false;
        }
        var hashNumber = new Regex(@"[0-9]+");
        var hasLetter = new Regex(@"[a-zA-Z]+");
        var hasSpecialChar = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

        return hashNumber.IsMatch(password) &&
               hasLetter.IsMatch(password) &&
               hasSpecialChar.IsMatch(password);
    }
}