using AntDesign;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Xiaobao.PaaS.Portal.Client.Pages
{
    public partial class HeaderContent : AntDomComponentBase
    {
        public const string UnLogined = "未登录";

        CurrentUser _currentUser = new CurrentUser { Name = UnLogined };

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        public void HandleSelectUser(MenuItem item)
        {
            switch (item.Key)
            {
                case "center":
                    NavigationManager.NavigateTo("/");
                    break;
                case "setting":
                    NavigationManager.NavigateTo("/users");
                    break;
                case "logout":
                    NavigationManager.NavigateTo("/logout", true);
                    break;
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            _currentUser.Name = "张三";
        }
    }

    public class CurrentUser
    {
        public string Name { get; set; }
    }
}
