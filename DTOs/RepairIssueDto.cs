﻿using System.ComponentModel.DataAnnotations;

namespace WiiTrakClient.DTOs
{
    public class RepairIssueDto
    {
        public Guid Id { get; set; }

        public string? Issue { get; set; } = string.Empty;

        public string? Description { get; set; } = string.Empty;
    }
}
