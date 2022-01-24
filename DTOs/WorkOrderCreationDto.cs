using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WiiTrakClient.DTOs
{
    public class WorkOrderCreationDto
    {
        public int WorkOrderNumber { get; set; }

        public string Issue { get; set; } = string.Empty;

        public string Notes { get; set; } = string.Empty;

        public string PicUrl { get; set; } = string.Empty;

        public bool IsComplete { get; set; }

        public bool IsAssigned { get; set; }

        public bool IsTrackingDeviceIssue { get; set; }

        public bool IsProvisioning { get; set; }

        public DateTime AssignedAt { get; set; }

        public DateTime CompletedAt { get; set; }

        public Guid? TechnicianId { get; set; }

        public Guid? DriverId { get; set; }

        public Guid? CompanyId { get; set; }

        public Guid? SubContractorId { get; set; }

        public Guid? StoreId { get; set; }

        public Guid? CartId { get; set; }
    }
}