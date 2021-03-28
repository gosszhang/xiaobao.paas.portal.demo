namespace Xiaobao.PaaS.Portal.Shard
{
    public class ResponseResult<T>
    {
        public int Code { get; set; }
        public string Msg { get; set; }
        public T Data { get; set; }
    }

    public class ResponseResult
    {
        public int Code { get; set; }
        public string Msg { get; set; }
    }
}
