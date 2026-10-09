# ADR 0006: Blazor and service boundaries

## Status

Accepted

## Context

A single Web API would hide the hard parts: HTTP between Payments and Ledger, separate schemas, and independent deployability.

## Decision

Four ASP.NET APIs (Identity, Accounts, Payments, Ledger), three workers, and a Blazor Server UI. Blazor Server keeps auth and HTTP clients on the server so JWT never has to live in browser JavaScript for the demo.

## Consequences

Docker Compose / Aspire start more processes. That is intentional: it is what the architecture claims to be.
