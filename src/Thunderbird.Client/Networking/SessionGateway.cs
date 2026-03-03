using Grpc.Net.Client;
using Thunderbird.Contracts.V1;

namespace Thunderbird.Client.Networking;

public sealed class SessionGateway
{
  private readonly SessionService.SessionServiceClient _client;

  public SessionGateway(string endpoint)
  {
    var channel = GrpcChannel.ForAddress(endpoint);
    _client = new SessionService.SessionServiceClient(channel);
  }

  public async Task<CreateSessionResponse> CreateSessionAsync(string hostUserId, string title, string rulesetVersion, CancellationToken cancellationToken)
  {
    var request = new CreateSessionRequest
    {
      HostUserId = hostUserId,
      Title = title,
      RulesetVersion = rulesetVersion,
      IsRanked = false,
      IdempotencyKey = Guid.NewGuid().ToString("N")
    };

    return await _client.CreateSessionAsync(request, cancellationToken: cancellationToken);
  }
}
