using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoporteRAG.Application.Models
{
    public class GoldenSetItem
    {
        public string Question { get; set; } = string.Empty;
        public string ExpectedSource { get; set; } = string.Empty;
        public string ExpectedAnswerKeyword { get; set; } = string.Empty;

    }
}
