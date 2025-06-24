namespace FuturesClean.API.Code.Comunication.Enum
{
    public enum InnerStatusCode
    {
        EntityNotFound = 0,

        FuturesDifferenceCreate = 10,
        FuturesDifferenceUpdate = 20,
        FuturesDifferenceDelete = 30,
        FuturesDifferenceRead = 40,
        FuturesDifferenceExist = 50,

        OK = 200,
        OKNoContent = 204,
        InternalServerError = 500,
    }
}
