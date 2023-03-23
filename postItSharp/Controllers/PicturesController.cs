namespace postItSharp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PicturesController : ControllerBase
{
  private readonly PicturesService _picturesService;
  private readonly Auth0Provider _auth;

  public PicturesController(PicturesService picturesService, Auth0Provider auth)
  {
    _picturesService = picturesService;
    _auth = auth;
  }

  [HttpGet]
  public ActionResult<List<Picture>> Find()
  {
    try
    {
      List<Picture> pictures = _picturesService.Find();
      return pictures;
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpPost]
  [Authorize]
  public async Task<ActionResult<Picture>> CreatePicture([FromBody] Picture pictureData)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      pictureData.CreatorId = userInfo.Id;
      Picture picture = _picturesService.CreatePicture(pictureData);
      picture.Creator = userInfo;
      return picture;
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
