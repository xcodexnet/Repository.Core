namespace CBLibrary.Repository.Application
{
    /// <summary>
    /// 应用层常量
    /// </summary>
    public class AppConstants
    {
        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public const string DEFAULTPASSPHRASE = "gsKxGZ012HLL3MI5";


        #region SystemSetting_User
        /// <summary>
        /// 启用登录
        /// </summary>
        public const string SS_ENABLEDLOGIN = "EnabledLogin";

        /// <summary>
        /// 启用注册
        /// </summary>
        public const string SS_ENABLEDREGISTER = "EnabledRegister";

        /// <summary>
        /// 启用手机验证码登录
        /// </summary>
        public const string SS_ENABLEDLOGINBYPHONECAPTCHA = "EnabledLoginByPhoneCaptcha";

        /// <summary>
        /// 找回密码方式
        /// </summary>
        public const string SS_RETRIEVEPASSWORDMODE = "RetrievePasswordMode";

        /// <summary>
        /// 邮箱验证检查
        /// </summary>
        public const string SS_CHECKEDEMAILCONFIRMED = "CheckedEmailConfirmed";

        /// <summary>
        /// 手机号验证检查
        /// </summary>
        public const string SS_CHECKEDPHONENUMBERCONFIRMED = "CheckedPhoneNumberConfirmed";
        #endregion

        #region Code

        /// <summary>
        /// 成功
        /// </summary>
        public const int C_SUCCESS = 200;

        /// <summary>
        /// 失败
        /// </summary>
        public const int C_FAILURE = -200;

        /// <summary>
        /// 错误请求:数据验证失败
        /// </summary>
        public const int C_BADREQUEST = -400;

        /// <summary>
        /// 未授权:未登录
        /// </summary>
        public const int C_UNAUTHORIZED = -401;

        /// <summary>
        /// 禁止:权限不足
        /// </summary>
        public const int C_FORBIDDEN = -403;

        /// <summary>
        /// 未找到
        /// </summary>
        public const int C_NOTFOUND = -404;

        /// <summary>
        /// 服务器异常 
        /// </summary>
        public const int C_INTERNALSERVERERROR = -500;
        #endregion

        #region Message

        /// <summary>
        /// 成功
        /// </summary>
        public const string M_SUCCESS = "成功";

        /// <summary>
        /// 失败
        /// </summary>
        public const string M_FAILURE = "失败";

        /// <summary>
        /// 验证码错误
        /// </summary>
        public const string M_INVALIDCAPTCHA = "验证码错误";

        /// <summary>
        /// 数据验证失败
        /// </summary>
        public const string M_BADREQUEST = "数据验证失败";

        /// <summary>
        /// 未授权:未登录
        /// </summary>
        public const string M_UNAUTHORIZED = "未授权";

        /// <summary>
        /// 禁止:权限不足
        /// </summary>
        public const string M_FORBIDDEN = "权限不足";

        /// <summary>
        /// 已存在
        /// </summary>
        public const string M_EXIST = "已存在";

        /// <summary>
        /// 不存在
        /// </summary>
        public const string M_NOTEXIST = "不存在";

        /// <summary>
        /// 请求数据成功
        /// </summary>
        public const string M_GET_SUCCESS = "请求数据成功";

        /// <summary>
        /// 请求数据失败
        /// </summary>
        public const string M_GET_FAILURE = "请求数据失败";

        /// <summary>
        /// 添加成功
        /// </summary>
        public const string M_ADD_SUCCESS = "添加成功";

        /// <summary>
        /// 添加失败
        /// </summary>
        public const string M_ADD_FAILURE = "添加失败";

        /// <summary>
        /// 更新成功
        /// </summary>
        public const string M_UPDATE_SUCCESS = "更新成功";

        /// <summary>
        /// 更新失败
        /// </summary>
        public const string M_UPDATE_FAILURE = "更新失败";

        /// <summary>
        /// 删除成功
        /// </summary>
        public const string M_DELETE_SUCCESS = "删除成功";

        /// <summary>
        /// 删除失败
        /// </summary>
        public const string M_DELETE_FAILURE = "删除失败";

        #endregion
    }
}
