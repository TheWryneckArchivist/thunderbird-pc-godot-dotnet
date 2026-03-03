namespace Thunderbird.Client.Simulation;

public sealed record DeterministicActionEnvelope(
  string ActionId,
  string ActionType,
  string PayloadJson,
  string RulesetVersion,
  long ClientTimestampUnix,
  string ClientBuild,
  string IdempotencyKey
);
