using LedgerX.Accounts.Domain;
using LedgerX.Contracts.Demo;
using LedgerX.Eventing.Persistence;
using LedgerX.SharedKernel.Money;
using LedgerX.SharedKernel.Primitives;
using LedgerX.SharedKernel.Time;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LedgerX.Accounts.Infrastructure;

public sealed class AccountsSeeder(AccountsDbContext db, IClock clock, ILogger<AccountsSeeder> logger)
{
    public async Task SeedAsync(CancellationToken cancellationToken)
    {
        await db.MigrateOrCreateAsync(cancellationToken).ConfigureAwait(false);

        await EnsureAccount(DemoIds.AliceCurrent, DemoIds.AliceCustomer, AccountType.Current, "Alice Current", "10000001", 5000m, cancellationToken).ConfigureAwait(false);
        await EnsureAccount(DemoIds.AliceSavings, DemoIds.AliceCustomer, AccountType.Savings, "Alice Savings", "10000002", 2500m, cancellationToken).ConfigureAwait(false);
        await EnsureAccount(DemoIds.BobCurrent, DemoIds.BobCustomer, AccountType.Current, "Bob Current", "20000001", 1000m, cancellationToken).ConfigureAwait(false);
        await EnsureAccount(DemoIds.CarolCurrent, DemoIds.CarolCustomer, AccountType.Current, "Carol Current", "30000001", 750m, cancellationToken).ConfigureAwait(false);
        await db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        if (!await db.Beneficiaries.AnyAsync(cancellationToken).ConfigureAwait(false))
        {
            var bob = await db.Accounts.FirstAsync(a => a.Id == BankAccountId.From(DemoIds.BobCurrent), cancellationToken).ConfigureAwait(false);
            var carol = await db.Accounts.FirstAsync(a => a.Id == BankAccountId.From(DemoIds.CarolCurrent), cancellationToken).ConfigureAwait(false);
            db.Beneficiaries.Add(Beneficiary.CreateInternal(CustomerId.From(DemoIds.AliceCustomer), "Bob Nguyen", bob, clock));
            db.Beneficiaries.Add(Beneficiary.CreateInternal(CustomerId.From(DemoIds.AliceCustomer), "Carol Okoye", carol, clock));
        }

        await db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        logger.LogInformation("Accounts seed completed.");
    }

    private async Task EnsureAccount(
        Guid id,
        Guid customerId,
        AccountType type,
        string name,
        string number,
        decimal projected,
        CancellationToken cancellationToken)
    {
        if (await db.Accounts.AnyAsync(a => a.Id == BankAccountId.From(id), cancellationToken).ConfigureAwait(false))
        {
            return;
        }

        var account = BankAccount.Open(
            CustomerId.From(customerId),
            type,
            Currency.Gbp,
            name,
            clock,
            BankAccountId.From(id),
            AccountNumber.Restore(number, AccountNumber.DemoSortCode));
        account.ApplyProjectedBalance(projected, clock);
        db.Accounts.Add(account);
    }
}
