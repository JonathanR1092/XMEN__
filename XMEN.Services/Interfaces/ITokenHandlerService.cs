using XMEN.Abstractions.Interfaces;

namespace XMEN.Services.Interfaces
{
    public interface ITokenHandlerService
    {
        string GenerateJwtToken(ITokenParameters parameters);
    }
}