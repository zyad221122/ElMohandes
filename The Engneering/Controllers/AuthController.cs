using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using The_Engneering.Services;

namespace The_Engneering.Controllers;
[Route("[controller]")]
[ApiController]
public class AuthController(IAuthService _service) : ControllerBase
{
    private readonly IAuthService service = _service;

    [HttpPost("")]
    public async Task<IActionResult> LoginAsync([FromBody]LoginRequest request, CancellationToken cancellationToken)
    {
        var authResponse = await service.GetTokenAsync(request.Email, request.Password, cancellationToken);

        if (authResponse == null) 
            return BadRequest("هناك خطأ في الإيميل او كلمه السر");

        return Ok(authResponse);
    }

}
