
namespace Xiaobao.PaaS.Portal.Server.Models
{
    public class AddUserRequest
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 账号是否可用
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public string Role { get; set; }
    }
}