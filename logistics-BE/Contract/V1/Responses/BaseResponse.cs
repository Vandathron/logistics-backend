using System;
namespace logistics_BE.Contract.V1.Responses
{
    public class BaseResponse<T>
    {
        public string ErrorMessage { get; set; }

        public T data { get; set; }

        public bool status { get; set; }

    }
}
