using CustomProject.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomProject.Entity
{
    [Table(TableName ="MeqaleYazar",IdentityColumn ="Id",PrimaryColumn ="Id",ForeignColumn ="MeqaleID")]
    public class MeqaleYazarForMeqale
    {
        public int Id { get; set; }
        public int MeqaleID { get; set; }
        public Guid UserID { get; set; }
    }
}
