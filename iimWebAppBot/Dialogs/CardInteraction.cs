using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iimWebAppBot.Dialogs;
using System.IO;
using AdaptiveCards.Templating;
using iimWebAppBot.Models;
using Microsoft.Bot.Builder;
using Newtonsoft.Json;

namespace iimWebAppBot.Dialogs
{
    public class CardInteraction
    {
        private readonly ILogger _log;
        private readonly LogBuilder _logBuilder;

        public CardInteraction(ILogger<MainDialog> logger)
        {
            _log = logger;
            _logBuilder = new LogBuilder();

        }
        //#region Cards Formation 

        
        public IMessageActivity MaterialByID(MaterialByIdDb dbResult)
        {
            try
            {
                _log.LogInformation(_logBuilder.MethodName() + " Method starts");

                string strJsonFile = Path.Combine(".", "Cards", "Material.json");

                _log.LogDebug("Json file is" + strJsonFile);

                string strPreBindedCard = File.ReadAllText(strJsonFile);

                _log.LogDebug("strPreBindedCard:" + strPreBindedCard);

                AdaptiveCardTemplate template = new AdaptiveCardTemplate(strPreBindedCard);
                var myData = new
                {
                    Material = dbResult.Material.ToString(),
                    AvgConsumption = dbResult.AvgConsumption.ToString(),
                    PredictedDemand = dbResult.PredictedDemand.ToString(),
                    LeadTime = dbResult.LeadTime.ToString(),
                    CurrentInventory = dbResult.CurrentInventory.ToString(),
                    OrdersInPipeline = dbResult.OrdersInPipeline.ToString(),
                    QuantityToOrder = dbResult.QuantityToOrder.ToString(),
                    Reorderdate = dbResult.Reorderdate.ToString(),
                    UnitPrice = dbResult.UnitPrice.ToString(),
                    PredictedCapex = dbResult.PredictedCapex.ToString(),
                    predictedcapexwithharvest = dbResult.predictedcapexwithharvest.ToString(),
                    openharvestqty = dbResult.openharvestqty.ToString(),
                    InventoryCapex = dbResult.InventoryCapex.ToString(),
                    ReorderPoint = dbResult.ReorderPoint.ToString(),
                    InventoryExhaustDate = dbResult.InventoryExhaustDate.ToString(),
                    //StockLevel = dbResult.StockLevel.ToString(),
                    
                };

                _log.LogInformation("Data binded to the card sucessfully");

                string strPostBindedCard = template.Expand(myData);

                _log.LogDebug("strPostBindedCard : " + strPostBindedCard);

                return MessageFactory.Attachment(AdaptiveCards(strPostBindedCard));
            }
            catch (Exception e)
            {

                _log.LogError(_logBuilder.ErrorsFormation(e));

                return MessageFactory.Text("Something went wrong...");
            }
        }
        public Attachment AdaptiveCards(string cardJson)
        {
            var adaptiveCardAttachment = new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject(cardJson),
            };

            return adaptiveCardAttachment;

        }
    }
   
}
