namespace postItSharp.Repositories;

public class PicturesRepository
{
  private readonly IDbConnection _db;

  public PicturesRepository(IDbConnection db)
  {
    _db = db;
  }

  internal List<Picture> FindAll()
  {
    string sql = @"
    SELECT
    pic.*,
    acct.*
    FROM pictures pic
    JOIN accounts acct ON pic.ownerId = acct.id;
    ";
    List<Picture> pictures = _db.Query<Picture, Profile, Picture>(sql, (pic, prof) =>
    {
      pic.Owner = prof;
      return pic;
    }).ToList();
    return pictures;
  }

  internal List<Picture> FindByAlbum(int albumId)
  {
    string sql = @"
    SELECT
    pic.*,
    acct.*
    FROM pictures pic
    JOIN accounts acct ON pic.ownerId = acct.id
    WHERE pic.albumId = @albumId;
    ";
    List<Picture> pictures = _db.Query<Picture, Profile, Picture>(sql, (pic, prof) =>
    {
      pic.Owner = prof;
      return pic;
    }, new { albumId }).ToList();
    return pictures;
  }
}
