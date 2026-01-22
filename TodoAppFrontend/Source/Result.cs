using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TodoAppFrontend.Source
{
    public enum ResultType
    {
        Success = 0,    // duh
        
        InputError,     // when the input that gets the result is wrong
        HttpFailure,    // when the http client loses connection, might want to nav back to main
        JsonFailure,    // when json doesnt deserialise

        UnknownFailure, // any other errors, ie: when IsSuccessStatusCode == false
    }

    public class Result
    {
        public ResultType ResultType { get; set; }
        public string ErrorMessage { get; set; }

        public bool IsSuccessful => this.ResultType == ResultType.Success;

        protected Result(ResultType resultType, string errorMessage)
        {
            this.ResultType = resultType;
            this.ErrorMessage = errorMessage;
        }

        public static Result Success() => new Result(ResultType.Success, string.Empty);
        public static Result Failure(string errorMessage) => new Result(ResultType.UnknownFailure, errorMessage);
        public static Result Failure(ResultType resultType, string errorMessage) => new Result(resultType, errorMessage);

        public static implicit operator bool(Result result) => result.IsSuccessful;
    }

    public class Result<T> : Result
    {
        public T Data { get; set; }

        protected Result(T data) : base(ResultType.Success, string.Empty)
        {
            this.Data = data;
        }

        protected Result(ResultType resultType, string errorMessage) : base(resultType, errorMessage)
        {

        }

        public static Result<T> Success(T data) => new Result<T>(ResultType.Success, string.Empty) { Data = data };
        public static new Result<T> Failure(string errorMessage) => new Result<T>(ResultType.UnknownFailure, errorMessage);
        public static new Result<T> Failure(ResultType resultType, string errorMessage) => new Result<T>(resultType, errorMessage);
    }
}