using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPFS.Domains
{
    class OrderForm : PropertyChangedBase
    {
        private bool _selected;
        private string _cd;
        private decimal? _price;
        private int _qty;
        private string _reason;
        private string _strategy;

        public bool selected
        {
            get
            {
                return _selected;
            }
            set
            {
                _selected = value;
                NotifyOfPropertyChange(() => selected);
            }
        }
        public virtual string fundID { get; set; }
        public virtual string fundAlias { get; set; }
        public string cd
        {
            get
            {
                return _cd;
            }
            set
            {
                _cd = value;
                NotifyOfPropertyChange(() => cd);
            }
        }
        public decimal? price
        {
            get
            {
                return _price;
            }
            set
            {
                _price = value;
                NotifyOfPropertyChange(() => price);
            }
        }
        public int qty
        {
            get
            {
                return _qty;
            }
            set
            {
                _qty = value;
                NotifyOfPropertyChange(() => qty);
            }
        }
        public virtual int priority { get; set; }
        public string reason
        {
            get
            {
                return _reason;
            }
            set
            {
                _reason = value;
                NotifyOfPropertyChange(() => reason);
            }
        }
        public string strategy
        {
            get
            {
                return _strategy;
            }
            set
            {
                _strategy = value;
                NotifyOfPropertyChange(() => strategy);
            }
        }
        public virtual string manageApproach { get; set; }
        public virtual string autoSelect { get; set; }
    }
}
