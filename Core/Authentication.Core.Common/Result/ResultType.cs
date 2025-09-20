namespace Authentication.Common.Result;

public enum ResultType : short
{
    InternalError,
    Ok,
    NotFound,
    Forbidden,
    Conflicted,
    Invalid,
    Unauthorized
}
