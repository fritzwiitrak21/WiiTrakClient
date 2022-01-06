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

        public AssetCondition Condition { get; set; }

        public AssetStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }
        
        
    }
}