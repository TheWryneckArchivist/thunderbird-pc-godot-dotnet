namespace Thunderbird.Client.Offline;

public sealed record OfflineJournalEntry(
  string ActionId,
  string ActionType,
  string PayloadJson,
  string RulesetVersion,
  DateTimeOffset ClientTimestamp,
  string IdempotencyKey,
  string DeltaClass
);
