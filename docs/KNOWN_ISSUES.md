# Known Issues

LedgerX is an educational / portfolio system. It is **not** a regulated bank and is not connected to real payment rails.

## Open

- EF migrations are not yet checked in. Development uses `MigrateOrCreateAsync` (migrate when history exists, otherwise `EnsureCreated`). Generate and commit migrations before treating the schema as production-shaped.
- Inter-service HTTP endpoints (`/api/internal/*`) are anonymous for the local demo. They must be locked down (mTLS or a service key) before any shared environment.
- Kafka advertised listener in Docker is `kafka:9092`, which is correct for containers but not for host-process publishers talking to the mapped port. Aspire / compose-from-host may need a second listener.
- Blazor session is in-memory (circuit scoped). Refreshing the browser requires signing in again.
- Screenshot files are not in the repository yet.
- `ledgerx_kafka_consumer_lag` is not exported as a Prometheus gauge.
- Aspire AppHost may warn `ASPIRE010` unless the Aspire CLI bundle is enabled.

## Resolved during build

- .NET 10 `dotnet new sln` emits `LedgerX.slnx`; a classic `LedgerX.sln` exists but is empty; use `LedgerX.slnx`.
- `Money.Currency` collided with the `Currency` type; factories use an `IsoCurrency` alias.
- OpenAPI 2.x no longer exposes `Microsoft.OpenApi.Models` the same way; document security scheme registration was simplified.
