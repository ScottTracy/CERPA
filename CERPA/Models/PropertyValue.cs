﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CERPA.Models
{
    public class PropertyValue
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int PropertyId { get; set; }
        public string ExpressionResult { get; set; }

    }
}