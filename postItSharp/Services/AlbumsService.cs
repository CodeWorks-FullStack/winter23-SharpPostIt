namespace postItSharp.Services;

public class AlbumsService
{
  private readonly AlbumsRepository _repo;
  private readonly CollaboratorsService _collaboratorsService;

  public AlbumsService(AlbumsRepository repo, CollaboratorsService collaboratorsService)
  {
    _repo = repo;
    _collaboratorsService = collaboratorsService;
  }

  internal Album ArchiveAlbum(int id, string userId)
  {
    Album album = this.Get(id, userId); // added in the userId so the get one can check for archived
    if (album.CreatorId != userId) throw new Exception("You don't own that!");
    album.Archived = !album.Archived; // flip the bool
    _repo.updateAlbum(album);
    return album;

  }

  internal Album CreateAlbum(Album albumData)
  {
    Album album = _repo.CreateAlbum(albumData);
    return album;
  }

  internal List<AlbumMember> FindCollaboratorsOnAlbum(int id, string userId)
  {
    Album album = this.Get(id, userId);
    List<AlbumMember> collaborators = _collaboratorsService.FindCollaboratorsOnAlbum(id);
    return collaborators;
  }

  internal List<Album> Get(string userId)
  {
    List<Album> albums = _repo.GetAll();
    // NOTE FindAll works just like filter in js
    albums = albums.FindAll(a => a.Archived == false || a.CreatorId == userId);
    return albums;
  }
  internal Album Get(int id, string userId)
  {
    Album album = _repo.GetOne(id);
    if (album == null) throw new Exception("it's not there");
    if (album.Archived && album.CreatorId != userId) throw new Exception("that's not your album, I don't know you"); // if album is archived and the user is not the creator don't return it.

    return album;
  }
}
