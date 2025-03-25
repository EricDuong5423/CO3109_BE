using RestSharp;

namespace CO3109_BE.Services
{
    public class AiApiService
    {
        private readonly RestClient _client;
        public AiApiService(RestClient client)
        {
            _client = client;
        }
        public async Task<String> FindBestEngine(object data)
        {
            var request = new RestRequest("find-engine", Method.Post);
            request.AddJsonBody(data);

            var reponse = await _client.ExecuteAsync(request);
            return reponse.Content ?? "Bad request";
        }
        public async Task<String> FindMaterial(object data)
        {
            var request = new RestRequest("find-material", Method.Post);
            request.AddJsonBody(data);

            var reponse = await _client.ExecuteAsync(request);
            return reponse.Content ?? "Bad request";
        }
    }
}
