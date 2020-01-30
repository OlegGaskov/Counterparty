using System;
using System.Collections.Generic;
using System.Text;

namespace Dadata.SmallApiClient.Models.ResponceModels
{
    public class Suggestions
    {
        public string unrestricted_value { get; set; }

        public SuggestionsData data { get; set; }
    }
}
