using FuturesClean.API.Code.Comunication.Enum;

namespace FuturesClean.API.Code.Comunication.InnerResponse
{
    public abstract class BaseResponse<T>
    {
        public virtual T Data { get; set; }
        public virtual InnerStatusCode InnerStatusCode { get; set; }
        public virtual string Message { get; set; }
    }
}
