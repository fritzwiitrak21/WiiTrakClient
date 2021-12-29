namespace WiiTrakClient.DTOs
{
    public class ServiceProviderUpdateDto
    {
        public string ServiceProviderName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhonePrimary { get; set; } = string.Empty;

        public string PhoneSecondary { get; set; } = string.Empty;

        public Guid CustomerAccountId { get; set; }

        public List<StoreDto>? Stores { get; set; }
    }
}
