namespace Authentication.Common.Result
{
    public class Result<T> : ResultCommonLogic
    {
        public bool IsEmpty => Value == null || Value.Equals(Empty);

        public T Value { get; }
        private static T Empty => default(T)!;

        internal Result(ResultType resultType, string message)
            : base(resultType, isFailure: true, message)
        {
            Value = Empty;
        }

        internal Result(T value)
            : base(ResultType.Ok, isFailure: false, string.Empty)
        {
            Value = value;
        }

        public static implicit operator T(Result<T> result) => result.Value;

        public static implicit operator Result(Result<T> result)
        {
            return result.IsSuccess ? Result.Ok() : new Result(result.ResultType, result.Message);
        }
    }


    public class Result : ResultCommonLogic
    {
        internal Result(ResultType resultType, string message)
            : base(resultType, isFailure: true, message) { }

        internal Result() : base(ResultType.Ok, false, string.Empty) { }

        public static Result Ok() => new Result();
        public static Result Fail(ResultType type, string message) => new Result(type, message);
    }
}