using System.Collections.Generic;

namespace MDS.Vue.Core
{
    public class OrgUnit
    {
        public long Id { get; set; }

        public long? ParentId { get; set; }

        public string Name { get; set; }

        public string Position { get; set; }

        public UnitType Type { get; set; }

        public OrgUnit Parent { get; set; }

        public virtual IEnumerable<OrgUnit> Children { get; set; }

        public bool IsHeadOfOrgUnit { get; set; } = false;

        public bool Deleted { get; set; } = false;
    }
}
