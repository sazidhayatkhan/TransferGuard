
namespace TransferGuard.Domain.ValueObjects;

public sealed record Money
{
    public decimal Amount { get; }
    public string Currency { get; }

    public Money(decimal amount, string currency)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(
                nameof(amount),
                "Money cannot be negative.");

        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentException(
                "Currency is required.",
                nameof(currency));

        Amount = amount;
        Currency = currency.Trim().ToUpperInvariant();
    }

    public Money Add(Money other)
    {
        EnsureSameCurrency(other);
        return new Money(Amount + other.Amount, Currency);
    }

    public Money Subtract(Money other)
    {
        EnsureSameCurrency(other);

        if (other.Amount > Amount)
            throw new InvalidOperationException(
                "Insufficient amount.");

        return new Money(Amount - other.Amount, Currency);
    }

    public bool IsGreaterThanOrEqualTo(Money other)
    {
        EnsureSameCurrency(other);
        return Amount >= other.Amount;
    }

    private void EnsureSameCurrency(Money other)
    {
        ArgumentNullException.ThrowIfNull(other);

        if (!string.Equals(
                Currency,
                other.Currency,
                StringComparison.Ordinal))
        {
            throw new InvalidOperationException(
                "Cannot operate on different currencies.");
        }
    }
}
