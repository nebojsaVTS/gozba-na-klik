using System.ComponentModel.DataAnnotations;

namespace GozbaNaKlik.API.DTOs
{
    public class CreateOrderRequest
    {
        public int UserId { get; set; }

        public int RestaurantId { get; set; }

        public string FulfillmentType { get; set; } = string.Empty;

        public int? AddressId { get; set; }

        [RegularExpression(
            @"^\+381\d{8,9}$",
            ErrorMessage = "Broj telefona mora sadržati 8 ili 9 cifara nakon +381."
        )]
        public string PhoneNumber { get; set; } = string.Empty;

        public List<CreateOrderItemRequest> Items { get; set; } = new();
    }
}