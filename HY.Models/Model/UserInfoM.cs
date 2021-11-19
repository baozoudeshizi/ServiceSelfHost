
namespace HY.Models.Model
{
    public class UserInfoDTO
    {
        public object ID;

        public string PassWord { get; set; }
        public int UserType { get; set; }
        public string Email { get; set; }
        public string TelPhone { get; set; }

        public TokenInfo TokenInfo { get; set; }
        public string Name { get; set; }
    }

    /// <summary>
    /// token返回类
    /// </summary>
    public class TokenInfo
    {
        /// <summary>
        /// 是否成功，true/false
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// Token值
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public double Expires_in { get; set; }
        /// <summary>
        /// Token类型
        /// </summary>
        public string Token_type { get; set; }
    }


    /// <summary>
    /// 当前操作用户
    /// </summary>
    public class CurrentOperator 
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }

}
