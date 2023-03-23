namespace postItSharp.Services;

public class CollaboratorsService
{
  private readonly CollaboratorsRepository _repo;

  public CollaboratorsService(CollaboratorsRepository repo)
  {
    _repo = repo;
  }

  internal Collaborator CreateCollaborator(Collaborator collaboratorData)
  {
    Collaborator collaborator = _repo.CreateCollaborator(collaboratorData);
    return collaborator;
  }

  internal List<AlbumMember> FindCollaboratorsOnAlbum(int albumId)
  {
    List<AlbumMember> collaborators = _repo.FindCollaboratorsOnAlbum(albumId);
    return collaborators;
  }

  internal List<CollaboratedAlbum> GetMyCollabedAlbums(string accountId)
  {
    List<CollaboratedAlbum> collabedAlbums = _repo.GetMyCollabedAlbums(accountId);
    return collabedAlbums;
  }

  internal string removeCollaborator(int id, string userId)
  {
    Collaborator collaborator = _repo.GetOne(id);
    if (collaborator == null) throw new Exception($"not collab and {id}");
    if (collaborator.AccountId != userId) throw new UnauthorizedAccessException("that's not for you to decide. Nacho Collab.");
    _repo.RemoveCollaborator(id);
    return $"removed your collaboration on that album, you know the one.";
  }
}
