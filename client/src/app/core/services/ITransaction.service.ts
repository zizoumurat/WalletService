import { Observable } from "rxjs";
import { TransactionListModel } from "../domain/transaction.model";


export abstract class ITransactionService {
    abstract getList(walletId: number): Observable<TransactionListModel[]>;
    abstract deposit(walletId: number, amount: number): Observable<any>;
    abstract withdraw(walletId: number, amount: number): Observable<any>;
}
