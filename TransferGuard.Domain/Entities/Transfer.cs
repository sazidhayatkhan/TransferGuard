
using TransferGuard.Domain.Enums;
using TransferGuard.Domain.ValueObjects;

namespace TransferGuard.Domain.Entities;

public sealed class Transfer
{
    public Guid Id { get; private set; }

    public Guid SenderWalletId { get; private set; }

    public Guid ReceiverWalletId { get; private set; }

    public Money Amount { get; private set; } = null!;

    public TransferStatus Status { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }

    private Transfer()
    {
        // Reserved for ORM materialization.
    }

    private Transfer(
        Guid senderWalletId,
        Guid receiverWalletId,
        Money amount)
    {
        if (senderWalletId == Guid.Empty)
            throw new ArgumentException(
                "Sender wallet ID is required.",
                nameof(senderWalletId));

        if (receiverWalletId == Guid.Empty)
            throw new ArgumentException(
                "Receiver wallet ID is required.",
                nameof(receiverWalletId));

        if (senderWalletId == receiverWalletId)
            throw new ArgumentException(
                "Sender and receiver must be different.");

        ArgumentNullException.ThrowIfNull(amount);

        if (amount.Amount <= 0)
            throw new ArgumentException(
                "Transfer amount must be positive.",
                nameof(amount));

        Id = Guid.NewGuid();
        SenderWalletId = senderWalletId;
        ReceiverWalletId = receiverWalletId;
        Amount = amount;
        Status = TransferStatus.Pending;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    public static Transfer Create(
        Guid senderWalletId,
        Guid receiverWalletId,
        Money amount)
    {
        return new Transfer(
            senderWalletId,
            receiverWalletId,
            amount);
    }
}
