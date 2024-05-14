using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EGNDotNetCore.RestApiWithNLayer.Features.PickAPile
{
    [Route("api/[controller]")]
    [ApiController]
    public class PickAPileController : ControllerBase
    {
        private async Task<PickAPile> GetDataAsync()
        {
            string JsonStr = await System.IO.File.ReadAllTextAsync("PickAPile.json");
            var model = JsonConvert.DeserializeObject<PickAPile>(JsonStr);
            return model;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPickAPile()
         {
             var response = await GetDataAsync();
             return Ok(response.Questions);
         }

        [HttpGet("questionId")]
        public async Task<IActionResult>GetSpecificData(int questionId)
         {
             var response = await GetDataAsync();
             return Ok(response.Answers.Where(x=>x.QuestionId == questionId).ToList());
         }

        [HttpGet("{questionId}/{answerId}")]
        public async Task<IActionResult>GetSpecificAnswer(int questionId,int answerId)
        {
            var response = await GetDataAsync();
            return Ok(response.Answers.FirstOrDefault(x=>x.QuestionId == questionId && x.AnswerId == answerId));
        }
    }

    public class PickAPile
    {
        public Question[] Questions { get; set; }
        public Answer[] Answers { get; set; }
    }

    public class Question
    {
        public int QuestionId { get; set; }
        public string QuestionName { get; set; }
        public string QuestionDesp { get; set; }
    }

    public class Answer
    {
        public int AnswerId { get; set; }
        public string AnswerImageUrl { get; set; }
        public string AnswerName { get; set; }
        public string AnswerDesp { get; set; }
        public int QuestionId { get; set; }
    }

}
