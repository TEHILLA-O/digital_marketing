# ADR 0002: PostgreSQL

## Status

Accepted

## Context

The ledger needs check constraints, unique indexes, `FOR UPDATE`, and transactional outbox rows in the same commit as business changes.

## Decision

PostgreSQL 16 via Npgsql + EF Core. One server, one database per bounded context (`ledgerx_identity`, `ledgerx_accounts`, `ledgerx_payments`, `ledgerx_ledger`, `ledgerx_audit`).

## Consequences

Services stay isolated without running five database engines. Testcontainers can reproduce the same engine in CI.
