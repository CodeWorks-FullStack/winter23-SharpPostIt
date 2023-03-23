namespace postItSharp.Repositories;

public class PicturesRepository
{
  private readonly IDbConnection _db;

  public PicturesRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Picture CreatePicture(Picture pictureData)
  {
    string sql = @"
    INSERT INTO pictures
    (imgUrl, creatorId, albumId)
    VALUES
    (@imgUrl, @creatorId, @albumId);
    SELECT LAST_INSERT_ID();
    ";
    int id = _db.ExecuteScalar<int>(sql, pictureData);
    pictureData.Id = id;
    return pictureData;
  }

  internal List<Picture> FindAll()
  {
    string sql = @"
    SELECT
    pic.*,
    acct.*
    FROM pictures pic
    JOIN accounts acct ON pic.creatorId = acct.id;
    ";
    List<Picture> pictures = _db.Query<Picture, Profile, Picture>(sql, (pic, prof) =>
    {
      pic.Creator = prof;
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
    JOIN accounts acct ON pic.creatorId = acct.id
    WHERE pic.albumId = @albumId;
    ";
    List<Picture> pictures = _db.Query<Picture, Profile, Picture>(sql, (pic, prof) =>
    {
      pic.Creator = prof;
      return pic;
    }, new { albumId }).ToList();
    return pictures;
  }
}
