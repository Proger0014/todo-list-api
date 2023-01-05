namespace TodoList.DTO.Token;

public class RefreshTokenCreate
{
    public long UserId { get; set; }
    public string FingerPrint { get; set; }


    public RefreshTokenCreate(long userId, string fingerPrint)
    {
        UserId = userId;
        FingerPrint = fingerPrint;
    }

    public RefreshTokenCreate() { }
}