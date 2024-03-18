using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AsyncDemo.Helpers
{
    public class ComboBoxItem
    {
        public string DisplayText { get; set; }
        public int Value { get; set; }

        public ComboBoxItem(string displayText, int value)
        {
            DisplayText = displayText;
            Value = value;
        }

        public override string ToString()
        {
            return DisplayText;
        }
    }

}
