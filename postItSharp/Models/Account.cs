namespace postItSharp.Models;

public class Profile
{
  public string Name { get; set; }
  public string Id { get; set; }
  public string Picture { get; set; }
}

public class AlbumMember : Profile
{
  public int collaboratorId { get; set; }
}

public class Account : Profile
{
  public string Email { get; set; }
}
