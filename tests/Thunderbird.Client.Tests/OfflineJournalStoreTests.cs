using Thunderbird.Client.Offline;

namespace Thunderbird.Client.Tests;

public class OfflineJournalStoreTests
{
  [Fact]
  public void Append_ShouldStoreEntry()
  {
    var store = new OfflineJournalStore();

    store.Append(new OfflineJournalEntry(
      ActionId: "a1",
      ActionType: "draw_card",
      PayloadJson: "{}",
      RulesetVersion: "v1",
      ClientTimestamp: DateTimeOffset.UtcNow,
      IdempotencyKey: "k1",
      DeltaClass: "unranked_progress"
    ));

    Assert.Single(store.Entries);
  }
}
