namespace WebAPIs
{
    /// <summary>
    /// 存储用户信息
    /// </summary>
    public class User
    {
        /// <summary>
        /// 用户登录名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 用户登录密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 用户的连接Id
        /// </summary>
        public string ConnectionId { get; set; }
    }
}