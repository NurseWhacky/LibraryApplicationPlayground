namespace DAL
{
    public interface IRepository//<TEntity> where TEntity : class, new()
    {
        void Create<TEntity>(TEntity entity) where TEntity : class, new();
        IEnumerable<TEntity> FindAll<TEntity>() where TEntity : class, new();
        TEntity? FindById<TEntity>(int entityId) where TEntity : class, new();
        void Update<TEntity>(TEntity entity) where TEntity : class, new();
        void Delete<TEntity>(int entityId) where TEntity : class, new();
        void SaveChanges(); // No-op implementation for xml
    }
}
