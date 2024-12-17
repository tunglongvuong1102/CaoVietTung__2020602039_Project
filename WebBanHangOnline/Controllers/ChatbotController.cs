using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace WebBanHangOnline.Controllers
{
    public class ChatbotController : Controller
    {
        // GET: Chatbot
        private readonly OpenAIChat _openAIChat;

        public ChatbotController()
        {
            _openAIChat = new OpenAIChat();
        }

        [HttpPost]
        public async Task<ActionResult> GetChatResponse([System.Web.Http.FromBody] dynamic userInput)
        {
            try
            {
                string input = userInput?.userInput ?? string.Empty;

                if (string.IsNullOrWhiteSpace(input))
                {
                    return Json(new { response = "Vui lòng nhập nội dung để chatbot hỗ trợ." });
                }

                string botResponse = await _openAIChat.GetChatResponse(input);
                return Json(new { response = botResponse });
            }
            catch (Exception ex)
            {
                // Ghi log lỗi nếu cần
                return Json(new { response = "Xin lỗi, đã xảy ra lỗi khi xử lý yêu cầu của bạn." });
            }
        }


        public class OpenAIChat // Định nghĩa ngay trong cùng file
        {
            private readonly string apiKey = "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";
            private readonly string apiUrl = "https://api.openai.com/v1/completions";

            public async Task<string> GetChatResponse(string prompt)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + apiKey);
                    var requestBody = new
                    {
                        model = "text-davinci-003",
                        prompt = prompt,
                        max_tokens = 150,
                        temperature = 0.7
                    };

                    var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(apiUrl, content);
                    var responseString = await response.Content.ReadAsStringAsync();

                    dynamic jsonResponse = JsonConvert.DeserializeObject(responseString);
                    return jsonResponse.choices[0].text.ToString();
                }
            }
        }
    }
}
