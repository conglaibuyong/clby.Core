namespace clby.Core.MongoDB.Extensions
{
    public interface W<T> { }

    public class InsertOneW<T> : W<T>
    {
        public InsertOneW(T entity)
        {
            this.Document = entity;
        }
        public T Document { get; private set; }
    }
    public class UpdateOneW<T> : W<T>
    {
        public UpdateOneW(Q<T> query, M<T> update)
        {
            this.Query = query;
            this.Update = update;
        }
        public Q<T> Query { get; private set; }
        public M<T> Update { get; private set; }
    }
    public class UpdateManyW<T> : W<T>
    {
        public UpdateManyW(Q<T> query, M<T> update)
        {
            this.Query = query;
            this.Update = update;
        }
        public Q<T> Query { get; private set; }
        public M<T> Update { get; private set; }
    }
    public class ReplaceOneW<T> : W<T>
    {
        public ReplaceOneW(Q<T> query, T document)
        {
            this.Query = query;
            this.Document = document;
        }
        public Q<T> Query { get; private set; }
        public T Document { get; private set; }
    }
    public class DeleteOneW<T> : W<T>
    {
        public DeleteOneW(Q<T> query)
        {
            this.Query = query;
        }
        public Q<T> Query { get; private set; }
    }
    public class DeleteManyW<T> : W<T>
    {
        public DeleteManyW(Q<T> query)
        {
            this.Query = query;
        }
        public Q<T> Query { get; private set; }
    }
}
