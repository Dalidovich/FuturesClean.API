namespace FuturesClean.API.Domain.Entities
{
    public class FuturesDifference
    {
        public Guid? Id { get; set; }
        public DateTime TimeMeasuredUtc { get; set; }
        public string Interval { get; set; }
        public string SymbolCurrent { get; set; }
        public string SymbolNext { get; set; }
        public decimal Spread { get; set; }

        public FuturesDifference(DateTime timeMeasuredUtc, string interval, string symbolCurrent, string symbolNext, decimal spread)
        {
            TimeMeasuredUtc = timeMeasuredUtc;
            Interval = interval;
            SymbolCurrent = symbolCurrent;
            SymbolNext = symbolNext;
            Spread = spread;
        }

        public FuturesDifference()
        {
        }
    }
}
