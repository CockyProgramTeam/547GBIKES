using System;
using System.Text.RegularExpressions;
namespace Enterpriseservices;

    public static class CreditCardValidator
    {
        // Regex for basic format (Visa, MasterCard, Amex, Discover)
        private static readonly Regex cardRegex = new Regex(@"^[0-9]{13,19}$");

        /// <summary>
        /// Validates a credit card number using format check and Luhn algorithm.
        /// </summary>
        /// <param name="cardNumber">Credit card number as string</param>
        /// <returns>True if valid, False otherwise</returns>
        public static bool IsValidCardNumber(string cardNumber)
        {
            if (string.IsNullOrWhiteSpace(cardNumber))
                return false;

            // Remove spaces and dashes
            cardNumber = cardNumber.Replace(" ", "").Replace("-", "");

            // Check format
            if (!cardRegex.IsMatch(cardNumber))
                return false;

            // Apply Luhn Algorithm
            return LuhnCheck(cardNumber);
        }

        private static bool LuhnCheck(string cardNumber)
        {
            int sum = 0;
            bool alternate = false;

            for (int i = cardNumber.Length - 1; i >= 0; i--)
            {
                int n = int.Parse(cardNumber[i].ToString());

                if (alternate)
                {
                    n *= 2;
                    if (n > 9)
                        n -= 9;
                }

                sum += n;
                alternate = !alternate;
            }

            return (sum % 10 == 0);
        }

        /// <summary>
        /// Detects card type based on prefix.
        /// </summary>
        public static string GetCardType(string cardNumber)
        {
            cardNumber = cardNumber.Replace(" ", "").Replace("-", "");

            if (cardNumber.StartsWith("4"))
                return "Visa";
            else if (Regex.IsMatch(cardNumber, @"^(51|52|53|54|55)"))
                return "MasterCard";
            else if (Regex.IsMatch(cardNumber, @"^(34|37)"))
                return "American Express";
            else if (Regex.IsMatch(cardNumber, @"^(6011|65)"))
                return "Discover";
            else
                return "Unknown";
        }
    }

    /*// Example usage
    class Program
    {
        static void Main()
        {
            string cardNumber = "4111111111111111"; // Example Visa test number
            bool isValid = CreditCardValidator.IsValidCardNumber(cardNumber);
            string cardType = CreditCardValidator.GetCardType(cardNumber);

            Console.WriteLine(isValid
                ? $"Credit card number is valid ({cardType})."
                : "Credit card number is invalid.");
        }
    }*/

