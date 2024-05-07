namespace Identity.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthService> _logger;

        public AuthService(ApplicationDbContext dbContext
            , IConfiguration configuration
            , ILogger<AuthService> logger)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<LoginResultDto> Login(LoginDto model)
        {
            var user = await _dbContext.ApplicationUsers
                .Include(x => x.ApplicationRole)
                .FirstOrDefaultAsync(x => x.AppId == model.AppId && x.SecretKey == model.SecretKey);
            if (user != null)
            {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.FirstName +" "+ user.LastName),
                    new Claim(ClaimTypes.Role, user.ApplicationRole?.Name),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("UserId", user.Id.ToString()),
                    new Claim("RoleId", user.RoleId.ToString()),
                    new Claim("RoleName", user.ApplicationRole?.Name)
                };

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return new LoginResultDto
                {
                    UserId = user.Id,
                    FullName = user.FirstName +" "+ user.LastName,
                    RoleId = user.RoleId,
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = token.ValidTo
                };
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> FindUser(string AppId, string SecretKey)
        {
            bool IsSuccess = _dbContext.ApplicationUsers.Any(x => x.AppId == AppId && x.SecretKey == SecretKey);
            return IsSuccess;
        }

        public async Task<bool> Register(RegisterDto model)
        {
            ApplicationUser user = new ApplicationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                AppId = model.AppId,
                SecretKey = model.SecretKey,
                RoleId = model.RoleId,
            };

            await _dbContext.ApplicationUsers.AddAsync(user);
            int res = await _dbContext.SaveChangesAsync();

            return res > 0 ? true : false;
        }

        public async Task<AppSecretDto> GetAppSecret()
        {
            AppSecretDto result = new AppSecretDto
            {
                AppId = Guid.NewGuid().ToString(),
                SecretKey = Guid.NewGuid().ToString()
            };

            return result;
        }
    }
}
