using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FormBot.Dialogs
{
    public class AutomationParameter
    {
        public string name { get; set; }
        public object value { get; set; }
        public object type { get; set; }
        public int order { get; set; }
        public bool secret { get; set; }
        public bool optional { get; set; }
        public object defaultValue { get; set; }
        public string displayName { get; set; }
        public string extension { get; set; }
        public object poolCredential { get; set; }
    }
}