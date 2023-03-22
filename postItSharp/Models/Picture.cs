namespace postItSharp.Models;

public class Picture
{
  public int Id { get; set; }
  public string ImgUrl { get; set; }
  public string OwnerId { get; set; }
  public int AlbumId { get; set; }
  public Profile Owner { get; set; }
}
