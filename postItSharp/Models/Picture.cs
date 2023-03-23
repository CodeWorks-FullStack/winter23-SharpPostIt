namespace postItSharp.Models;

public class Picture
{
  public int Id { get; set; }
  public string ImgUrl { get; set; }
  public string CreatorId { get; set; }
  public int AlbumId { get; set; }
  public Profile Creator { get; set; }
}
