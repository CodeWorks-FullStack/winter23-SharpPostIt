namespace postItSharp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlbumsController : ControllerBase
{
  private readonly AlbumsService _albumsService;
  private readonly PicturesService _picturesService;
  private readonly Auth0Provider _auth;

  public AlbumsController(AlbumsService albumsService, Auth0Provider auth, PicturesService picturesService)
  {
    _albumsService = albumsService;
    _picturesService = picturesService;
    _auth = auth;
  }

  [HttpGet]
  async public Task<ActionResult<List<Album>>> Find()
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      // when getting userinfo on an un authorized route, use the ? to not dig into the null userinfo for logged out requests
      List<Album> albums = _albumsService.Get(userInfo?.Id);
      return Ok(albums);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{id}/pictures")]
  public ActionResult<List<Picture>> FindPicturesByAlbum(int id)
  {
    try
    {
      List<Picture> pictures = _picturesService.FindByAlbum(id);
      return pictures;
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpPost]
  [Authorize]
  // Task is added to the return type in any async function
  async public Task<ActionResult<Album>> CreateAlbum([FromBody] Album albumData)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      albumData.OwnerId = userInfo.Id;
      Album album = _albumsService.CreateAlbum(albumData);
      album.Owner = userInfo;
      return Ok(album);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
  [HttpDelete("{id}")]
  [Authorize]
  async public Task<ActionResult<Album>> ArchiveAlbum(int id)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      Album album = _albumsService.ArchiveAlbum(id, userInfo.Id);
      return Ok(album);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
