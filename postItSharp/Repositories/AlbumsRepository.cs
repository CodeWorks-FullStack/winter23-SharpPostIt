namespace postItSharp.Repositories;

public class AlbumsRepository
{
  private readonly IDbConnection _db;

  public AlbumsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Album CreateAlbum(Album albumData)
  {
    string sql = @"
    INSERT INTO albums
    (creatorId, title, category, coverImg)
    VALUES
    (@creatorId, @title, @category, @coverImg);
    SELECT LAST_INSERT_ID();
    ";
    int id = _db.ExecuteScalar<int>(sql, albumData);
    albumData.Id = id;
    return albumData;
  }

  internal List<Album> GetAll()
  {
    string sql = @"
    SELECT
    alb.*,
    acct.*
    FROM albums alb
    JOIN accounts acct ON alb.creatorId = acct.id;
    ";
    // -----------------------------⬇️ first select--------------⬇️
    // -------------------------------------⬇️ second from select-------⬇️
    // ----⬇️-return type --------------------------⬇️
    List<Album> albums = _db.Query<Album, Profile, Album>(sql, (album, prof) =>
    {
      // (album, account) => is a map function that runs once for every resulting row from our 🐿️
      album.Creator = prof; // combine the the objects from the map parameters
      return album; // return the return type object
    }).ToList();
    return albums;
  }

  internal Album GetOne(int id)
  {
    string sql = @"
    SELECT
    alb.*,
    acct.*
    FROM albums alb
    JOIN accounts acct ON alb.creatorId = acct.id
    WHERE alb.id = @id;";
    Album album = _db.Query<Album, Profile, Album>(sql, (album, prof) =>
    {
      album.Creator = prof;
      return album;
    }, new { id }).FirstOrDefault();
    return album;
  }

  internal int updateAlbum(Album album)
  {
    string sql = @"
    UPDATE albums SET
    title = @title,
    coverImg = @coverImg,
    archived = @archived,
    category = @category
    WHERE id = @id;
    ";
    int rows = _db.Execute(sql, album);
    return rows;
  }
}
