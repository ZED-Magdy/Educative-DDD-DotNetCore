namespace Educative.Domain.Entity
{
    public class Paginator
    {
        public int skip { get; }
        public int take { get; }

        public Paginator(int page, int PerPage)
        {
            skip = (page - 1) * PerPage;
            take = PerPage;
        }
    }
}
