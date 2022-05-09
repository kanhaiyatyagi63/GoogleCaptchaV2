using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace core.GoogleCaptchaV2.Utility
{
    public class ReCaptcha
    {
        private readonly HttpClient _captchaClient;
        private readonly GoogleCaptchaV2 _googleCaptchaV2;

        public ReCaptcha(HttpClient captchaClient, IOptions<GoogleCaptchaV2> configuration)
        {
            _captchaClient = captchaClient;
            _googleCaptchaV2 = configuration.Value;
        }

        public async Task<bool> IsValid(string captcha)
        {
            try
            {
                var postTask = await _captchaClient
                    .PostAsync($"?secret={_googleCaptchaV2.SecretKey}&response={captcha}", new StringContent(""));
                var result = await postTask.Content.ReadAsStringAsync();
                var resultObject = JObject.Parse(result);
                dynamic success = resultObject["success"];
                return (bool)success;
            }
            catch (Exception e)
            {
                // TODO: log this (in elmah.io maybe?)
                return false;
            }
        }
    }
}
