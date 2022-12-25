namespace TodoList.DTO.Token;

public class RefreshTokenChangeStatus
{
    public string RefreshToken { get; set; }
    public bool IsRevorked { get; set; }
    public bool IsUsed { get; set; }
}