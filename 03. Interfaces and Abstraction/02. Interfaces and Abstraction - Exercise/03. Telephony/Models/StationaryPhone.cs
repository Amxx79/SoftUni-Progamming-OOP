using Telephony.Models.Interfaces;

public class StationaryPhone : ICallable
{
    public string Call(string number)
    {
        if (!ValidatePhoneNumber(number))
        {
            throw new ArgumentException("Invalid number!");
        }

        return $"Dialing... {number}";
    }

    bool ValidatePhoneNumber(string phoneNumber)
    {
        return phoneNumber.All(c => char.IsDigit(c));
    }
}