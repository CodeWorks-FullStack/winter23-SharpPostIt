namespace postItSharp.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
  private readonly AccountService _accountService;
  private readonly CollaboratorsService _collaboratorsService;
  private readonly Auth0Provider _auth;

  public AccountController(AccountService accountService, Auth0Provider auth0Provider, CollaboratorsService collaboratorsService)
  {
    _accountService = accountService;
    _auth = auth0Provider;
    _collaboratorsService = collaboratorsService;
  }

  [HttpGet]
  [Authorize]
  public async Task<ActionResult<Account>> Get()
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_accountService.GetOrCreateProfile(userInfo));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("collaborators")]
  [Authorize]
  public async Task<ActionResult<List<CollaboratedAlbum>>> GetMyCollabedAlbums()
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      List<CollaboratedAlbum> collabedAlbums = _collaboratorsService.GetMyCollabedAlbums(userInfo.Id);
      return Ok(collabedAlbums);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
