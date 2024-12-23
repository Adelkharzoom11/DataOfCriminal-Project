using ApiProject.Core.Interface;
using ApiProject.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CriminalController : ControllerBase
    {
        private readonly ICriminalSrvice _criminalSrvice;

        public CriminalController(ICriminalSrvice criminalSrvice)
        {
            _criminalSrvice = criminalSrvice;
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromForm] CriminalDto criminalDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "البيانات المدخلة غير صحيحة", errors = ModelState });

            try
            {
                await _criminalSrvice.AddCriminalAsync(criminalDto);
                return Ok(new { message = "تمت الإضافة بنجاح" }); // الرد بصيغة JSON
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"حدث خطأ أثناء الإضافة: {ex.Message}" });
            }
        }
    }
}
