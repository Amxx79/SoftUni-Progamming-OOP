using Telephony.Models.Interfaces;

public class Smartphone : ICallable, IBrowsable
{
    public string Call(string number)
    {
        if (!ValidatePhoneNumber(number))
        {
            throw new ArgumentException("Invalid number!");
        }

        return $"Calling... {number}";
    }

    public string Browse(string url)
    {
        if (!ValidURL(url))
        {
            throw new ArgumentException("Invalid URL!");
        }

        return $"Browsing: {url}!";
    }
    bool ValidatePhoneNumber(string phoneNumber)
    {
        return phoneNumber.All(c => char.IsDigit(c));
    }

    bool ValidURL(string url)
    {
        return url.All(c => !char.IsDigit(c));
    }
}
