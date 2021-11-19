namespace HY.Models.Model
{
    /// <summary>
    /// 配置返回类的基本字段
    /// </summary>
    public class TDataRetBase
    {
        /// <summary>
        /// 成功
        /// </summary>
        public static int SUCCESS = 200;

        /// <summary>
        /// 失败
        /// </summary>
        public static int FAILURE = 400;

        /// <summary>
        /// 过期
        /// </summary>
        public static int TOKEN_EXPIRE = 50014;

    }

    /// <summary>
    /// 返回类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TDataRet<T> : TDataRetBase
    {
        /// <summary>
        /// 成功，并返回Data
        /// </summary>
        /// <param name="data"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static TData<T> Success(T data, string msg = "操作成功")
        {
            return new TData<T>(SUCCESS, msg, data);
        }

        /// <summary>
        /// 失败
        /// </summary>
        /// <returns></returns>
        public static TData<T> Failure()
        {
            return new TData<T>(FAILURE, "操作失败");
        }

        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static TData<T> Failure(string msg)
        {
            return new TData<T>(FAILURE, msg);
        }

        /// <summary>
        /// 失败，并返回Data
        /// </summary>
        /// <param name="data"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static TData<T> Failure(T data, string msg = "操作成功")
        {
            return new TData<T>(FAILURE, msg, data);
        }

        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static TData<T> Success(string msg = "操作成功")
        {
            return new TData<T>(SUCCESS, msg);
        }

        //public static TData<T> Expire()
        //{
        //    return new TData<T>(TOKEN_EXPIRE, "token 过期");
        //}
    }

    /// <summary>
    /// 返回类，不带泛型
    /// </summary>
    public class TDataRet : TDataRetBase
    {
        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static TData Success(string msg = "操作成功")
        {
            return new TData(SUCCESS, msg);
        }


        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static TData Failure(string msg = "操作失败")
        {
            return new TData(FAILURE, msg);
        }

        /// <summary>
        /// 失败
        /// </summary>
        /// <returns></returns>
        public static TData Failure()
        {
            return new TData(FAILURE, "操作失败");
        }

        //public static TData Expire()
        //{
        //    return new TData(TOKEN_EXPIRE, "token 过期");
        //}
    }
}
