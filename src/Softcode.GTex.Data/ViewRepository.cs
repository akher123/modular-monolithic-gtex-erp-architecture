namespace Softcode.GTex.Data
{
    public class ViewRepository<TEntity> : BaseViewRepository<ApplicationViewDbContext, TEntity> where TEntity : class
    {
        public ViewRepository(ApplicationViewDbContext context)
            : base(context)
        {
            
        }
    }
}
