namespace TodoList.DTO.Token;

public class RefreshTokenCreate
{
    public Guid Id { get; set; }
    public long UserId { get; set; }
    public string FingerPrint { get; set; }

    public RefreshTokenCreate()
    {
        FingerPrint = string.Empty;
    }
}