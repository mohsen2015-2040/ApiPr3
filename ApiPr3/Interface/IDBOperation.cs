using ApiPr3.Models;

namespace ApiPr3.Interface
{
    public interface IDBOperation
    {
        public IEnumerable<Book> Select(string sqlStatement);

        public int Update(string sqlStatement);

        public int Delete(string sqlStatement);

        public int Insert(string sqlStatement);
    }
}
