using Grpc.Net.Client;
using Thunderbird.Contracts.V1;
using Thunderbird.Client.Offline;

namespace Thunderbird.Client.Networking;

public sealed class OfflineSyncGateway
{
  private readonly OfflineSyncService.OfflineSyncServiceClient _client;

  public OfflineSyncGateway(string endpoint)
  {
    var channel = GrpcChannel.ForAddress(endpoint);
    _client = new OfflineSyncService.OfflineSyncServiceClient(channel);
  }

  public async Task<UploadOfflineDeltaResponse> UploadDeltaAsync(
    string userId,
    string clientBuild,
    string rulesetVersion,
    IEnumerable<Thunderbird.Client.Offline.OfflineJournalEntry> entries,
    CancellationToken cancellationToken)
  {
    var request = new UploadOfflineDeltaRequest
    {
      UserId = userId,
      ClientBuild = clientBuild,
      RulesetVersion = rulesetVersion,
      IdempotencyKey = Guid.NewGuid().ToString("N")
    };

    foreach (var entry in entries)
    {
      request.Entries.Add(new Thunderbird.Contracts.V1.OfflineJournalEntry
      {
        Action = new LocalActionEnvelope
        {
          ActionId = entry.ActionId,
          ActionType = entry.ActionType,
          PayloadJson = entry.PayloadJson,
          RulesetVersion = entry.RulesetVersion,
          ClientTimestampUnix = entry.ClientTimestamp.ToUnixTimeSeconds(),
          ClientBuild = clientBuild,
          IdempotencyKey = entry.IdempotencyKey
        },
        DeltaClass = entry.DeltaClass switch
        {
          "cosmetic" => DeltaClass.Cosmetic,
          "unranked_progress" => DeltaClass.UnrankedProgress,
          "non_economy_unlock" => DeltaClass.NonEconomyUnlock,
          "ranked_mmr" => DeltaClass.RankedMmr,
          "competitive_outcome" => DeltaClass.CompetitiveOutcome,
          "premium_currency" => DeltaClass.PremiumCurrency,
          _ => DeltaClass.Unspecified
        }
      });
    }

    return await _client.UploadOfflineDeltaAsync(request, cancellationToken: cancellationToken);
  }
}
