using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PizzaShop_Course.ViewModel.Validation
{
    public class PhoneNumberValidation
    {
        public static bool IsValidUkrainianPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return false;
            try
            {
                // Use regular expression to check for valid Ukrainian phone number format
                Regex regex = new Regex(@"^\+380\d{9}$");
                return regex.IsMatch(phoneNumber.Trim());
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
