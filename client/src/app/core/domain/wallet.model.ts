export class WalletModel {
    id: number;
    name: string;
    userId: string;
    balance: number;
    walletCurrency: Currency;
    createdDate: Date
}

export class WalletListModel {
    id: number;
    name: string;
    balance: number;
    walletCurrency: Currency;
    currencyName: string;
}

export enum Currency {
    USD = 1,
    EUR = 2,
    TL = 3,
}