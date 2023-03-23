namespace postItSharp.Repositories;

public class CollaboratorsRepository
{
  private readonly IDbConnection _db;

  public CollaboratorsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Collaborator CreateCollaborator(Collaborator collaboratorData)
  {
    string sql = @"
    INSERT INTO collaborators
    (albumId, accountId)
    VALUES
    (@albumId, @accountId);
    SELECT LAST_INSERT_ID();
    ";
    int id = _db.ExecuteScalar<int>(sql, collaboratorData);
    collaboratorData.Id = id;
    return collaboratorData;
  }

  internal List<AlbumMember> FindCollaboratorsOnAlbum(int albumId)
  {
    string sql = @"
    SELECT
    acct.*,
    collab.id AS collaboratorId
    FROM collaborators collab
    JOIN accounts acct ON collab.accountId = acct.id
    WHERE collab.albumId = @albumId;
    ";
    List<AlbumMember> collaborators = _db.Query<AlbumMember>(sql, new { albumId }).ToList();
    return collaborators;
  }

  internal List<CollaboratedAlbum> GetMyCollabedAlbums(string accountId)
  {
    string sql = @"
    SELECT
    alb.*,
    collab.*,
    creator.*
    FROM collaborators collab
    JOIN albums alb ON collab.albumId = alb.id
    JOIN accounts creator ON alb.creatorId = creator.id
    WHERE collab.accountId = @accountId;
    ";
    List<CollaboratedAlbum> collabedAlbums = _db.Query<CollaboratedAlbum, Collaborator, Profile, CollaboratedAlbum>(sql, (collaboratedAlbum, collaborator, profile) =>
    {
      collaboratedAlbum.CollaboratorId = collaborator.Id;
      collaboratedAlbum.Creator = profile;
      return collaboratedAlbum;

    }, new { accountId }).ToList();
    return collabedAlbums;
  }

  internal Collaborator GetOne(int id)
  {
    string sql = @"
    SELECT 
    * 
    FROM collaborators 
    WHERE id = @id;";
    Collaborator collaborator = _db.Query<Collaborator>(sql, new { id }).FirstOrDefault();
    return collaborator;
  }

  internal void RemoveCollaborator(int id)
  {
    string sql = @"
    DELETE FROM collaborators
    WHERE id = @id;
    ";
    _db.Execute(sql, new { id });
  }
}
