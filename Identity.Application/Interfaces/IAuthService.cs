namespace Identity.Application.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResultDto> Login(LoginDto model);
        Task<bool> Register(RegisterDto model);
        Task<bool> FindUser(string AppId, string SecretKey);
        Task<AppSecretDto> GetAppSecret();
    }
}
