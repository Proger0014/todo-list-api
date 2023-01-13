using System.Security.Claims;

namespace TodoList.DTO.User;

public class UserAccessDeniedCheck
{
    public IEnumerable<Claim> UserClaims { get; set; }
    public long UserId { get; set; }

    public UserAccessDeniedCheck()
    {
        UserClaims = new List<Claim>();
    }
}