using iimWebAppBot.DbInteraction;
using iimWebAppBot.Models;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using iimWebAppBot.LuisHelper;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime.Models;





namespace iimWebAppBot.Dialogs
{
    public class MessageFormation
    {
        private readonly CallRepo _dbInput;
        private readonly string _newLine;
        private readonly TextInfo _text;
        private readonly ILogger _log;
        private readonly LogBuilder _logBuilder;
        private readonly CardInteraction _card;



        public MessageFormation(ILogger<MainDialog> logger, IConfiguration configuration)
        {
            _dbInput = new CallRepo(logger, configuration);
            _log = logger;
            _logBuilder = new LogBuilder();
            _newLine = Environment.NewLine;
            _text = CultureInfo.CurrentCulture.TextInfo;
            _card = new CardInteraction(logger);

        }
        public async Task MaterialIDIntent(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken, LuisIntent luisResult)
        {
            try
            {

                var Matnr = luisResult.Entities.MaterialNumber[0];
                
                _log.LogDebug("strMaterialID : " + Matnr);

                MaterialByIdDb  dbResult = _dbInput.GetMaterialById(Matnr);

                var reply = _card.MaterialByID(dbResult);

                await turnContext.SendActivityAsync(reply);


            }
            catch (Exception e)
            {

                _log.LogError(_logBuilder.ErrorsFormation(e));

                //return MessageFactory.Text("Something went wrong...");
                await turnContext.SendActivityAsync("Something went wrong...");

            }
        }


    }
}
