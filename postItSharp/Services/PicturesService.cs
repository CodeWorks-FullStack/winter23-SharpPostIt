namespace postItSharp.Services;

public class PicturesService
{
  private readonly PicturesRepository _repo;

  public PicturesService(PicturesRepository repo)
  {
    _repo = repo;
  }

  internal Picture CreatePicture(Picture pictureData)
  {
    Picture picture = _repo.CreatePicture(pictureData);
    return picture;
  }

  internal List<Picture> Find()
  {
    List<Picture> pictures = _repo.FindAll();
    return pictures;
  }
  internal List<Picture> FindByAlbum(int albumId)
  {
    List<Picture> pictures = _repo.FindByAlbum(albumId);
    return pictures;
  }

}
