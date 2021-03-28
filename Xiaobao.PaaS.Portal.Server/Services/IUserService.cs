using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xiaobao.PaaS.Portal.Server.Models;

namespace Xiaobao.PaaS.Portal.Server.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// 创建账号
        /// </summary>
        /// <param name="addUserModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task AddUserAsync(AddUserRequest addUserModel);

        /// <summary>
        /// 编辑账号
        /// </summary>
        /// <param name="editUserModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task EditUserAsync(EditUserRequest editUserModel);

        /// <summary>
        /// 获取用户列表统计
        /// </summary>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="disable"></param>
        /// <returns></returns>
        Task<int> GetUsersCountAsync(string name);

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="skipCount"></param>
        /// <param name="pageSize"></param>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="disable"></param>
        /// <returns></returns>
        Task<List<UserModel>> GetUsersAsync(int skipCount, int pageSize, string name);
    }
}
