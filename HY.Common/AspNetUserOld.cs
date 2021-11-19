using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HY.Common
{
    public class AspNetUserOld
    {
        private readonly IHttpContextAccessor _accessor;
        //private readonly ILogger<AspNetUser> _logger;

        public AspNetUserOld(IHttpContextAccessor accessor/*, ILogger<AspNetUser> logger*/)
        {
            _accessor = accessor;
            //_logger = logger;
        }

        public string ID => GetClaimValueByType("jti").FirstOrDefault();
        public string Name => GetName();

        private string GetName()
        {
            //if (IsAuthenticated() && _accessor.HttpContext.User.Identity.Name)
            //{
            //    return _accessor.HttpContext.User.Identity.Name;
            //}
            //else
            //{
            //    if (!string.IsNullOrEmpty(GetToken()))
            //    {
            //        var getNameType = Permissions.IsUseIds4 ? "name" : "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
            //        return GetUserInfoFromToken(getNameType).FirstOrDefault().ObjToString();
            //    }
            //}

            return "";
        }



        public bool IsAuthenticated()
        {
            return true;
            //return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }


        //public string GetToken()
        //{
        //    return _accessor.HttpContext?.Request?.Headers["Authorization"].ObjToString().Replace("Bearer ", "");
        //}

        //public List<string> GetUserInfoFromToken(string ClaimType)
        //{
        //    var jwtHandler = new JwtSecurityTokenHandler();
        //    var token = "";

        //    token = GetToken();
        //    // token校验
        //    if (token.IsNotEmptyOrNull() && jwtHandler.CanReadToken(token))
        //    {
        //        JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(token);

        //        return (from item in jwtToken.Claims
        //                where item.Type == ClaimType
        //                select item.Value).ToList();
        //    }

        //    return new List<string>() { };
        //}



        public List<string> GetClaimValueByType(string ClaimType)
        {
            return (from item in GetClaimsIdentity()
                    where item.Type == ClaimType
                    select item.Value).ToList();

        }

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            var claim = _accessor.HttpContext.User.Claims;
            return claim;
        }
    }
}
