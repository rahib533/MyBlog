﻿using CustomProject.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomProject.Entity
{
    [Table(IdentityColumn = "Id", TableName = "MeqaleTag", PrimaryColumn = "Id", ForeignColumn = "TagID")]
    public class MeqaleTagForTag
    {
        public int MeqaleID { get; set; }
        public int TagID { get; set; }
        public int Id { get; set; }
    }
}
