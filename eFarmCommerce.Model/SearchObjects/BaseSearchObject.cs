using System;
using System.Collections.Generic;
using System.Text;

namespace eFarmCommerce.Model.SearchObjects
{
    public class BaseSearchObject
    {
        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}
