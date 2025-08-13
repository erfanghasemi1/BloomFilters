using BloomFilters.Query;
using Microsoft.AspNetCore.Mvc;

namespace BloomFilters.Controllers
{
    public class LoginController : Controller
    {
        private readonly BloomFiltersClass _bloomFilters = new BloomFiltersClass(40 , 3);
        private readonly LoginQuery _query;
        public LoginController(LoginQuery loginQuery)
        {
            _query = loginQuery;
            _bloomFilters.Add("erfan");
            _bloomFilters.Add("ali");
            _bloomFilters.Add("asghar");
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel request)
        {
            if (!_bloomFilters.CheckUser(request.username))
                return Unauthorized("bloom filters claim you don't have an account");

            if (!_query.CheckUserLogin(request)) return Unauthorized("database claim you don't have an account");

            return Ok("you login successfully");
        }
    }
}
