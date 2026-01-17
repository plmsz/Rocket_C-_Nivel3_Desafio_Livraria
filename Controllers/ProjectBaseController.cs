using Microsoft.AspNetCore.Mvc;

namespace Nivel3_Desafio_Livraria.Controllers;

[Route("api/[controller]")]
[ApiController]
public abstract class ProjectBaseController : ControllerBase
{
    protected bool ValideteKey(string key)
    {
        return Request.Headers["key"].ToString() == "livraria-2026";
    }

    protected bool ValidateToken(string token)
    {
        return Request.Headers["token"].ToString() == "report-token-123";
    }

    protected virtual string Log()
    {
        var controllerName = ControllerContext.ActionDescriptor.ControllerName;
        var method = ControllerContext.ActionDescriptor.ActionName;
        return $"Controler: {controllerName} - Método: {method}";
    }
}