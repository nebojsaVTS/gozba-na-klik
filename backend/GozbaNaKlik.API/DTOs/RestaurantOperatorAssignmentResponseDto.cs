namespace GozbaNaKlik.API.DTOs
{
    public class RestaurantOperatorAssignmentResponseDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;

        public int RestaurantId { get; set; }
        public string RestaurantName { get; set; } = string.Empty;
    }
}
