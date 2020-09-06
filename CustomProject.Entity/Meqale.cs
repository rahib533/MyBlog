using CustomProject.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomProject.Entity
{
    [Table(IdentityColumn = "Id", IsActiveColumn = "IsActive", TableName = "Meqale", PrimaryColumn ="Id", ForeignColumn = "KategoriyaID")]
    public class Meqale
    {
        public int Id { get; set; }
        public string Aciqlama { get; set; }
        public string Basliq { get; set; }
        public string Metn { get; set; }
        public System.DateTime Tarixi { get; set; }
        public int KategoriyaID { get; set; }
        public bool IsActive { get; set; }
    }
}
