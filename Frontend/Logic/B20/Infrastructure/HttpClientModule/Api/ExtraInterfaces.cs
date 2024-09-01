using B20.Ext;

namespace HttpClientModule.Api
{
    public interface HttpClient {
        HttpResponse Get(string path);

        HttpResponse Post<T>(string path, Optional<T> body);
    }
    
    public interface HttpResponse
    {
        int GetStatusCode();
        Optional<T> GetBody<T>();
    }
}