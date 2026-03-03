# Offline Journal and Selective Merge

The PC client records deterministic action envelopes locally, then uploads delta journals via gRPC.
Server validates and merges only allowed classes:
- cosmetic
- unranked_progress
- non_economy_unlock

Rejected classes include ranked/economy changes.
