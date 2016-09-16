namespace MyMVCBookStore.Data
{
    using System.Linq;
    using DAL.Entities;

    public interface IDemoData
    {
        IQueryable<Book> GetBooks();
    }
}
