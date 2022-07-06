using IdentityServer.Data;
using IdentityServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace IdentityServer.Quickstart.IdentityUser
{
    [Authorize(Policy = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        public UserController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET api/user/guid
        [HttpGet("{guid}")]
        public async Task<IActionResult> GetUserEmail(string guid)
        {
            ApplicationUser user = await _dbContext.Users.AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == guid);

            if (user == null)
            {
                return NotFound(new { message = "The requested user was not found" });
            }

            var userEmail = user.Email;

            if(userEmail == null || userEmail == string.Empty)
            {
                return NotFound(new { message = "The requested user has no email" });
            }

            return Ok(userEmail);
        }
    }
}
