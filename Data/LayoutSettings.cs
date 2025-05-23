using ChequePrintWebApp.Models;



namespace ChequePrintWebApp.Data
{
    public static class LayoutSettings
    {
        public static List<PrintPosition> Positions = new List<PrintPosition>
    {
        new PrintPosition { FieldName = "Date", Top = "1.3cm", Right = "1.9cm" },
        new PrintPosition { FieldName = "PayeeName", Top = "2.3cm", Left = "1cm" },
        new PrintPosition {FieldName = "AccountNumber", Top="2.7cm", Left="1cm"},
        new PrintPosition { FieldName = "AmountInWords", Top = "3.4cm", Left = "1cm" },
        new PrintPosition { FieldName = "AmountNumeric", Top = "3.4cm", Right = "1.9cm" }
    };
    }

}
