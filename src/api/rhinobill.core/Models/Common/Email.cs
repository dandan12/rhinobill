using System.Text.RegularExpressions;

namespace rhinobill.core.Models.Common
{
    public class Email
    {
        private static readonly string emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

        public Email(string emailAddress)
        {
            if (string.IsNullOrEmpty(emailAddress))
                throw new ArgumentNullException(nameof(emailAddress));


            if (!Regex.IsMatch(emailAddress, emailRegex, RegexOptions.IgnoreCase))
                throw new Exception("Invalid email address format");

            EmailAddress = emailAddress;
        }

        public static Email ParseOrDefault(string emailAddress)
        {
            if (string.IsNullOrEmpty(emailAddress))
                return null;


            if (!Regex.IsMatch(emailAddress, emailRegex, RegexOptions.IgnoreCase))
                return null;

            return new(emailAddress);
        }

        public string EmailAddress { get; }

        public override string ToString()
        {
            return EmailAddress;
        }
    }
}
