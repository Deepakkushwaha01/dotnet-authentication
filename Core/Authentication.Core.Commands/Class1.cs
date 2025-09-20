using Authentication.Common.Mediatr.Commands.Abstractions;
using Authentication.Common.Result;

namespace Authentication.Core.Commands;

public class GetOAuthIntentCommand : ICommand<Result<string>>
{
    public GetOAuthIntentCommand()
    {
    }
}

public class GetOAuthIntentCommandHandler() : ICommandHandler<GetOAuthIntentCommand, Result<string>>
{
    public Task<Result<string>> ExecuteAsync(GetOAuthIntentCommand command, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(Result.Ok("Intent"));

    }
}
