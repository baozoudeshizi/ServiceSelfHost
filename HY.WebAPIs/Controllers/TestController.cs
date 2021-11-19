using HY.Services;
using System.Collections.Generic;
using System.Web.Http;

namespace WebAPIs.Controllers
{
    /// <summary>
    /// 测试用
    /// 尝试某些接口是否可以使用
    /// </summary>
    public class TestController : ApiController  //必须要继承APIController，否则不会再Swagger中显示
    {
        private readonly ITestService _deviceServ;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="divice"></param>
        public TestController(ITestService divice)
        {
            _deviceServ = divice;
        }


        /// <summary>
        /// test
        /// </summary>
        public string TestDI()
        {
           var aa = _deviceServ.test();
            return aa;
        }

    }
}
