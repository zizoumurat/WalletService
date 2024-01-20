namespace WalletApi.Domain.Constants;

public static class ErrorMessages
{
    public const string WalletNameAlReadyExist = "Bu cüzdan adı daha önce kullanılmış";
    public const string WalletNotFound = "Cüzdan bulunamadı!";
    public const string WalletCanNotDelete = "Cüzdanınızda bakiyeniz bulunduğu için bu cüzdan silinemez";
    public const string InsufficientBalance = "Yetersiz bakiye!";
    public const string TryAgainLater = "İşleminizi şu an gerçekleştiremiyoruz. Lütfen daha sonra tekrar deneyin.";
}
