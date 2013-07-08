using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VPFS.Domains;
using EntityFramework.Extensions;
using System.Threading;
using System.ComponentModel;
using System.Collections;

namespace VPFS.ViewModels
{
    class OrderPlacementViewModel : Conductor<object>, INotifyDataErrorInfo
    {
        private Fund_FrontEndEntities db = new Fund_FrontEndEntities();
        private IList<OrderForm> _orderFormList;
        private IList<v_ORDER_FORM_RESULT> _orderFormResultList;
        private IList<TRN_FMODR> _orderList;
        private IList<FRONT_DBF_PRODUCT> productsList;
        private IList<FRONT_DBF_PRODUCT> _productsFilterList;
        private IList<DBF_ORDER_REASON> _orderReasonList;
        private IList<DBF_ORDER_STRATEGY> _orderStrategyList;
        private IList _enquiryList;
        private IList _enquiryItemList;
        private string _enquiryItem;
        private string _orderType;
        private string _productID = "";
        private bool _isProcessing;
        private int _selectedTabIndex;
        private int _totalSelectedFunds;
        private int _totalSelectedQuantity;
        private string _fundsPassRatio;
        private string _quantityPassRatio;
        private string _enquiryType;
        private string _enquiryCriteria;
        private decimal? _productPrice;
        private int _productQty;
        private bool _productPriceEnabled = true;
        private int selectAllToken = 0;
        private int selectClassicToken = 0;
        private decimal? clsPrice = 0;
        private Dictionary<string, IList<string>> _errors = new Dictionary<string, IList<string>>();
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            if (this._errors.ContainsKey(propertyName))
                return this._errors[propertyName];
            return null;
        }

        public bool HasErrors
        {
            get { return (this._errors.Count > 0); }
        }

        public bool IsValid
        {
            get { return !this.HasErrors; }
        }

        public void AddError(string propertyName, string error)
        {
            this._errors[propertyName] = new List<string>() { error };
            this.NotifyErrorsChanged(propertyName);
        }

        public void RemoveError(string propertyName)
        {
            if (this._errors.ContainsKey(propertyName))
                this._errors.Remove(propertyName);
            this.NotifyErrorsChanged(propertyName);
        }

