namespace NZWalksAPI.Models
{
    public class Walk
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public Guid RegionId { get; set; }
        public Guid DifficultyId { get; set; }

        // Navigation properties
        public required Region Region { get; set; }
        public required Difficulty Difficulty { get; set; }
    }
}