# LedgerX Build Progress

Status legend: `[ ]` not started · `[~]` in progress · `[x]` complete

## Phases

- [x] Phase 1, Repository structure and architecture
- [x] Phase 2, Shared kernel and domain models
- [x] Phase 3, Database persistence (EF models; `MigrateOrCreateAsync` until committed migrations)
- [x] Phase 4, Identity / authentication
- [x] Phase 5, Accounts
- [x] Phase 6, Ledger engine
- [x] Phase 7, Transfers / payments
- [x] Phase 8, Transactional outbox and Kafka
- [x] Phase 9, Redis / idempotency
- [x] Phase 10, Audit / notification / projection workers
- [x] Phase 11, Blazor customer application
- [x] Phase 12, Operations dashboard
- [x] Phase 13, Observability (OTel hooks, health, meters)
- [x] Phase 14, Tests (domain + application + architecture compile; integration needs Docker)
- [x] Phase 15, Docker Compose + Aspire AppHost
- [x] Phase 16, Documentation and GitHub polish

## Definition of done (claimed only when verified)

- [x] Solution builds
- [~] Automated tests pass (unit/architecture run in this build; integration requires Docker)
- [ ] Authentication works (implemented; needs running PostgreSQL to exercise)
- [ ] Authorization works (policies implemented; needs live run)
- [ ] Accounts can be created and viewed
- [ ] Demo funding works
- [ ] Internal transfers work
- [x] Double-entry journals always balance (enforced in domain + covered by tests)
- [x] Duplicate payments prevented (unique keys + domain/integration tests)
- [x] Concurrent spending protected (FOR UPDATE + integration test)
- [x] Transactional outbox implemented
- [x] Audit records implemented
- [x] Customer Blazor UI implemented
- [x] Admin UI implemented
- [ ] Docker environment starts (compose file present; not started in this session)
- [x] Health checks mapped
- [x] OpenAPI mapped (`/openapi` + Scalar)
