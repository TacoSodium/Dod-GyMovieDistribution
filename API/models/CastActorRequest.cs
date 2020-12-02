using System;
using System.Collections.Generic;

namespace API.models
{
    public class CastActorRequest
    {
        public int CastID { get; set; }
        public int ActorNo { get; set; }
        public int MovieNo { get; set; }
    }
}