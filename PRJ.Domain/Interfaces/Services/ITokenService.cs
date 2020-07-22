using PRJ.Domain.Entities;


namespace PRJ.Domain.Interfaces.Services
{
    public interface ITokenService
    {
        UserTokenEntity CreateToken(UserEntity user);
    }
}
