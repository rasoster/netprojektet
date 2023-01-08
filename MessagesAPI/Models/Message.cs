
namespace Models;

public partial class Message
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Content { get; set; }

    public DateTime? Times { get; set; }

    public bool? Seen { get; set; }

    public int? Reciever { get; set; }

    public virtual Profile? RecieverNavigation { get; set; }

    public string SenderName { get; set; }
}
