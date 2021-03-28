using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Xiaobao.PaaS.Portal.Server.Models;
using Xiaobao.PaaS.Portal.Server.Services;
using Xiaobao.PaaS.Portal.Shard;

namespace Xiaobao.PaaS.Portal.Server.Controller
{
    /// <summary>
    /// 
    /// </summary>
    public class UserController : BaseControllerApi
    {
        private readonly IUserService _userService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="userService"></param>
        public UserController(
            IUserService userService
            )
        {
            _userService = userService;
        }

        /// <summary>
        /// 获取账号列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseResult<PageResponse<UserModel>>> GetUsers([FromBody] GetUsersRequest request)
        {
            var pageResponse = new PageResponse<UserModel>
            {
                Total = await _userService.GetUsersCountAsync(request.Name)
            };
            if (pageResponse.Total > 0)
            {
                pageResponse.List = await _userService.GetUsersAsync(
                    (request.Index - 1) * request.Size, request.Size, request.Name
                    );
            }
            return new ResponseResult<PageResponse<UserModel>>
            {
                Data = pageResponse
            };
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseResult<bool>> AddUser([FromBody] AddUserRequest request)
        {
            await _userService.AddUserAsync(request);
            return new ResponseResult<bool>();
        }

        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseResult<bool>> EditUser([FromBody] EditUserRequest request)
        {
            await _userService.EditUserAsync(request);
            return new ResponseResult<bool>();
        }
    }
}
