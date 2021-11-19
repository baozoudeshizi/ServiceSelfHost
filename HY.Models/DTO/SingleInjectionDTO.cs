namespace HY.Modele.DTO
{
    /// <summary>
    /// 单次进样类
    /// </summary>
    public class SingleInjectionDTO
    {
        /// <summary>
        /// 项目ID
        /// </summary>
        public string ProjectID { get; set; }
        /// <summary>
        /// 色谱系统ID
        /// </summary>
        public string ChromSysID { get; set; }
        /// <summary>
        /// 样品名称
        /// </summary>
        public string SampleName { get; set; }
        /// <summary>
        /// 样品类型：0-未知样，1-标准样
        /// </summary>
        public int SampleType { get; set; }

        /// <summary>
        /// 运行方法：0-空白，1-进样，2-平衡
        /// </summary>
        public int InjectFunction { get; set; }
        /// <summary>
        /// 仪器方法
        /// </summary>
        public string InstrumentMethodID {get;set;}

        /// <summary>
        /// 样品位置
        /// </summary>
        public string SampleLocation { get; set; }

        /// <summary>
        /// 进样体积，单位为uL，默认为10
        /// </summary>
        public float InjectionVolume { get; set; }

    }


    /// <summary>
    /// 序列进样类
    /// </summary>
    public class GroupInjectionDTO
    {
        /// <summary>
        /// 项目ID
        /// </summary>
        public string ProjectID { get; set; }
        /// <summary>
        /// 色谱系统ID
        /// </summary>
        public string ChromSysID { get; set; }

        /// <summary>
        /// 序列方法ID
        /// </summary>
        public string SeqMethodID { get; set; }

    }
}
