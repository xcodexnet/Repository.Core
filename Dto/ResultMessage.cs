namespace CBLibrary.Repository.Dto
{
    /// <summary>
    /// 操作结果
    /// </summary>
    public class ResultMessage
    {
        /// <summary>
        /// 状态码: 默认情况下,大于等于0时表示业务成功,否则为业务失败
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 状态码对应描述
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 全参构造函数
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        public ResultMessage(int code = 0, string message = "success")
        {
            this.Code = code;
            this.Message = message;
        }

        /// <summary>
        /// 状态为0的业务成功消息
        /// </summary>
        public ResultMessage()
        {
            this.Code = 0;
            this.Message = "success";
        }
    }

    /// <summary>
    /// Api请求结果的泛型扩展
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResultMessage<T> : ResultMessage
    {
        /// <summary>
        /// 请求的数据
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ResultMessage()
        {

        }

        /// <summary>
        /// 全参构造函数
        /// </summary>
        /// <param name="data"></param>
        /// <param name="code"></param>
        /// <param name="message"></param>
        public ResultMessage(T data, int code = 0, string message = "success") : base(code, message)
        {
            this.Data = data;
        }
    }
}