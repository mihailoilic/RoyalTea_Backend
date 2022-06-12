using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Application.UseCases.DTO
{
    public class AddressDto
    {
        public int Id { get; set; }
        public string DeliveryLocation { get; set; }
        public string CountryName { get; set; }

    }

    public class UpdateAddressDto : AddAddressDto
    {
        public int Id { get; set; }
    }

    public class AddAddressDto
    {
        public string DeliveryLocation { get; set; }
        public int CountryId { get; set; }
    }
}
