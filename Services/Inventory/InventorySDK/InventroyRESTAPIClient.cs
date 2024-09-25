using Shared;
using Shared.Models;

namespace InventorySDK
{
    public class InventoryRESTAPIClient : BaseSdkClient, IInventoryRESTAPIClient
    {
        public InventoryRESTAPIClient(HttpClient httpClient) : base("https://localhost:7048", httpClient) { }

        public async Task<TimeSpan> UpdateInventory(UpdateInventoryRequestModel request)
        {
            return await SendJsonRequest<TimeSpan>(HttpMethod.Post, "UpdateInventory", request);
        }
    }
}
