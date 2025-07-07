using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TICRM.DTOs.Base
{
    public class BaseDto : BaseBasicDto
    {
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.Guid> AssignedUser { get; set; }
        public Nullable<System.Guid> AssignedTeam { get; set; }
        public virtual StatusDto Status { get; set; }
        public virtual TeamDto Team { get; set; }
        public virtual UserDto User { get; set; }

    }


    public class BaseBasicDto
    {

        public Nullable<System.Guid> StatusId { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }



}
