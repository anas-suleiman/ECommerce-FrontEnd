using Shared.Models;

namespace OrderManagementCore.Managers
{
    public interface IRESTAPIOrderManager
    {
        Task<List<ServiceResponse>> PlaceOrder(OrderItem item);
    }
}
