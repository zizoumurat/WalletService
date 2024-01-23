export class TransactionModel {
    id: string;
    userId: string;
    walletId: number;
    amount: number;
    status: TransactionStatus;
    date: Date
}

export enum TransactionStatus {
    Pending = 1,
    Complated = 2,
    Failed = 3
}

export class TransactionListModel {
    id: string;
    amount: number;
    status: string;
    date: Date;
    type: string;
}