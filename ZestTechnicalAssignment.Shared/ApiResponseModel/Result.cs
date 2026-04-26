using System;
using System.Collections.Generic;
using System.Text;

namespace ZestTechnicalAssignment.Shared.ApiResponseModel
{
    public class Result<T>
    {
        public T? Value { get; set; }
        public string? ErrorMessage { get; set; }
        public bool? Success { get; set; }
        public int? StatusCode { get; set; }

        public Result(string? Message,bool Success,int status ) 
        {
            ErrorMessage = Message;
            this.Success = Success;
            StatusCode = status;
            
        }

        public Result(T? data, bool Success, int status)
        {
            Value = data;
            this.Success = Success;
            StatusCode = status;

        }

        public static Result<T> Successs(T Data) => new(Data,true,200);
        public static Result<T> Failure(string message) => new(message, false, 200);


    }
}
