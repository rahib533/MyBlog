using CustomProject.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomProject.Entity
{
    [Table(IdentityColumn = "Id", IsActiveColumn = "IsActive", TableName = "Photos", PrimaryColumn = "Id", ForeignColumn = "MeqaleID")]
    public class PhotosForMeqale
    {
        public int Id { get; set; }
        public string KicikYol { get; set; }
        public string OrtaYol { get; set; }
        public string BoyukYol { get; set; }
        public Nullable<int> MeqaleID { get; set; }
        public Nullable<int> KategoriyaID { get; set; }
        public bool Main { get; set; }
        public bool IsActive { get; set; }
    }
}
