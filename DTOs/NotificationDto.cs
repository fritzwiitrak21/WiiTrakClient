namespace WiiTrakClient.DTOs
{
    public class NotificationDto
    {
        public Guid Id { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid From { get; set; }
        public string Message { get; set; } = string.Empty;
        public Guid To { get; set; }
        public DateTime? NotifiedAt { get; set; }
        public bool IsNotified { get; set; }
        public string MessageFrom { get; set; } = string.Empty;
        public int NotificationHour { get; set; }
    }
}
