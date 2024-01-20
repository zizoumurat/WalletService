namespace WalletService.Common.Messages;

public sealed record CreateTransactionFailedCommand(string transactionId, string errorMessage, string userId);
