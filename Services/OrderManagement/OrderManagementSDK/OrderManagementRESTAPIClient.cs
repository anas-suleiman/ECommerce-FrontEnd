using Shared;
using Shared.Models;

namespace OrderManagementSDK
{
    public class OrderManagementRESTAPIClient : BaseSdkClient, IOrderManagementRESTAPIClient
    {
        public OrderManagementRESTAPIClient(HttpClient httpClient) : base("https://localhost:7138", httpClient) { }

        public async Task<List<ServiceResponse>> PlaceOrder(OrderItem request)
        {
            return await SendJsonRequest<List<ServiceResponse>>(HttpMethod.Post, "PlaceOrder", request);
        }
    }
}
