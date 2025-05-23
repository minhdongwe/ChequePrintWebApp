namespace ChequePrintWebApp.Helper
{
    public static class NumberToWordsConverter
    {
        private static readonly string[] Units = { "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };
        private static readonly string[] Teens = { "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
        private static readonly string[] Tens = { "", "", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
        private static readonly string[] Thousands = { "", "Thousand", "Lakh", "Crore" };

        public static string ConvertToWords(decimal number)
        {
            if (number == 0)
                return "Zero Rupees Only";

            long intPart = (long)Math.Floor(number);
            int decimalPart = (int)((number - intPart) * 100);

            string words = $"{ConvertWholeNumber(intPart)} Rupees";

            if (decimalPart > 0)
                words += $" and {ConvertWholeNumber(decimalPart)} Paisa";

            return words + " Only";
        }

        private static string ConvertWholeNumber(long number)
        {
            if (number == 0)
                return "";

            if (number < 10)
                return Units[number];

            if (number < 20)
                return Teens[number - 10];

            if (number < 100)
                return Tens[number / 10] + (number % 10 > 0 ? " " + Units[number % 10] : "");

            if (number < 1000)
                return Units[number / 100] + " Hundred" + (number % 100 > 0 ? " and " + ConvertWholeNumber(number % 100) : "");

            if (number < 100000)
                return ConvertWholeNumber(number / 1000) + " Thousand" + (number % 1000 > 0 ? " " + ConvertWholeNumber(number % 1000) : "");

            if (number < 10000000)
                return ConvertWholeNumber(number / 100000) + " Lakh" + (number % 100000 > 0 ? " " + ConvertWholeNumber(number % 100000) : "");

            return ConvertWholeNumber(number / 10000000) + " Crore" + (number % 10000000 > 0 ? " " + ConvertWholeNumber(number % 10000000) : "");
        }
    }

}
