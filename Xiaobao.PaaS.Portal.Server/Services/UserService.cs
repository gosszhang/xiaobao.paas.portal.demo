using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xiaobao.PaaS.Portal.Shard.Exceptions;
using Xiaobao.PaaS.Portal.Server.Models;

namespace Xiaobao.PaaS.Portal.Server.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class UserService : IUserService
    {
        private readonly static List<UserModel> _users = new List<UserModel>();
        private static int NextId = 1;

        private readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        public UserService(
            IMapper mapper)
        {
            _mapper = mapper;
            InitUsers();
        }

        private void InitUsers()
        {
            if (NextId > 1)
            {
                return;
            }
            var roles = new[] {
                "管理员",
                "项目经理",
                "开发",
                "测试",
                "运维",
            };
            for (var i = 0; i < 20; i++)
            {
                _users.Add(
                    new UserModel
                    {
                        Email = $"zhan{i}@xiaobao.com",
                        Name = $"张{i}",
                        Id = NextId++,
                        Enable = true,
                        Role = roles[i % roles.Length]
                    });
            }
        }

        /// <summary>
        /// 创建账号
        /// </summary>
        /// <param name="addUserModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task AddUserAsync(AddUserRequest addUserModel)
        {
            if (_users.Any(x => x.Email == addUserModel.Email))
            {
                throw new BusinessException("当前邮箱对应的账号已经存在");
            }
            var user = _mapper.Map<UserModel>(addUserModel);
            user.Id = NextId++;
            _users.Add(user);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 编辑账号
        /// </summary>
        /// <param name="editUserModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task EditUserAsync(EditUserRequest editUserModel)
        {
            var existUser = _users.FirstOrDefault(x => x.Id == editUserModel.Id);
            if (existUser == null)
            {
                throw new BusinessException("账号信息不存在");
            }

            if (!existUser.Email.Equals(editUserModel.Email) && _users.Any(x => x.Email == editUserModel.Email))
            {
                throw new BusinessException("编辑后的邮箱已经存在相应账号");
            }

            existUser.Name = editUserModel.Name;
            existUser.Email = editUserModel.Email;
            existUser.Role = editUserModel.Role;
            existUser.Enable = editUserModel.Enable;

            return Task.CompletedTask;
        }

        /// <summary>
        /// 获取用户列表统计
        /// </summary>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="disable"></param>
        /// <returns></returns>
        public Task<int> GetUsersCountAsync(string name)
        {
            return Task.FromResult(_users.Where(x => string.IsNullOrWhiteSpace(name) || x.Name.Contains(name)).Count());
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="skipCount"></param>
        /// <param name="pageSize"></param>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="disable"></param>
        /// <returns></returns>
        public Task<List<UserModel>> GetUsersAsync(int skipCount, int pageSize, string name)
        {
            var users = _users.Where(x => string.IsNullOrWhiteSpace(name) || x.Name.Contains(name)).Skip(skipCount).Take(pageSize).ToList();
            return Task.FromResult(users);
        }
    }
}
