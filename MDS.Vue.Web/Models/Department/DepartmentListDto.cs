using System.Collections.Generic;

namespace MDS.Vue.Web.Models.Department
{
    public class DepartmentListDto
    {
        public long DirectionId { get; set; }

        public string DirectionName { get; set; }

        public int DepartmentsCount { get; set; }

        public List<DepartmentListItemDto> Departments { get; set; }
    }
}
