
// Importando  Pacote
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers {


    [ApiController]
    [Route("")]// Raiz
    public class HomeController : ControllerBase {

        public IActionResult Get() {

            return Ok();

        }

    }
}