namespace postItSharp.Services;

public class AlbumsService
{
  private readonly AlbumsRepository _repo;

  public AlbumsService(AlbumsRepository repo)
  {
    _repo = repo;
  }

  internal Album ArchiveAlbum(int id, string userId)
  {
    Album album = this.Get(id);
    if (album.OwnerId != userId) throw new Exception("You don't own that!");
    album.Archived = !album.Archived; // flip the bool
    _repo.updateAlbum(album);
    return album;

  }

  internal Album CreateAlbum(Album albumData)
  {
    Album album = _repo.CreateAlbum(albumData);
    return album;
  }

  internal List<Album> Get(string userId)
  {
    List<Album> albums = _repo.GetAll();
    // NOTE FindAll works just like filter in js
    albums = albums.FindAll(a => a.Archived == false || a.OwnerId == userId);
    return albums;
  }
  internal Album Get(int id)
  {
    Album album = _repo.GetOne(id);
    if (album == null) throw new Exception("it's not there");

    return album;
  }
}
