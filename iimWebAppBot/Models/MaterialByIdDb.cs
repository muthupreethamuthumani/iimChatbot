using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iimWebAppBot.Models
{
    public class MaterialByIdDb
    {
        public string Material { get; set; }
        public int AvgConsumption { get; set; }
        public int PredictedDemand { get; set; }
        public int LeadTime { get; set; }
        public int CurrentInventory { get; set; }
        public double OrdersInPipeline { get; set; }
        public double QuantityToOrder { get; set; }
        public DateTime Reorderdate { get; set; }
        public double UnitPrice { get; set; }
        public double PredictedCapex { get; set; }
        public double predictedcapexwithharvest { get; set; }
        public double openharvestqty { get; set; }
        public double InventoryCapex { get; set; }
        public int ReorderPoint { get; set; }
        public DateTime InventoryExhaustDate { get; set; }
        public string StockLevel { get; set; }
        //public object Entities { get; internal set; }
    }
}
