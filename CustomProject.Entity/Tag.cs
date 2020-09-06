using CustomProject.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomProject.Entity
{
    [Table(IdentityColumn = "Id", IsActiveColumn = "IsActive", TableName = "Tag", PrimaryColumn = "Id")]
    public class Tag
    {
        public int Id { get; set; }
        public string Adi { get; set; }
        public bool IsActive { get; set; }
    }
}
