using SmartLab.Core.Data.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLabServiceWindows.Models
{

        public class DataEntityBase : DataEntity
        {
            public int Type { get; set; }
            public DataEntity Entity { get; set; }
            public override string ToString()
            {
                return Name;
            }
        }

        public class TreeListNodeInfo : DataEntityBase
        {
            /// <summary>
            /// 文件夹ID
            /// </summary>
            public string FolderID { get; set; }

            /// <summary>
            /// 父文件夹ID
            /// </summary>
            public string ParentFolderID { get; set; }

            /// <summary>
            /// 实体ID
            /// </summary>
            public string EntityID { get; set; }
            //public string Name { get; set; }

            /// <summary>
            /// 实体类型 -1文件夹 0数据
            /// </summary>
            public int EntityType { get; set; }
            //public object Entity { get; set; }

            /// <summary>
            /// 节点类型 0 单次进样，1 序列进样，2 文件夹，3 仪器方法匹配，
            /// 4 仪器方法不匹配，5 序列方法匹配，6 序列方法不匹配，
            /// 7 处理方法，8 未知样, 9 标准样
            /// </summary>
            public int DataType { get; set; }
        }

    /// <summary>
    /// 项目详细信息数据源
    /// </summary>
    public class ProjectDetailDataSource
    {
        /// <summary>
        /// 当前的项目
        /// </summary>
        public ProjectInfo ProjectInfo { get; set; }

        /// <summary>
        /// 类型列表
        /// </summary>
        public Dictionary<Type, object> TypeList = new Dictionary<Type, object>();

        public ProjectDetailDataSource()
        {
            TypeList.Add(typeof(RunInjectSeq), _RunInjectSeqs);
            TypeList.Add(typeof(InstrumentMethod), _InstrumentMethods);
            TypeList.Add(typeof(ResultData), _ResultDatas);
            TypeList.Add(typeof(ProcessMethod), _ProcessMethods);
            TypeList.Add(typeof(ReportMethod), _ReportMethods);
            TypeList.Add(typeof(SeqMethod), _SeqMethods);
            TypeList.Add(typeof(ChromSysInfo), _ChromSysInfos);
        }

        /// <summary>
        /// 进样列表
        /// </summary>
        private List<BaseEntity> _RunInjectSeqs = new List<BaseEntity>();

        /// <summary>
        /// 进样列表
        /// </summary>
        public List<BaseEntity> RunInjectSeqs
        {
            get
            {
                return _RunInjectSeqs;
            }
            set
            {
                if (value == null)
                    value = new List<BaseEntity>();
                //if (value != null)
                //{
                _RunInjectSeqs = value;
                TypeList[typeof(RunInjectSeq)] = value;
                //}
            }
        }

        /// <summary>
        /// 结果数据列表
        /// </summary>
        private List<BaseEntity> _ResultDatas = new List<BaseEntity>();

        /// <summary>
        /// 结果数据列表
        /// </summary>
        public List<BaseEntity> ResultDatas
        {
            get
            {
                return _ResultDatas;
            }
            set
            {
                if (value == null)
                    value = new List<BaseEntity>();
                //if (value != null)
                //{
                _ResultDatas = value;
                TypeList[typeof(ResultData)] = value;
                //}
            }
        }

        /// <summary>
        /// 仪器方法列表
        /// </summary>
        public List<MethodData> InstrumentMethods
        {
            get { return _InstrumentMethods; }
            set
            {
                if (value == null)
                    value = new List<MethodData>();
                //if (value != null)
                //{
                _InstrumentMethods = value;
                TypeList[typeof(InstrumentMethod)] = value;
                //}
            }
        }

        /// <summary>
        /// 仪器方法列表
        /// </summary>
        private List<MethodData> _InstrumentMethods = new List<MethodData>();

        /// <summary>
        /// 处理方法列表
        /// </summary>
        private List<MethodData> _ProcessMethods = new List<MethodData>();

        /// <summary>
        /// 处理方法列表
        /// </summary>
        public List<MethodData> ProcessMethods
        {
            get { return _ProcessMethods; }
            set
            {
                if (value == null)
                    value = new List<MethodData>();
                //if (value != null)
                //{
                _ProcessMethods = value;
                TypeList[typeof(ProcessMethod)] = value;
                //}
            }
        }

        /// <summary>
        /// 报告方法列表
        /// </summary>
        private List<MethodData> _ReportMethods = new List<MethodData>();

        /// <summary>
        /// 报告方法列表
        /// </summary>
        public List<MethodData> ReportMethods
        {
            get
            {
                return _ReportMethods;
            }
            set
            {
                if (value == null)
                    value = new List<MethodData>();
                //if (value != null)
                //{
                _ReportMethods = value;
                TypeList[typeof(ReportMethod)] = value;
                //}
            }
        }

        /// <summary>
        /// 序列方法列表
        /// </summary>
        private List<MethodData> _SeqMethods = new List<MethodData>();

        /// <summary>
        /// 序列方法列表
        /// </summary>
        public List<MethodData> SeqMethods
        {
            get
            {
                return _SeqMethods;
            }
            set
            {
                if (value == null)
                    value = new List<MethodData>();
                //if (value != null)
                //{
                _SeqMethods = value;
                TypeList[typeof(SeqMethod)] = value;
                //}
            }
        }

        /// <summary>
        /// 关联的色谱系统列表
        /// </summary>
        private List<ChromSysInfoEx> _ChromSysInfos = new List<ChromSysInfoEx>();

        /// <summary>
        /// 关联的色谱系统列表
        /// </summary>
        public List<ChromSysInfoEx> ChromSysInfos
        {
            get
            {
                return _ChromSysInfos;
            }
            set
            {
                if (value == null)
                    value = new List<ChromSysInfoEx>();
                //if (value != null)
                //{
                _ChromSysInfos = value;
                TypeList[typeof(ChromSysInfo)] = value;
                //}
            }
        }
    }

    /// <summary>
    /// 方法数据
    /// </summary>
    [Serializable]
    public class MethodData : DataEntityBase
    {
        /// <summary>
        /// 方法状态，默认可用
        /// </summary>
        public int _status = 1;
        /// <summary>
        /// 方法状态：0不可用 1可用
        /// </summary>
        public int Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
            }
        }
    }

    /// <summary>
    /// 色谱系统扩展
    /// </summary>
    public class ChromSysInfoEx : DataEntityBase
    {
        /// <summary>
        /// 色谱系统状态：-1离线 0未知 1在线
        /// </summary>
        public int Status { get; set; }
    }
}
