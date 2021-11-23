namespace HY.Common.GlobalContext
{
    public class MonitorConfigTest
    {
        /// <summary>
        /// 监视的路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 是否需要检查子文件夹
        /// </summary>
        public bool IsIncludeSub { get; set; }
    }
}
