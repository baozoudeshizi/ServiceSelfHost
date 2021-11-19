using AutoMapper;
using HY.Models.Model;

namespace WebAPIs
{
    class AutoMapProfile: Profile
    {
        /// <summary>
        /// 配置映射文件
        /// </summary>
        public AutoMapProfile()
        {

            //CreateMap<UserInfo, UserInfoDTO>();
            //.ForMember(d => d.CreateDate, o => o.MapFrom(s => s.BaseCreateTime));

        }
    }
}
