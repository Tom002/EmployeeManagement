using EmployeeManagement.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Bll.Dto
{
    public class EmployeeDetailsDto : EmployeeListDto
    {
        public string Username { get; set; } = null!;

        public long BossId { get; set; }

        public string BossName { get; set; } = null!;
    }
}
