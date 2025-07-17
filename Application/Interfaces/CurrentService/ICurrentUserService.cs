namespace Application.Interfaces.CurrentService
{
    public interface ICurrentUserService
    {
            string? UserId { get; }
            string? UserName { get; }
            bool IsAuthenticated { get; }

        
    }

}

