namespace Softcode.GTex.Data
{
    public class Repository<TEntity> : BaseRepository<ApplicationDbContext, TEntity> where TEntity : class
    {
        public Repository(ApplicationDbContext context, ILoggedInUserService loggedInUserService)
            : base(context, loggedInUserService)
        {
            
        }

         
    }
}
