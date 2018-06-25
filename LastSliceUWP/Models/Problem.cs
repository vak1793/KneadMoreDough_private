using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastSliceUWP.Models
{
  class Problem
  {
    public string Id { get; set; }
    public Dictionary<string, string[]> Puzzle { get; set; }
    public DateTime TimeIssued { get; set; }
  }
}
