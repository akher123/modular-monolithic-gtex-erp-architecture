using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softcode.GTex.Api.Accounting
{
    public static class AccountingRoutePrefix
    {
        private const string AccountingRoutePrefixBase = ApiRoutePrefix.RoutePrefixBase + "accounting/";

        public const string VoucherTypes = AccountingRoutePrefixBase + "voucher-types";
        public const string CompanySectors = AccountingRoutePrefixBase + "company-sectors";
        public const string CostCenters = AccountingRoutePrefixBase + "cost-centers";
        public const string Transactions = AccountingRoutePrefixBase + "transactions";
        public const string ChartOfAccounts = AccountingRoutePrefixBase + "chart-of-accounts";

    }
}