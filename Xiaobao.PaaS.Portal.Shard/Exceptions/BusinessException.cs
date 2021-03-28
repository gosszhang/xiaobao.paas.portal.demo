using System;

namespace Xiaobao.PaaS.Portal.Shard.Exceptions
{
    public class BusinessException : Exception
    {
        public int Code { get; set; }
        public string Msg { get; set; }

        public BusinessException(string msg, int code = 0)
        {
            Msg = msg;
            Code = code;
        }
    }
}
