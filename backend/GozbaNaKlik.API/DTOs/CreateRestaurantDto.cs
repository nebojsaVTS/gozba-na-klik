namespace GozbaNaKlik.API.DTOs
    
{
        public class CreateRestaurantDto
        {
            public string Name { get; set; }

            public string Address { get; set; }

            public string PhoneNumber { get; set; }

            public int OwnerId { get; set; }
        }
    }
