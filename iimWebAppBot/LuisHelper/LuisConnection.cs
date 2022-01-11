using Microsoft.Bot.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.AI.Luis;
using Microsoft.Bot.Configuration;
using Microsoft.Extensions.Configuration;
using System.Threading;

namespace iimWebAppBot.LuisHelper
{
    public class LuisConnection:IRecognizer
    {
        private readonly LuisRecognizer _luisRecognizer;


        [Obsolete]

        public LuisConnection(IConfiguration configuration)
        {

            string strAppId = configuration["AppId"];
            //string strAuthoringKey = configuration["AuthoringKey"];
            string strSubscriptionKey = configuration["SubscriptionKey"];
            string strRegion = configuration["Region"];
            string strVersion = configuration["Version"];

            var service = new LuisService()
            {
                AppId = strAppId,
                //AuthoringKey = strAuthoringKey,
                SubscriptionKey = strSubscriptionKey,
                Region = strRegion,
                Version = strVersion
            };



            var app = new LuisApplication(service);


            var regOptions = new LuisRecognizerOptionsV2(app)
            {
                IncludeAPIResults = true,
                PredictionOptions = new LuisPredictionOptions()
                {
                    IncludeAllIntents = true,
                    IncludeInstanceData = true


                }
            };



            _luisRecognizer = new LuisRecognizer(regOptions);

        }

        public async Task<RecognizerResult> RecognizeAsync(ITurnContext turnContext, CancellationToken cancellationToken)
        {

            return await _luisRecognizer.RecognizeAsync(turnContext, cancellationToken);

        }

        public async Task<T> RecognizeAsync<T>(ITurnContext turnContext, CancellationToken cancellationToken) where T : IRecognizerConvert, new()
        {
            return await _luisRecognizer.RecognizeAsync<T>(turnContext, cancellationToken);
        }
    }
}
