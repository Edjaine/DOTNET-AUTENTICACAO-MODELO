using System.Threading.Tasks;
using DOTNET_IDENTITY_3.Infra;
using DOTNET_IDENTITY_3.Models;
using DOTNET_IDENTITY_3.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DOTNET_IDENTITY_3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Autentication: ControllerBase
    {

        // [HttpGet]
        // public ActionResult Teste(){
        //     return Ok("It's Alive");
        // }

        [HttpPost]
        public ActionResult Login([FromBody] UserViewModel model){
            var usuario = UserRepository.Get(model.Login, model.Password);
            
            if(usuario == null)
               return NotFound("Usuário não encotrado");

            var token = TokenService.GenerateToken(usuario);            
            
            return Ok(token);
        }
        [HttpGet]
        [Route("employee")]
        [Authorize(Roles = "employee,manager")]
        public string Employee() => "Funcionário";
    }
}