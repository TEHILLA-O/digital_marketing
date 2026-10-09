# ADR 0003: Kafka and the transactional outbox

## Status

Accepted

## Context

Publishing to Kafka inside a business transaction is not atomic. A crash after Kafka success and before commit, or the reverse, creates ghosts or lost events.

## Decision

Each write-side DbContext has `outbox_messages`. The API transaction writes the business change and the outbox row together. A hosted `OutboxPublisherService<TContext>` publishes committed rows with Confluent.Kafka. Consumers use `inbox_messages` keyed by `(EventId, ConsumerName)`.

## Consequences

At-least-once delivery with idempotent handlers. Kafka can be disabled in tests (`Kafka:Enabled=false`) without changing domain code.
