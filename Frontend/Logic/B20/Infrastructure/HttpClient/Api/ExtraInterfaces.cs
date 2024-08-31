using B20.Ext;

namespace HttpClient.Api
{
    public interface HttpResponse
    {
        int getStatusCode();
        Optional<T> getBody<T>();
    }
}