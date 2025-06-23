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

    }
}
