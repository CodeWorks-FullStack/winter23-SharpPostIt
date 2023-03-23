namespace postItSharp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CollaboratorsController : ControllerBase
{
  private readonly CollaboratorsService _collaboratorsService;
  private readonly Auth0Provider _auth;

  public CollaboratorsController(CollaboratorsService collaboratorsService, Auth0Provider auth)
  {
    _collaboratorsService = collaboratorsService;
    _auth = auth;
  }

  [HttpPost]
  [Authorize]
  public async Task<ActionResult<Collaborator>> CreateCollaborator([FromBody] Collaborator collaboratorData)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      collaboratorData.AccountId = userInfo.Id;
      Collaborator collaborator = _collaboratorsService.CreateCollaborator(collaboratorData);
      return Ok(collaborator);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpDelete("{id}")]
  [Authorize]
  public async Task<ActionResult<string>> RemoveCollaborator(int id)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      string message = _collaboratorsService.removeCollaborator(id, userInfo.Id);
      return Ok(message);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
