
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using iimWebAppBot.Dialogs;
using iimWebAppBot.LuisHelper;

namespace iimWebAppBot.Dialogs
{
    public class MainDialog:ActivityHandler
    {
        private readonly LuisConnection _luis;

        private readonly MessageFormation _message;

        private readonly ILogger _log;

        private readonly LogBuilder _logBuilder;

        public MainDialog(LuisConnection luisHelper, ILogger<MainDialog> logger, IConfiguration configuration)
        {

            _luis = luisHelper;
            _message = new MessageFormation(logger, configuration);
            _log = logger;
            _logBuilder = new LogBuilder();


        }

        //#region OnMembersAddedAsync
        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    //await _message.OnMemberAdded(turnContext, cancellationToken);

                    await turnContext.SendActivityAsync(MessageFactory.Text("..."));

                }
            }
        }
        //#endregion

        //#region OnMessageActivityAsync

        
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            try
            {
                _log.LogInformation(_logBuilder.MethodName().ToString() + " Method starts");
                if (turnContext.Activity.Value == null)
                {
                    _log.LogDebug("Input Message from BOT : " + turnContext.Activity.Text);

                    var luisResult = await _luis.RecognizeAsync<LuisIntent>(turnContext, cancellationToken);

                    var topIntent = luisResult.TopIntent().intent;

                    _log.LogDebug("Top Intent : " + topIntent);

                    switch(topIntent)
                    {
                        //#region MaterialById
                        case LuisIntent.Intent.GetMaterialNumber:
                            _log.LogInformation(topIntent + " intent is triggered");
                            await _message.MaterialIDIntent(turnContext, cancellationToken, luisResult);
                            break;
                    }
                }
            }
            catch(Exception e)
            {
                
                await turnContext.SendActivityAsync(MessageFactory.Text("Sorry!! To get an overview of details" ));
            }
        }



    }
}


