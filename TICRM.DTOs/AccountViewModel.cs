using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TICRM.DTOs
{
    public class AccountViewModel
    {
        public UserDto loggedInUser { get; set; }
        public AccountDto account { get; set; }
        public List<OpportunityDto> accountOppertunities { get; set; }
        public List<LocationDto> accountLocations { get; set; }
        public List<DeviceDto> accountDevices { get; set; }
        public List<CustomerAssetDto> accountAssetes { get; set; }
        public List<ActivityDTO> accountActivity { get; set; }



    }
}
