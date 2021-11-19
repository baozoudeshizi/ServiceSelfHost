namespace HY.Models.Model
{
    /// <summary>
    /// 返回类
    /// </summary>
    public class TData
    {
        /// <summary>
        /// 返回码
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 约定值
        /// </summary>
        public string Msg { get; set; }
        
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public TData()
        {

        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        public TData(int code, string msg)
        {
            this.Code = code;
            this.Msg = msg;
        }
    }

    /// <summary>
    /// 返回类，带泛型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TData<T>
    {

        /// <summary>
        /// 返回码
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 约定值
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public TData()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        /// <param name="data"></param>
        public TData(int code, string msg,T data)
        {
            this.Code = code;
            this.Msg = msg;
            this.Data = data;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        public TData(int code, string msg)
        {
            this.Code = code;
            this.Msg = msg;
        }
    }
}
