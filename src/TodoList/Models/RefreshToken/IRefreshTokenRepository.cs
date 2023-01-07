using TodoList.Models.Base;

namespace TodoList.Models.RefreshToken;

public interface IRefreshTokenRepository 
    : IBaseRepository<RefreshToken>
{
    RefreshToken GetByUserId(long id);
}