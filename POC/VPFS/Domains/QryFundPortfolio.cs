using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPFS.Domains
{
    class QryFundPortfolio
    {
        public virtual string holdingType { get; set; }
        public virtual string productID { get; set; }
        public virtual string productName { get; set; }
        public virtual string prodCCY { get; set; }
        public virtual decimal? valQty { get; set; }
        public virtual decimal? lastPrice { get; set; }
        public virtual decimal? avgCost { get; set; }
        public virtual decimal? profit { get; set; }
        public virtual decimal? weighting { get; set; }
    }
}
