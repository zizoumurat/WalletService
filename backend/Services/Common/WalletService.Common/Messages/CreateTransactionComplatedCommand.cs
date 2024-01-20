namespace WalletService.Common.Messages;

public sealed record CreateTransactionComplatedCommand(string transactionId, string userId);
