namespace LogTruck.Application.Common.Security
{
    public interface ICurrentUserService
    {
        Guid? UserId { get; }
        string? Email { get; }
        string? Nome { get; }
        string? Role { get; }
        bool IsAuthenticated { get; }
    }
}
