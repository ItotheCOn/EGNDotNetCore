using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EGNDotNetCore.RestApiWithNLayer.Features.Birds
{
    [Route("api/[controller]")]
    [ApiController]
    public class BirdsController : ControllerBase
    {
        private async Task<Birds> getAllBirdsAsync()
        {
            string JsonStr = await System.IO.File.ReadAllTextAsync("Birds.json");
            var respone = JsonConvert.DeserializeObject<Birds>(JsonStr);
            return respone;
        }

        [HttpGet("Tbl_Bird")]
        public async Task<IActionResult> GetAllBirdsInfo()
        {
            var model = await getAllBirdsAsync();
            return Ok(model.Tbl_Bird);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetBirdType(int id)
        {
            var model = await getAllBirdsAsync();
            return Ok(model.Tbl_Bird.FirstOrDefault(x => x.Id == id));
        }
    }

    public class Birds
    {
        public Tbl_Bird[] Tbl_Bird { get; set; }
    }

    public class Tbl_Bird
    {
        public int Id { get; set; }
        public string BirdMyanmarName { get; set; }
        public string BirdEnglishName { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
    }

}
