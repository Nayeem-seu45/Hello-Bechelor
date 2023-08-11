﻿using Bechelor.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bechelor.Core.Domin.Core
{
    public class UserWiseBill : BaseEntity
    {
        public int UserId { get; set; }
        public int ExpenseCategoryId { get; set; }
        public decimal Amount { get; set; }
    }
}
