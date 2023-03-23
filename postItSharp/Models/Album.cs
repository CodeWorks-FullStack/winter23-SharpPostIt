namespace postItSharp.Models;

public class Album
{
  public int Id { get; set; }
  public string CreatorId { get; set; }
  public string Title { get; set; }
  public string Category { get; set; }
  public bool Archived { get; set; } = false;
  public string CoverImg { get; set; } = "https://images.unsplash.com/photo-1453227588063-bb302b62f50b?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1470&q=80";

  public Profile Creator { get; set; }
}

public class CollaboratedAlbum : Album
{
  public int CollaboratorId { get; set; }
}
