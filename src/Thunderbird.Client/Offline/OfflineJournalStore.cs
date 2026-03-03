using System.Text.Json;

namespace Thunderbird.Client.Offline;

public sealed class OfflineJournalStore
{
  private readonly List<OfflineJournalEntry> _entries = [];

  public IReadOnlyList<OfflineJournalEntry> Entries => _entries;

  public void Append(OfflineJournalEntry entry)
  {
    _entries.Add(entry);
  }

  public string ExportAsJson()
  {
    return JsonSerializer.Serialize(_entries);
  }
}
