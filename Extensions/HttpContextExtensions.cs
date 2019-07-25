using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;

namespace CBLibrary.Repository.Extensions
{
    /// <summary>
    /// HttpContext扩展方法
    /// </summary>
    public static class HttpContextExtensions
    {
        /// <summary>
        /// 获取用户Id
        /// </summary>
        /// <param name="accessor"></param>
        /// <returns></returns>
        public static string GetUserId(this IHttpContextAccessor accessor)
        {
            var id = accessor?.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid);
            //return null == id ? Guid.Empty : Guid.Parse(id.Value);

            return id?.Value;
        }

        /// <summary>
        /// 获取用户名
        /// </summary>
        /// <param name="accessor"></param>
        /// <returns></returns>
        public static string GetUserName(this IHttpContextAccessor accessor)
        {
            var name = accessor?.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
            return name?.Value;
        }

        /// <summary>
        /// 获取用户角色
        /// </summary>
        /// <param name="accessor"></param>
        /// <returns></returns>
        public static string GetUserRoles(this IHttpContextAccessor accessor)
        {
            var roles = accessor?.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            return roles?.Value;
        }


        /// <summary>
        /// 获取用户IP
        /// </summary>
        /// <param name="accessor"></param>
        /// <returns></returns>
        public static string GetUserIp(this IHttpContextAccessor accessor)
        {
            var ipAddress = accessor?.HttpContext?.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (true == ipAddress.IsNullOrWhiteSpace())
            {
                ipAddress = accessor?.HttpContext?.Connection.RemoteIpAddress.ToString();
            }

            return ipAddress;
        }

    }
}
