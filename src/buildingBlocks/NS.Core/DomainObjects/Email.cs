using System.Text.RegularExpressions;

namespace NS.Core.DomainObjects
{
    public class Email
    {
        public const int MaxLength = 254;
        public const int MinLength = 5;

        public string Address { get; private set; }

        /*Remeber: This constructor is exclusive for EF*/
        public Email()
        {
        }

        public Email(string address)
        {
            if (!Validate(address))
            {
                throw new DomainException("E-mail address is invalid");
            }

            Address = address;
        }

        public static bool Validate(string address)
        {
            var regexEmail = new Regex(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
            
            return regexEmail.IsMatch(address);
        }
    }
}