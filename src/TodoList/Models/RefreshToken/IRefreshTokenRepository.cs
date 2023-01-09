using TodoList.Models.Base;

namespace TodoList.Models.RefreshToken;

public interface IRefreshTokenRepository : 
    IBaseRepository<RefreshToken, Guid>.Delete,
    IBaseRepository<RefreshToken, Guid>.GetAll,
    IBaseRepository<RefreshToken, Guid>.GetById,
    IBaseRepository<RefreshToken, Guid>.Insert,
    IBaseRepository<RefreshToken, Guid>.Save,
    IBaseRepository<RefreshToken, Guid>.Update
{
    RefreshToken? GetByUserId(long id);
}