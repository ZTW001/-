using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginDemo.ViewModel.Login
{
    public class LoginViewModel : NotificationObject
    {
        public LoginViewModel()
        {
            obj.UserName = "test";
        }

        /// <summary>
        /// Model对象
        /// </summary>
        private LoginModel obj = new LoginModel();

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get
            {
                return obj.UserName;
            }
            set
            {
                obj.UserName = value;
                this.RaisePropertyChanged("UserName");
            }
        }

        private BaseCommand loginClick;
        /// <summary>
        /// 登录事件
        /// </summary>
        public BaseCommand LoginClick
        {
            get
            {
                if (loginClick == null)
                {
                    loginClick = new BaseCommand(new Action<object>(o =>
                    {
                        //执行登录逻辑
                    }));
                }
                return loginClick;
            }
        }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            get
            {
                return obj.Password;
            }
            set
            {
                obj.Password = value;
                this.RaisePropertyChanged("Password");
            }
        }

        /// <summary>
        /// 性别
        /// </summary>
        public int Gender
        {
            get
            {
                return obj.Gender;
            }
            set
            {
                obj.Gender = value;
                this.RaisePropertyChanged("Gender");
            }
        }
    }
}
