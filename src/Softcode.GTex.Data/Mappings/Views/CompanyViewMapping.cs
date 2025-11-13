using Softcode.GTex.Data.Models.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Softcode.GTex.Data.Mappings.Views
{
    public class CompanyViewMapping : IQueryTypeConfiguration<CompanyView>
    {
        public void Configure(QueryTypeBuilder<CompanyView> builder)
        {
            builder.ToView("CompanyView", "crm");
        }
    }
}
