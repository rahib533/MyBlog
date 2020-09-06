using CustomProject.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomProject.Entity
{
    [Table(IdentityColumn = "Id", TableName = "Beyenme", PrimaryColumn = "Id", ForeignColumn = "MeqaleID")]
    public class LikeForMeqale
    {
        public int MeqaleID { get; set; }
        public Guid UserID { get; set; }
        public int Id { get; set; }
    }
}
