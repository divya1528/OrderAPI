using ApplicationCore.Entities;
using ApplicationCore.Model;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace OrderAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrderController : ControllerBase
    {
        IOrderRepository _repository;
        IUserRepository _userRepository;
        IJWTAuthManager _authManager;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IOrderRepository repository, IUserRepository userRepository, IJWTAuthManager authManager, ILogger<OrderController> logger)
        {
            _repository = repository;
            _userRepository =  userRepository;
            _authManager = authManager;
            _logger = logger;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public IActionResult login([FromBody] LoginModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var abc = _userRepository.GetAll().Result;

            var result = _userRepository.Get(user).Result;
            if (result!= null)
            {
                var token = _authManager.GenerateJWT(result);
                return Ok(token);
            }
            return NotFound(result);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(typeof(IEnumerable<Order>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Order>>> Get()
        {
            _logger.LogInformation("Getting order details");
            var orders = await _repository.GetOrders();
            return Ok(orders);
        }
    }
}
