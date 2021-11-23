namespace HY.Models.Model
{
    public static class GlobalConfig 
    {
       public static MonitorConfig Monitor { get; set; }

    }


    /// <summary>
    /// 监视器类
    /// </summary>
    public class MonitorConfig
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
