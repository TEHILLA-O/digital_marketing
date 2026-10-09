# ADR 0005: Redis

## Status

Accepted

## Context

Payments need a fast idempotency replay cache and a short distributed lock so double-clicks do not all hit PostgreSQL at once.

## Decision

Redis stores idempotent JSON responses for 24 hours and source-account locks for 15 seconds. The unique SQL constraint remains the source of truth. Projection worker also caches recent balances.

## Consequences

Redis is doing real work, not decorating a README. The system still rejects duplicates if Redis is flushed.
