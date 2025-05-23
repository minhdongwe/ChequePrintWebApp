namespace ChequePrintWebApp.Models
{
    public class PrintPosition
    {
        public string FieldName { get; set; }      // e.g. "Date", "PayeeName"
        public string Top { get; set; }            // e.g. "2.5cm"
        public string Left { get; set; }           // e.g. "1cm"
        public string Right { get; set; }          // e.g. "2cm"
    }

}
