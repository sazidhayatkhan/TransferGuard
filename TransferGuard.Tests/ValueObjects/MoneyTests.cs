
using TransferGuard.Domain.ValueObjects;

namespace TransferGuard.Tests.ValueObjects;

public class MoneyTests
{
    [Fact]
    public void Add_SameCurrency_ReturnsCorrectAmount()
    {
        var first = new Money(1000m, "BDT");
        var second = new Money(200m, "BDT");

        var result = first.Add(second);

        Assert.Equal(1200m, result.Amount);
        Assert.Equal("BDT", result.Currency);
    }

    [Fact]
    public void Add_DifferentCurrencies_ThrowsException()
    {
        var bdt = new Money(1000m, "BDT");
        var usd = new Money(100m, "USD");

        Assert.Throws<InvalidOperationException>(
            () => bdt.Add(usd));
    }

    [Fact]
    public void Subtract_InsufficientAmount_ThrowsException()
    {
        var balance = new Money(100m, "BDT");
        var withdrawal = new Money(200m, "BDT");

        Assert.Throws<InvalidOperationException>(
            () => balance.Subtract(withdrawal));
    }

    [Fact]
    public void Constructor_NegativeAmount_ThrowsException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => new Money(-100m, "BDT"));
    }

    [Fact]
    public void Constructor_ZeroAmount_IsAllowed()
    {
        var money = new Money(0m, "BDT");

        Assert.Equal(0m, money.Amount);
    }
}
