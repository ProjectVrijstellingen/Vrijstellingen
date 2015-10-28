﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTP2015.Entities
{
    public class RequestPartimInformation : BaseEntity
    {
        public RequestPartimInformation()
        {
            Status = Status.Untreated;
        }

        public int RequestId { get; set; }
        public int PartimInformationId { get; set; }
        public Status Status { get; set; }

        public virtual Request Request { get; set; }
        public virtual PartimInformation PartimInformation { get; set; }
    }
}