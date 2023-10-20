using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAS.Infrastructure.Helpers
{
    public class ViewPaging<T> where T : class
    {
        public IList<T> Items { get; set; }
        public Pagination Pagination { get; set; }

        public ViewPaging(IList<T> items, Pagination pagination)
        {
            Items = items;
            Pagination = pagination;
        }
    }
}
