# ADR 0004: Double-entry accounting

## Status

Accepted

## Context

`sender.Balance -= amount` cannot prove conservation of money and cannot support reversals, statements, or audit.

## Decision

Customer deposits are **liabilities**. House cash is an **asset**. Internal transfers debit the sender liability and credit the receiver liability. Deposits debit cash and credit the customer. Withdrawals reverse that. Journals that do not balance cannot post. Corrections are reversing journals.

## Consequences

Balances are derived (or safely projected) from immutable lines. Frozen accounts may still receive credits; they cannot send. See `docs/CONCURRENCY.md`.
