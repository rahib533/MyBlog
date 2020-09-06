using CustomProject.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomProject.Entity
{
    [Table(IdentityColumn = "Id", IsActiveColumn = "IsActive", TableName = "Rey", PrimaryColumn = "Id", ForeignColumn = "MeqaleID")]
    public class Rey
    {
        public int Id { get; set; }
        public System.Guid UserID { get; set; }
        public string UserName { get; set; }
        public int MeqaleID { get; set; }
        public string Text { get; set; }
        public Nullable<System.DateTime> Tarixi { get; set; }
        public bool IsActive { get; set; }
    }
}
