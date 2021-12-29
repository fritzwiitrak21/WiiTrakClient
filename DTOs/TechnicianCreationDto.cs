﻿namespace WiiTrakClient.DTOs
{
    public class TechnicianCreationDto
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string ProfilePic { get; set; } = string.Empty;

        public Guid SystemOwnerId { get; set; }
    }
}
