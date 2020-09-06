using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomProject.Common
{
    public class Table:Attribute
    {
        public string TableName { get; set; }
        public string PrimaryColumn { get; set; }
        public string IdentityColumn { get; set; }
        public string ForeignColumn { get; set; }
        public string IsActiveColumn { get; set; }
    }
}
