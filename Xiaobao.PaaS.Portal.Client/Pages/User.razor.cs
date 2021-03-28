using AntDesign;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using PaasPortalSdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xiaobao.PaaS.Portal.Client.Options;

namespace Xiaobao.PaaS.Portal.Client.Pages
{
    public partial class User : ComponentBase
    {
        UserModel _userModel = new UserModel();

        DrawerComponentOption _userDrawerComponentOption = new DrawerComponentOption();

        List<string> _rolesOptions = new List<string>();

        UserModelPageResponse _userPageData = new UserModelPageResponse
        {
            List = new List<UserModel>()
        };


        GetUsersRequest _pageRequest = new GetUsersRequest
        {
            Index = 1,
            Size = 10,
            Name = string.Empty
        };

        ITable table;
        bool loading = false;
        private bool Loading
        {
            get { return loading; }
            set
            {
                loading = value;
                InvokeAsync(base.StateHasChanged);
            }
        }

        [Inject]
        protected MessageService MessageService { get; set; }

        [Inject]
        protected IPaasPortalClient _paasPortalClient { get; set; }


        private async Task LoadDataAsync()
        {
            Loading = true;

            var users = await _paasPortalClient.GetUsersAsync(_pageRequest);
            _userPageData = users.Data;

            Loading = false;
        }

        protected override async Task OnInitializedAsync()
        {
            LoadRoles();
            await LoadDataAsync();
        }

        private void LoadRoles()
        {
            _rolesOptions = new List<string> {
                "管理员",
                "项目经理",
                "开发",
                "测试",
                "运维",
            };
        }

        #region 新建编辑账号

        /// <summary>
        /// 添加角色
        /// </summary>
        private void ShowAddUser()
        {
            _userModel = new UserModel();
            _userDrawerComponentOption.Title = "添加账号";
            _userDrawerComponentOption.Open();
        }

        /// <summary>
        /// 编辑角色
        /// </summary>
        /// <param name="userModel"></param>
        private void ShowEditUser(UserModel userModel)
        {
            _userModel = userModel;
            _userDrawerComponentOption.Title = "编辑账号";
            _userDrawerComponentOption.Open();
        }


        /// <summary>
        /// 保存操作
        /// </summary>
        /// <param name="editContext"></param>
        /// <returns></returns>
        private async Task SaveUser(EditContext editContext)
        {
            try
            {
                BooleanResponseResult result;
                if (_userModel.Id > 0)
                {
                    result = await _paasPortalClient.EditUserAsync(new EditUserRequest
                    {
                        Id = _userModel.Id,
                        Name = _userModel.Name,
                        Email = _userModel.Email,
                        Role = _userModel.Role,
                        Enable = _userModel.Enable
                    });
                }
                else
                {
                    result = await _paasPortalClient.AddUserAsync(new AddUserRequest
                    {
                        Name = _userModel.Name,
                        Email = _userModel.Email,
                        Role = _userModel.Role,
                        Enable = _userModel.Enable
                    });
                }
                if (result.Code != 0)
                {
                    await MessageService.Error($"账号保存失败：{result.Msg}");
                    return;
                }
                _userDrawerComponentOption.Close();
                await MessageService.Success("账号保存成功");
                await LoadDataAsync();
            }
            catch (Exception ex)
            {
                await MessageService.Error(ex.Message);
            }
        }

        #endregion 新建编辑账号
    }
}
