namespace StockQuoteAlert.Models
{
    public class Quote
    {
        public Quote(string name, float uv, float lv)
        {
            Name = name;
            UpperValue = uv;
            LowerValue = lv;
        }

        public string Name { get; set; }
        public float UpperValue { get; set; }
        public float LowerValue { get; set; }
    }
}
