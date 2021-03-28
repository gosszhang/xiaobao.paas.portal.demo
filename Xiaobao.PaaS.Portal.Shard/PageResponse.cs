using System.Collections.Generic;

namespace Xiaobao.PaaS.Portal.Shard
{
    public class PageResponse<T> 
    {
        public int Total { get; set; }

        public List<T> List { get; set; } = new List<T>();
    }
}
