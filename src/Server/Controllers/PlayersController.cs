using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayersController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<PlayerDto> Get()
        {
            return new List<PlayerDto>
            {
                new PlayerDto{ Id = 1, FirstName = "John", Surname = "Doe", Nickname = "JD" }
            };
        }
    }
}
