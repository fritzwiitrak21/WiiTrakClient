using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiiTrakClient.Enums;

namespace WiiTrakClient.Features.Drivers.Models
{
    public class CartChange
    {   
        public Guid Id { get; set; }

        public CartCondition Condition { get; set; }

        public CartStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }
        
        
    }
}