using JobsOnMarket.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JobMarket.Data;
using JobMarket.Data.Entity;

namespace JobsOnMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private IDataUnitOfWork dataUnitOfWork;
        public AuthController(UserManager<IdentityUser> userManager, IConfiguration configuration,IDataUnitOfWork  dataUnitOfWork)
        {
            this._userManager = userManager;
            this._configuration = configuration;
            this.dataUnitOfWork = dataUnitOfWork;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Returns 400 Bad Request with error details
            }
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.UnhashedPassword))
            {
                var authClaims = await GetClaimsOfUser(user);
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                var token = GetToken(authClaims);
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized(new { message = "User not found." });
            var roles = await _userManager.GetRolesAsync(user);
            return Ok(new { roles,user.UserName });
        }
        // POST: api/Auth/set
        [HttpPost("set")]
        [Authorize]
        public async Task<IActionResult> SetRole([FromBody] string role)
        {
            // Validate role
            var allowedRoles = new[] { "General", "Customer", "Contractor" };
            if (!allowedRoles.Contains(role))
                return BadRequest(new { message = "Invalid role. Allowed roles: General, Customer, Staff." });

            // Get current user from JWT
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized(new { message = "User not found." });

            // Remove existing roles
            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);

            // Add new role
            var result = await _userManager.AddToRoleAsync(user, role);
            if (!result.Succeeded)
                return StatusCode(500, new { message = "Failed to set role.", errors = result.Errors });

            return Ok(new { message = $"Role set to {role} for user {user.UserName}." });
        }
        private JwtSecurityToken GetToken(IList<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        private async Task<IList<Claim>> GetClaimsOfUser(IdentityUser user)
        {
            var authClaims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }
            return authClaims;
        }
        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Get the current user based on the authenticated principal (from the JWT)
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Change the password using the UserManager
            var result = await _userManager.ChangePasswordAsync(user, changePasswordDto.OldPassword, changePasswordDto.NewPassword);

            if (result.Succeeded)
            {
                // Sign out the user to invalidate the current JWT and force re-login with the new password
                // This is a common security practice for JWTs after a password change.
                // In a pure API scenario, the client is responsible for discarding the old token.
                return Ok("Password changed successfully. Please log in again with your new password.");
            }

            // Handle errors
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return BadRequest(ModelState);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationDto dto) {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Returns 400 Bad Request with error details
            }
            var user = new IdentityUser { UserName = dto.UserName, Email = dto.UserName };
            try
            {
                var result = await _userManager.CreateAsync(user, dto.UnhashedPassword);
                var role=await _userManager.AddToRoleAsync(user, dto.RoleName);
                if (dto.RoleName.Equals("Customer", StringComparison.InvariantCultureIgnoreCase))
                {
                    var customer = new Customer
                    {
                        FirstName = dto.FirstName,
                        LastName = dto.Surname,
                    };
                    await dataUnitOfWork.CustomerRepository.AddAsync(customer);
                    await dataUnitOfWork.CompleteAsync();
                    
                    await dataUnitOfWork.CustomerUserRepository.AddAsync(new CustomerUser {UserId = user.Id,CustomerId = customer.ID});
                    await dataUnitOfWork.CompleteAsync();
                }
                else if (dto.RoleName.Equals("Contractor", StringComparison.InvariantCultureIgnoreCase))
                {
                    var contractor = new Contractor
                    {
                        Name = dto.FirstName + " " + dto.Surname,
                        Rating = 0,
                    };
                    await dataUnitOfWork.ContractorRepository.AddAsync(contractor);
                    await dataUnitOfWork.CompleteAsync();
                    
                    await dataUnitOfWork.ContractorUserRepository.AddAsync(new ContractorUser() {UserId = user.Id,ContractorId = contractor.ID});
                    await dataUnitOfWork.CompleteAsync();
                }
                if (result.Succeeded)
                {
                    return await Login(dto);
                }
                return BadRequest(result.Errors);
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
