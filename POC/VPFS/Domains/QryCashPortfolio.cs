using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPFS.Domains
{
    class QryCashPortfolio
    {
        public virtual string fundAlias { get; set; }
        public virtual string fundCCY { get; set; }
        public virtual decimal? fundBal { get; set; }
        public virtual decimal? cashOnHand { get; set; }
        public virtual decimal? cashOnHandWgt { get; set; }
        public virtual decimal? pendCash { get; set; }
        public virtual decimal? cashRemained { get; set; }
        public virtual decimal? cashRemainedWgt { get; set; }
    }
}
