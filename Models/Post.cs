using System;
using System.Collections.Generic;

namespace GilsApi.Models;

public partial class Post
{
    public int IdPost { get; set; }

    public int UserId { get; set; }

    public DateTime PublishTime { get; set; }

    public string Text { get; set; } = null!;

    public virtual ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<ReactionsUsersPost> ReactionsUsersPosts { get; set; } = new List<ReactionsUsersPost>();

    public virtual User User { get; set; } = null!;
}
