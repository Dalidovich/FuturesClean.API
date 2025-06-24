using System.Text;

namespace FuturesClean.API.Code.Builders
{
    public class BaseRequestBuilder
    {
        private StringBuilder _Instance;
        private BaseRequestBuilder _InstanceBuilder;

        public BaseRequestBuilder(string instance)
        {
            _Instance = new StringBuilder(instance);
            _InstanceBuilder = this;
        }

        public BaseRequestBuilder BuildSymbol(string symbol)
        {
            _Instance.Append($"symbol={symbol}&");

            return _InstanceBuilder;
        }

        public BaseRequestBuilder BuildInterval(string interval)
        {
            _Instance.Append($"interval={interval}&");

            return _InstanceBuilder;
        }

        public BaseRequestBuilder BuildLimit(long? count)
        {
            _Instance.Append($"limit={count}&");

            return _InstanceBuilder;
        }

        public BaseRequestBuilder BuildEndTime(long endTime)
        {
            _Instance.Append($"endTime={endTime}&");

            return _InstanceBuilder;
        }

        public string Build() => _Instance.ToString();
    }
}
