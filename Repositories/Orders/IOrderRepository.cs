using PlayerDuo.DTOs.Common;
using PlayerDuo.DTOs.Orders;
using PlayerDuo.Utilities;

namespace PlayerDuo.Repositories.Orders
{
    public interface IOrderRepository
    {
        // Task<PagedResult<OrderMainInfoVm>> GetUserOrders(int userId, string state, int page, int perPage);
        // Task<PagedResult<OrderManageInfoVm>> GetTourProviderOrders(int userId, string state, int page, int perPage, string keyword);
        // Task<OrderManageInfoVm> GetOrderById(int orderId);  // for provider, response after processing order
        // Task<OrderManageDetailVm> GetOrderDetailManage(int userId, int orderId);    // detail for provider
        Task<ApiResult<List<OrderVm>>> GetMyOrders(int userId, int? status); // những đơn hàng mình đặt người khác
        Task<ApiResult<List<OrderVm>>> GetOrders(int userId, int? status);  // những đơn hàng người khác đặt mình, role player
        Task<ApiResult<OrderVm>> GetOrderById(int orderId, int userId); // thông tin chi tiết 1 đơn hàng
        Task<ApiResult<string>> CreateOrder(int userId, CreateOrderRequest request); // đặt hàng 
        Task<ApiResult<string>>ConfirmOrder(int userId, int orderId); // xác nhận đơn hàng 
        Task<ApiResult<string>> CancelOrder(int userId, int orderId);
        Task<ApiResult<List<ReviewVM>>> GetReviewBySkillId(int skillId);
        Task<ApiResult<string>> RatingOrder(int userId, int orderId, RatingOrderRequest ratingRequest);
        Task<ApiResult<string>> FinishOrder(int userId, int orderId);
    }
}
