using B20.Ext;

namespace HttpClient.Api
{
    public interface HttpResponse
    {
        int GetStatusCode();
        Optional<T> GetBody<T>();
    }
}