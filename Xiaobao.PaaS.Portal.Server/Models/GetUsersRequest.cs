
using Xiaobao.PaaS.Portal.Shard;

namespace Xiaobao.PaaS.Portal.Server.Models
{
    public class GetUsersRequest : Page
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
    }
}