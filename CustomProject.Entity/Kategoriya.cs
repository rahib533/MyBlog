using CustomProject.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomProject.Entity
{
    [Table(IdentityColumn ="Id", IsActiveColumn ="IsActive", TableName = "Kategoriya")]
    public class Kategoriya
    {
        public int Id { get; set; }
        public string Adi { get; set; }
        public string Aciqlama { get; set; }
        public bool IsActive { get; set; }
    }
}
