import { Observable } from "rxjs";
import { TransactionListModel } from "../domain/transaction.model";

export abstract class ITransactionRepository {
    abstract getList(walletId: number, userId: string): Observable<TransactionListModel[]>;
    abstract newTransaction(walletId: number, userId: string, amount: number): Observable<any>;
}