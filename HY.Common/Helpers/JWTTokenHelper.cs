using HY.Models.Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace HY.Common.Helpers
{
    /// <summary>
    /// 用户Token管理
    /// </summary>
    public static class JWTTokenHelper
    {
        /// <summary>
        /// 获取基于JWT的Token
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        public static TokenInfo BuildJwtToken(UserInfoDTO user)
        {
            //注意，此处生命的Id不要改变，因为在解析Token时是获取第一个Claim作为用户Id
            var claims = new List<Claim> {
                    new Claim(JwtRegisteredClaimNames.Jti, user.ID.ToString()),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Expiration, DateTime.Now.AddSeconds(3600*24).ToString()) };
            // 实例化JwtSecurityToken
            var jwt = new JwtSecurityToken(
                issuer: "huyun",
                audience: "zyp",
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.Add(TimeSpan.FromSeconds(24 * 60 * 60))
            );
            // 生成 Token
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            //打包返回前台
            var responseJson = new TokenInfo
            {
                Success = true,
                Token = encodedJwt,
                Expires_in = 24 * 60 * 60,
                Token_type = "Bearer"
            };
            return responseJson;
        }

        /// <summary>
        /// 根据Tokne获取用户信息
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static CurrentOperator DecodeJTWToken(string token)
        {
            //解析Token
            var jwtHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(token);

            //用Linq表达式不知道为啥不行
            //List<Claim> cliams = jwtToken.Claims.ToList();
            //var userID = cliams.Where(s => s.Type == "jti").Select(s=>s.Value);
            string ID = (from item in jwtToken.Claims
                        where item.Type == "jti"
                        select item.Value).FirstOrDefault().ToString();

            string Name = (from item in jwtToken.Claims
                           where item.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"
                           select item.Value).FirstOrDefault().ToString();

            CurrentOperator currentOperator = new CurrentOperator{ ID = ID, Name = Name };
            return currentOperator;
            //var user = (from item in jwtToken.Claims
            //            select item.Value).ToList();
            //return aa.FirstOrDefault();
        }
    }



}
