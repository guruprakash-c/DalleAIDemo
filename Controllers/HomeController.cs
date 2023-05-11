using DalleAIDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using System.Net.Http;

namespace DalleAIDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        string _key = string.Empty;
        public HomeController(IConfiguration _config)
        {
            _key = _config.GetSection("OpenAIKey").Value;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GenerateImages([FromBody]input _input)
        {
            var resp = new ResponseModel();
            using(var _client = new HttpClient())
            {
                _client.DefaultRequestHeaders.Clear();
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _key);
                var _msg = await _client.PostAsync("https://api.openai.com/v1/images/generations", new StringContent(JsonConvert.SerializeObject(_input), Encoding.UTF8, "application/json"));
                if(_msg.IsSuccessStatusCode) { 
                    var _content = await _msg.Content.ReadAsStringAsync();
                    resp = JsonConvert.DeserializeObject<ResponseModel>(_content);
                    resp.statusCode = 200;
                    resp.statusMessage = "Ok";
                }
                else
                {
                    resp.statusCode = 0;
                    resp.statusMessage = _msg.ReasonPhrase;
                }
            }
            return Json(resp);
        }
    }
}