using Shared.Models;

namespace OrderManagementSDK
{
    public interface IOrderManagementRESTAPIClient
    {
        Task<List<ServiceResponse>> PlaceOrder(OrderItem request);
    }
}
