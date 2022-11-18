using PlayerDuo.DTOs.Orders;
using PlayerDuo.Repositories.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PlayerDuo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrdersController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpPost("me")]
        [Authorize]
        public async Task<ActionResult> CreateOrder(CreateOrderRequest request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            var claimsPrincipal = this.User;
            var userId = Int32.Parse(claimsPrincipal.FindFirst("id").Value);

            var result = await _orderRepository.CreateOrder(userId, request);

            if(result.IsSuccessed == false)
            {
                return BadRequest(error: result.Message);
            }

            return Ok(result.Message);
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<ActionResult> GetMyOrders(int? status)
        {
            var claimsPrincipal = this.User;
            var userId = Int32.Parse(claimsPrincipal.FindFirst("id").Value);

            var result = await _orderRepository.GetMyOrders(userId, status);

            if(result.IsSuccessed == false)
            {
                return BadRequest(error: result.Message);
            }

            return Ok(result);
        }

        [HttpGet("manage")]
        [Authorize]
        public async Task<ActionResult> GetOrders(int? status)
        {
            var claimsPrincipal = this.User;
            var userId = Int32.Parse(claimsPrincipal.FindFirst("id").Value);

            var result = await _orderRepository.GetOrders(userId, status);

            if(result.IsSuccessed == false)
            {
                return BadRequest(error: result.Message);
            }

            return Ok(result);
        }

        [HttpGet("{orderId}")]
        [Authorize]
        public async Task<ActionResult> GetOrderById(int orderId)
        {
            var claimsPrincipal = this.User;
            var userId = Int32.Parse(claimsPrincipal.FindFirst("id").Value);

            var result = await _orderRepository.GetOrderById(orderId, userId);

            if(result.IsSuccessed == false)
            {
                return BadRequest(error: result.Message);
            }

            return Ok(result);
        }

        [HttpGet("review/{skillId}")]
        public async Task<ActionResult> GetReviewBySkillId(int skillId)
        {

            var result = await _orderRepository.GetReviewBySkillId(skillId);

            if (result.IsSuccessed == false)
            {
                return BadRequest(error: result.Message);
            }

            return Ok(result);
        }

        [HttpPut("{orderId:int}/confirm")]
        [Authorize(Roles = "Player")]
        public async Task<ActionResult> ConfirmOrder(int orderId)
        {
            try
            {
                var claimsPrincipal = this.User;
                var userId = Int32.Parse(claimsPrincipal.FindFirst("id").Value);

                var result = await _orderRepository.ConfirmOrder(userId, orderId);
                if (result.IsSuccessed == false)
                {
                    // return Forbid();
                    return BadRequest(result.Message);
                }

                return Ok(result.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now + "- Server Error: " + ex);
                return StatusCode(500);
            }
        }

        [HttpPut("{orderId:int}/cancel")]
        [Authorize(Roles = "Player")]
        public async Task<ActionResult> CancelOrder(int orderId)
        {
            try
            {
                var claimsPrincipal = this.User;
                var userId = Int32.Parse(claimsPrincipal.FindFirst("id").Value);

                var result = await _orderRepository.CancelOrder(userId, orderId);
                if (result.IsSuccessed == false)
                {
                    // return Forbid();
                    return BadRequest(result.Message);
                }
                // if (result == 0)
                // {
                //     return BadRequest("Already in this state.");
                // }

                return Ok(result.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now + "- Server Error: " + ex);
                return StatusCode(500);
            }
        }
    }
}