        public void NotifyErrorsChanged(string propertyName)
        {
            if (this.ErrorsChanged != null)
                this.ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public IList<OrderForm> orderFormList
        {
            get
            {
                return _orderFormList;
            }
            set
            {
                _orderFormList = value;
                NotifyOfPropertyChange(() => orderFormList);
            }
        }

        public IList<v_ORDER_FORM_RESULT> orderFormResultList
        {
            get
            {
                return _orderFormResultList;
            }
            set
            {
                _orderFormResultList = value;
                NotifyOfPropertyChange(() => orderFormResultList);
            }
        }

        public IList<TRN_FMODR> orderList
        {
            get
            {
                return _orderList;
            }
            set
            {
                _orderList = value;
                NotifyOfPropertyChange(() => orderList);
            }
        }

        public IList<FRONT_DBF_PRODUCT> productsFilterList
        {
            get
            {
                return _productsFilterList;
            }
            set
            {
                _productsFilterList = value;
                NotifyOfPropertyChange(() => productsFilterList);
            }
        }

        public IList<DBF_ORDER_REASON> orderReasonList
        {
            get
            {
                return _orderReasonList;
            }
            set
            {
                _orderReasonList = value;
                NotifyOfPropertyChange(() => orderReasonList);
            }
        }

        public IList<DBF_ORDER_STRATEGY> orderStrategyList
        {
            get
            {
                return _orderStrategyList;
            }
            set
            {
                _orderStrategyList = value;
                NotifyOfPropertyChange(() => orderStrategyList);
            }
        }

        public IList enquiryList
        {
            get
            {
                return _enquiryList;
            }
            set
            {
                _enquiryList = value;
                NotifyOfPropertyChange(() => enquiryList);
            }
        }

        public IList enquiryItemList
        {
            get
            {
                return _enquiryItemList;
            }
            set
            {
                _enquiryItemList = value;
                NotifyOfPropertyChange(() => enquiryItemList);
            }
        }

        public string enquiryItem
        {
            get
            {
                return _enquiryItem;
            }
            set
            {
                _enquiryItem = value;
                NotifyOfPropertyChange(() => enquiryItem);
            }
        }

        public string orderType
        {
            get
            {
                return _orderType;
            }
            set
            {
                _orderType = value;
                NotifyOfPropertyChange(() => orderType);
            }
        }

        public string productID
        {
            get
            {
                return _productID;
            }
            set
            {
                if (productsList != null)
                {
                    IList<FRONT_DBF_PRODUCT> selectedProduct;
                    selectedProduct = productsList.Where(a => a.ProductID == value).ToList();
                    if (selectedProduct.Count == 0)
                    {
                        AddError("productID", "abc not allowed");
                    }
                    else
                    {
                        RemoveError("productID");
                    }
                }

                _productID = value;
                NotifyOfPropertyChange(() => productID);
            }
        }

        public bool isProcessing
        {
            get
            {
                return _isProcessing;
            }
            set
            {
                _isProcessing = value;
                NotifyOfPropertyChange(() => isProcessing);
                NotifyOfPropertyChange(() => CanOrderCalculateMain);
            }
        }

        public int selectedTabIndex
        {
            get
            {
                return _selectedTabIndex;
            }
            set
            {
                _selectedTabIndex = value;
                NotifyOfPropertyChange(() => selectedTabIndex);
            }
        }

        public int totalSelectedFunds
        {
            get
            {
                return _totalSelectedFunds;
            }
            set
            {
                _totalSelectedFunds = value;
                NotifyOfPropertyChange(() => totalSelectedFunds);
            }
        }

        public int totalSelectedQuantity
        {
            get
            {
                return _totalSelectedQuantity;
            }
            set
            {
                _totalSelectedQuantity = value;
                NotifyOfPropertyChange(() => totalSelectedQuantity);
            }
        }

        public string fundsPassRatio
        {
            get
            {
                return _fundsPassRatio;
            }
            set
            {
                _fundsPassRatio = value;
                NotifyOfPropertyChange(() => fundsPassRatio);
            }
        }

        public string quantityPassRatio
        {
            get
            {
                return _quantityPassRatio;
            }
            set
            {
                _quantityPassRatio = value;
                NotifyOfPropertyChange(() => quantityPassRatio);
            }
        }

        public string enquiryType
        {
            get
            {
                return _enquiryType;
            }
            set
            {
                _enquiryType = value;
                NotifyOfPropertyChange(() => enquiryType);
            }
        }

        public string enquiryCriteria
        {
            get
            {
                return _enquiryCriteria;
            }
            set
            {
                _enquiryCriteria = value;
                NotifyOfPropertyChange(() => enquiryCriteria);
            }
        }

        public decimal? productPrice
        {
            get
            {
                return _productPrice;
            }
            set
            {
                _productPrice = value;
                NotifyOfPropertyChange(() => productPrice);
            }
        }

        public int productQty
        {
            get
            {
                return _productQty;
            }
            set
            {
                _productQty = value;
                NotifyOfPropertyChange(() => productQty);
            }
        }

        public bool productPriceEnabled
        {
            get
            {
                return _productPriceEnabled;
            }
            set
            {
                _productPriceEnabled = value;
                NotifyOfPropertyChange(() => productPriceEnabled);
            }
        }

        public void SelectAll()
        {
            selectClassicToken = 0;

            if (selectAllToken == 0)
            {
                selectAllToken = 1;
                for (int i = 0; i < orderFormList.Count; i++)
                {
                    if (orderFormList[i].autoSelect == "Y")
                    {
                        orderFormList[i].selected = true;
                    }
                    else
                    {
                        orderFormList[i].selected = false;
                    }
                }
            }
            else if (selectAllToken == 1)
            {
                selectAllToken = 0;
                for (int i = 0; i < orderFormList.Count; i++)
                {
                    orderFormList[i].selected = false;
                }
            }

            selectClassicToken = 0;
            OrderSummary();
        }

        public void SelectClassic()
        {
            selectAllToken = 0;

            if (selectClassicToken == 0)
            {
                selectClassicToken = 1;
                for (int i = 0; i < orderFormList.Count; i++)
                {
                    if (orderFormList[i].manageApproach == "CLASSIC")
                    {
                        orderFormList[i].selected = true;
                    }
                    else
                    {
                        orderFormList[i].selected = false;
                    }
                }
            }
            else if (selectClassicToken == 1)
            {
                selectClassicToken = 0;
                for (int i = 0; i < orderFormList.Count; i++)
                {
                    orderFormList[i].selected = false;
                }
            }

            OrderSummary();
        }

        public void OrderSummary()
        {
            totalSelectedFunds = 0;
            totalSelectedQuantity = 0;

            for (int i = 0; i < orderFormList.Count; i++)
            {
                if (orderFormList[i].selected)
                {
                    totalSelectedFunds++;
                    totalSelectedQuantity += orderFormList[i].qty;
                }
            }
        }

        public void UpdatePrice(decimal price)
        {
            for (int i = 0; i < orderFormList.Count; i++)
            {
                orderFormList[i].price = price;
            }
        }

        public void UpdateCD(bool cd)
        {
            clsPrice = 0;

            if (cd)
            {
                productPriceEnabled = false;

                if (productsList != null)
                {
                    IList<FRONT_DBF_PRODUCT> selectedProduct;
                    selectedProduct = productsList.Where(a => a.ProductID == productID).ToList();
                    if (selectedProduct.Count != 0)
                    {
                        clsPrice = selectedProduct[0].ClsPrice;
                    }
                }

                for (int i = 0; i < orderFormList.Count; i++)
                {
                    orderFormList[i].cd = "Y";
                    orderFormList[i].price = 0;
                }

                productPrice = clsPrice;
            }
            else
            {
                productPriceEnabled = true;

                for (int i = 0; i < orderFormList.Count; i++)
                {
                    orderFormList[i].cd = "N";
                    orderFormList[i].price = productPrice;
                }
            }
        }

        public void UpdateQty(int qty)
        {
            for (int i = 0; i < orderFormList.Count; i++)
            {
                orderFormList[i].qty = qty;
            }

            OrderSummary();
        }

        public void UpdateReason(string reason)
        {
            if (reason != "")
            {
                for (int i = 0; i < orderFormList.Count; i++)
                {
                    orderFormList[i].reason = reason;
                }
            }
        }

        public void UpdateStrategy(string strategy)
        {
            if (strategy != "")
            {
                for (int i = 0; i < orderFormList.Count; i++)
                {
                    orderFormList[i].strategy = strategy;
                }
            }
        }

        public void PopulateProductsData()
        {
            string[] criteria = productID.Split('|');
            int criteriaCount = criteria.Count();

            if (productID.Count() == 1)
            {
                IQueryable<FRONT_DBF_PRODUCT> productsQuery = from a in db.FRONT_DBF_PRODUCT
                                                              where a.ProductType != "GOLDBAR"
                                                              select a;
                productsList = productsQuery.ToList();
            }

            productsFilterList = productsList;

            if (criteriaCount >= 1 && criteria[0].Trim() != "")
            {
                productsFilterList = productsFilterList.Where(a => a.ProductID.StartsWith(criteria[0].Trim(), StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (criteriaCount >= 2 && criteria[1].Trim() != "")
            {
                productsFilterList = productsFilterList.Where(a => a.ProductType != null && a.ProductType.StartsWith(criteria[1].Trim(), StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (criteriaCount >= 3 && criteria[2].Trim() != "")
            {
                productsFilterList = productsFilterList.Where(a => a.ExchID != null && a.ExchID.StartsWith(criteria[2].Trim(), StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (criteriaCount >= 4 && criteria[3].Trim() != "")
            {
                productsFilterList = productsFilterList.Where(a => a.ProductName != null && a.ProductName.IndexOf(criteria[3].Trim(), StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            }
        }

        public void OrderCalculate()
        {
            BUF_FMODR newOrder = new BUF_FMODR();
            int passFunds = 0;
            int totalFunds = 0;
            int? passQty = 0;
            int? totalQty = 0;

            db.BUF_FMODR.Delete(a => a.Updatedby == "CHEAH");
            db.LOG_FMODR_ERROR.Delete(a => a.UpdatedBy == "CHEAH");

            for (int i = 0; i < orderFormList.Count; i++)
            {
                if (orderFormList[i].selected)
                {
                    newOrder.FundID = orderFormList[i].fundID;

                    newOrder.ProductID = productID;
                    newOrder.LSFlag = orderType[0].ToString();
                    newOrder.BSFlag = orderType[1].ToString() + "%";
                    newOrder.Unit = orderFormList[i].qty;
                    newOrder.FMID = "CHEAH";
                    newOrder.Reason = orderFormList[i].reason;
                    newOrder.Price = orderFormList[i].price;
                    newOrder.Tgtwgt = 0;
                    newOrder.OdrType = " ";
                    newOrder.Style = "Semi-Aggressive";
                    newOrder.Priority = orderFormList[i].priority;
                    newOrder.CD = orderFormList[i].cd;
                    newOrder.Updatedby = "CHEAH";
                    newOrder.Updatedon = DateTime.Now;
                    newOrder.ActType = "";
                    newOrder.Strategy = orderFormList[i].strategy;

                    db.BUF_FMODR.Add(newOrder);
                    db.SaveChanges();
                }
            }

            db.sp_cal_fmodr_v2();
            IQueryable<v_ORDER_FORM_RESULT> orderFormResultQuery = from a in db.v_ORDER_FORM_RESULT select a;
            orderFormResultList = orderFormResultQuery.ToList();

            for (int i = 0; i < orderFormResultList.Count; i++)
            {
                if (orderFormResultList[i].errcode == null)
                {
                    passFunds++;
                    passQty += orderFormResultList[i].qty;
                }

                totalFunds++;
                totalQty += orderFormResultList[i].qty;
            }

            fundsPassRatio = passFunds + " / " + totalFunds;
            quantityPassRatio = passQty + " / " + totalQty;
            selectedTabIndex = 1;
            isProcessing = false;
        }

        public bool CanOrderCalculateMain
        {
            get { return isProcessing ? false : true; }
        }

        public void OrderCalculateMain()
        {
            isProcessing = true;
            Thread calThread = new Thread(new ThreadStart(OrderCalculate));
            calThread.Start();
        }

        public void OrderSave()
        {
            db.sp_ins_fmodr_v2();
            db.sp_sys_email_alert_fm("CHEAH");
            db.sp_sys_email_alert_ss("C");

            IQueryable<TRN_FMODR> orderQuery = from a in db.TRN_FMODR
                                               where a.Posted == "N"
                                               orderby a.OdrTime descending
                                               select a;
            orderList = orderQuery.ToList();

            selectedTabIndex = 0;
        }

        public void CustomizeColumns(DataGridAutoGeneratingColumnEventArgs e)
        {
            DataGridTextColumn txtColumn = e.Column as DataGridTextColumn;

            switch (e.Column.Header.ToString())
            {
                case "holdingType":
                    e.Column.Header = "Type";
                    break;
                case "fundAlias":
                    e.Column.Header = "Fund";
                    break;
                case "productID":
                    e.Column.Header = "Product";
                    break;
                case "productName":
                    e.Column.Header = "Product Name";
                    e.Column.Width = 150;
                    break;
                case "valQty":
                    e.Column.Header = "Quantity";
                    txtColumn.Binding.StringFormat = "{0:#0;(#0)}";
                    break;
                case "prodCCY":
                    e.Column.Header = "CCY";
                    break;
                case "fundCCY":
                    e.Column.Header = "CCY";
                    break;
                case "fundBal":
                    e.Column.Header = "Fund Size";
                    txtColumn.Binding.StringFormat = "{0:#,000.00;(#,000.00)}";
                    break;
                case "cashOnHand":
                    e.Column.Header = "Cash On Hand";
                    txtColumn.Binding.StringFormat = "{0:#,000.00;(#,000.00)}";
                    break;
                case "cashOnHandWgt":
                    e.Column.Header = "Wgt %";
                    txtColumn.Binding.StringFormat = "{0:#0.00;(#0.00)}";
                    break;
                case "pendCash":
                    e.Column.Header = "Pending Cash";
                    txtColumn.Binding.StringFormat = "{0:#,000.00;(#,000.00)}";
                    break;
                case "cashRemained":
                    e.Column.Header = "Cash Remained";
                    txtColumn.Binding.StringFormat = "{0:#,000.00;(#,000.00)}";
                    break;
                case "cashRemainedWgt":
                    e.Column.Header = "Wgt %";
                    txtColumn.Binding.StringFormat = "{0:#0.00;(#0.00)}";
                    break;
                case "lastPrice":
                    e.Column.Header = "Price";
                    txtColumn.Binding.StringFormat = "{0:#0.00;(#0.00)}";
                    break;
                case "avgCost":
                    e.Column.Header = "Avg Cost";
                    txtColumn.Binding.StringFormat = "{0:#0.00;(#0.00)}";
                    break;
                case "profit":
                    e.Column.Header = "P/L %";
                    txtColumn.Binding.StringFormat = "{0:#0.00;(#0.00)}";
                    break;
                case "weighting":
                    e.Column.Header = "WGT %";
                    txtColumn.Binding.StringFormat = "{0:#0.00;(#0.00)}";
                    break;
                case "issuedShareWeighting":
                    e.Column.Header = "% of Issued Share";
                    txtColumn.Binding.StringFormat = "{0:#0.00;(#0.00)}";
                    break;
                default:
                    e.Column.Header = "";
                    break;
            }

        }

        public void ListEnquiryItem()
        {
            if (enquiryType == "fund")
            {

                IQueryable<QryFundAlias> fundAliasQuery = from a in db.FRONT_DBF_FUND
                                                          select new QryFundAlias
                                                            {
                                                                fundAlias = a.FundAlias
                                                            };
                enquiryItemList = fundAliasQuery.ToList();
                enquiryItem = "fundAlias";
            }
            else if (enquiryType == "stock")
            {
                IQueryable<QryProductID> productIDQuery = from a in db.FRONT_DBF_PRODUCT
                                                          where a.ProductType != "GOLDBAR"
                                                          select new QryProductID
                                                          {
                                                              productID = a.ProductID
                                                          };
                enquiryItemList = productIDQuery.ToList();
                enquiryItem = "productID";
            }
            else if (enquiryType == "cash")
            {
                enquiryItemList = new string[] { "Fund Cash", "TWD Cash" };
                enquiryItem = "";
            }
        }

        public void EnquiryPortfolio()
        {
            if (enquiryType == "fund")
            {
                IQueryable<QryFundPortfolio> portfolioQuery = from a in db.FRONT_BAL_PORTFOLIO
                                                              join b in db.FRONT_DBF_FUND on a.FundID equals b.FundID
                                                              join c in db.FRONT_DBF_PRODUCT on a.ProductID equals c.ProductID
                                                              where b.FundAlias == enquiryCriteria && a.ProdType != "C"
                                                              select new QryFundPortfolio
                                                                     {
                                                                         holdingType = (a.Type == "18" ? "D" :
                                                                                        a.Type == "19" ? "SS" :
                                                                                        a.Type == "15" ? "L" :
                                                                                        " "),
                                                                         productID = a.ProductID,
                                                                         productName = c.ProductName,
                                                                         valQty = a.ValQty.Value,
                                                                         prodCCY = a.ProdCCY,
                                                                         lastPrice = c.LastPrice,
                                                                         avgCost = a.AvgCost,
                                                                         profit = (a.AvgCost == 0 ? 0 :
                                                                                   a.ValQty.Value > 0 ? (c.LastPrice.Value - a.AvgCost.Value) / a.AvgCost.Value :
                                                                                   (a.AvgCost - c.LastPrice) / a.AvgCost),
                                                                         weighting = (a.ProdType == "F" ? a.ValQty * a.ValPrice * a.ProdTypeMultiplier / a.FxRate : a.ProdBal_FundCCY) * 100 / a.FundBal
                                                                     };
                enquiryList = portfolioQuery.ToList();
            }
            else if (enquiryType == "stock")
            {
                IQueryable<QryStockPortfolio> portfolioQuery = from a in db.FRONT_BAL_PORTFOLIO
                                                               join b in db.FRONT_DBF_FUND on a.FundID equals b.FundID
                                                               join c in db.FRONT_DBF_PRODUCT on a.ProductID equals c.ProductID
                                                               where a.ProductID == enquiryCriteria && a.ProdType != "C"
                                                               select new QryStockPortfolio
                                                               {
                                                                   fundAlias = b.FundAlias,
                                                                   lastPrice = c.LastPrice,
                                                                   avgCost = a.AvgCost,
                                                                   profit = (a.AvgCost == 0 ? 0 :
                                                                             a.ValQty.Value > 0 ? (c.LastPrice.Value - a.AvgCost.Value) / a.AvgCost.Value :
                                                                             (a.AvgCost - c.LastPrice) / a.AvgCost),
                                                                   valQty = a.ValQty.Value,
                                                                   weighting = (a.ProdType == "F" ? a.ValQty * a.ValPrice * a.ProdTypeMultiplier / a.FxRate : a.ProdBal_FundCCY) * 100 / a.FundBal,
                                                                   issuedShareWeighting = (c.IssuedShare == 0 ? 0 : a.ValQty * 100 / c.IssuedShare)
                                                               };
                enquiryList = portfolioQuery.ToList();
            }
            else if (enquiryType == "cash")
            {
                if (enquiryCriteria == "Fund Cash")
                {
                    IQueryable<QryCashPortfolio> portfolioQuery = from a in db.v_QRY_FUND_CASH
                                                                  where a.userid == "CHEAH"
                                                                  select new QryCashPortfolio
                                                                  {
                                                                      fundAlias = a.fundalias,
                                                                      fundCCY = a.fundccy,
                                                                      fundBal = a.fundbal,
                                                                      cashOnHand = a.cashonhand,
                                                                      cashOnHandWgt = a.cashonhand * 100 / a.fundbal,
                                                                      pendCash = a.pendcash,
                                                                      cashRemained = a.cashremained,
                                                                      cashRemainedWgt = a.cashremained * 100 / a.fundbal
                                                                  };
                    enquiryList = portfolioQuery.ToList();
                }
                else if (enquiryCriteria == "TWD Cash")
                {
                    IQueryable<QryCashPortfolio> portfolioQuery = from a in db.v_QRY_FUND_CASH_TWD
                                                                  where a.userid == "CHEAH"
                                                                  select new QryCashPortfolio
                                                                  {
                                                                      fundAlias = a.fundalias,
                                                                      fundCCY = a.fundccy,
                                                                      fundBal = a.fundbal,
                                                                      cashOnHand = a.cashonhand,
                                                                      cashOnHandWgt = a.cashonhand * 100 / a.fundbal,
                                                                      pendCash = a.pendcash,
                                                                      cashRemained = a.cashremained,
                                                                      cashRemainedWgt = a.cashremained * 100 / a.fundbal
                                                                  };
                    enquiryList = portfolioQuery.ToList();
                }

            }

        }

        public void LoadWindow()
        {
            IQueryable<OrderForm> orderFormQuery = from a in db.FRONT_DBF_FUND
                                                   join b in db.FRONT_ADM_USERFUND on a.FundID equals b.FundID
                                                   where b.UserID == "CHEAH"
                                                   select new OrderForm
                                                   {
                                                       fundID = a.FundID,
                                                       fundAlias = a.FundAlias,
                                                       manageApproach = a.ManageApproach,
                                                       autoSelect = a.Autoselect,
                                                       cd = "N",
                                                       priority = 90
                                                   };
            orderFormList = orderFormQuery.ToList();

            IQueryable<DBF_ORDER_REASON> orderReasonQuery = from a in db.DBF_ORDER_REASON select a;
            orderReasonList = orderReasonQuery.ToList();

            IQueryable<DBF_ORDER_STRATEGY> orderStrategyQuery = from a in db.DBF_ORDER_STRATEGY select a;
            orderStrategyList = orderStrategyQuery.ToList();

            IQueryable<TRN_FMODR> orderQuery = from a in db.TRN_FMODR
                                               where a.Posted == "N"
                                               orderby a.OdrTime descending
                                               select a;
            orderList = orderQuery.ToList();
        }
    }
}
