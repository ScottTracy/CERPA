using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CERPA.Models
{
    public class OperationsViewModel
    {
        public int Id { get; set; }
        public Order Order { get; set; }
        public Job Job { get; set; }
        public PickOrder PickOrder { get; set; }
        public PropertyValue PropertyValue { get; set; }
        public PartProperty PartProperty { get; set; }
        public VariableValue VariableValue { get; set; }
        public QuantityExpressionValue QuantityExpressionValue { get; set; }
        public VariableExpression VariableExpression { get; set; }
        public InventoryItem InventoryItem { get; set; }
        public PartProcess PartProcess { get; set; }
        public AssemblyProfile AssemblyProfile { get; set; }
        public ChildQuantityExpression ChildQuantityExpression { get; set; }
        public ConfigurableAssemblyVariable ConfigurableAssemblyVariable{get;set;}
        public PartStructure PartStructure { get; set; }
    }

}