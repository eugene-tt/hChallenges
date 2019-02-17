using SQLite;

namespace Challenge2.Data.Controllers
{
    public abstract class BaseController
    {
        protected DataContext context => DataContext.Instance.Value;
        protected SQLiteConnection db => DataContext.Database;
    }

}
