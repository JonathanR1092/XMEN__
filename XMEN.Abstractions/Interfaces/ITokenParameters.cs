namespace XMEN.Abstractions.Interfaces
{
    public interface ITokenParameters
    {
        string UserName { get; set; }

        string PasswordHash { get; set; }

        string Id { get; set; }
    }
}