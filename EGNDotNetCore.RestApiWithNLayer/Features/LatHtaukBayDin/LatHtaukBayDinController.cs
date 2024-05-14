using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
namespace EGNDotNetCore.RestApiWithNLayer.Features.LatHtaukBayDin
{
    [Route("api/[controller]")]
    [ApiController]
    public class LatHtaukBayDinController : ControllerBase
    {
        private async Task<LatHtaukBayDin> GetDataAsync()
        {
            string JsonStr = await System.IO.File.ReadAllTextAsync("LatHtaukBayDin.json");
            var question = JsonConvert.DeserializeObject<LatHtaukBayDin>(JsonStr);
            return question;
        }
        [HttpGet("questions")]
        public async Task<IActionResult> GetAllQuestions()
        {
            var model = await GetDataAsync();
            return Ok(model.questions);
        }
        [HttpGet("NumberList")]
        public async Task<IActionResult> GetAllNumberList()
        {
            var model = await GetDataAsync();
            return Ok(model.numberList);
        }
        [HttpGet("{questionNumber}/{answerNumber}")]
        public async Task<IActionResult> Answer(int questionNumber,int answerNumber)
        {
            var model = await GetDataAsync();    
            return Ok(model.answers.FirstOrDefault(x => x.questionNo == questionNumber && x.answerNo == answerNumber));
        }
    }

    public class LatHtaukBayDin
    {
        public Question[] questions { get; set; }
        public Answer[] answers { get; set; }
        public string[] numberList { get; set; }
    }

    public class Question
    {
        public int questionNo { get; set; }
        public string questionName { get; set; }
    }

    public class Answer
    {
        public int questionNo { get; set; }
        public int answerNo { get; set; }
        public string answerResult { get; set; }
    }

}
