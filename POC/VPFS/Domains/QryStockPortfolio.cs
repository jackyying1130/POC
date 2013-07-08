using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPFS.Domains
{
    class QryStockPortfolio
    {
        public virtual string fundAlias { get; set; }
        public virtual decimal? lastPrice { get; set; }
        public virtual decimal? avgCost { get; set; }
        public virtual decimal? profit { get; set; }
        public virtual decimal? valQty { get; set; }
        public virtual decimal? weighting { get; set; }
        public virtual decimal? issuedShareWeighting { get; set; }
    }
}
