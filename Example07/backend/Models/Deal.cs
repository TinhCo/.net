using System;
using System.Collections.Generic;
namespace backend.Models;

public partial class Deal
{
    public long Id { get; set; }
    public decimal Sale { get; set; }
    public DateTime? Starts { get; set; }
    public DateTime? Ends { get; set;}
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get;set; }

}
