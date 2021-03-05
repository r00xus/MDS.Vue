using System.Collections.Generic;

namespace MDS.Vue.Web.Models.Employee
{
    public class EmployeeListDto
    {
        public long? DirectionId { get; set; }

        public string DirectionName { get; set; }

        public long DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public int EmployeesCount { get; set; }

        public string HeadName { get; set; }

        public string HeadPosition { get; set; }

        public List<EmployeeListItemDto> Employees { get; set; }
    }
}
