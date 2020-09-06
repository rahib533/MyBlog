using CustomProject.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomProject.Entity
{
    [Table(IdentityColumn = "Id", TableName = "Beyenme", PrimaryColumn = "Id", ForeignColumn = "UserID")]
    public class LikeForUser
    {
        public int MeqaleID { get; set; }
        public Guid UserID { get; set; }
        public Guid Id { get; set; }
    }
}
