namespace WalletService.Common.Constants;

public static class QueueNames
{
    public const string transactionQueueName = "transaction";
    public const string transactionQueueNameUri = "queue:transaction";
    public const string transactionComplatedQueueName = "transaction_complated";
    public const string transactionComplatedQueueNameUri = "queue:transaction_complated";
    public const string transactionFailedQueueName = "transaction_failed";
    public const string transactionFailedQueueNameUri = "queue:transaction_failed";
}
