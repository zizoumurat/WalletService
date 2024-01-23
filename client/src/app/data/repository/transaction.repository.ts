import { ITransactionRepository } from "src/app/core/repositories/ITransaction.repository";
import { Injectable } from "@angular/core";
import { Observable, map } from "rxjs";
import { ApiService } from "../api/api.service";
import { TransactionListModel, TransactionModel } from "src/app/core/domain/transaction.model";
import { HttpParams } from "@angular/common/http";
import { Currency } from "src/app/core/domain/wallet.model";

const endPoint = "transaction";

@Injectable()
export class TransactionRepository extends ITransactionRepository {
    constructor(
        private api: ApiService
    ) {
        super();
    }

    override getList(walletId: number, userId: string): Observable<TransactionListModel[]> {
        return this.api.get<TransactionModel[]>(`${endPoint}/${userId}/${walletId}`, httpParams).pipe(
            map(transactions => transactions.map(transaction => ({
                id: transaction.id,
                amount: transaction.amount,
                status: this.getStatus(transaction.status),
                date: transaction.date,
                type: transaction.amount > 0 ? 'Para Yatırma' : 'Para Çekme',
            } as TransactionListModel)))
        );
    }

    private getStatus(value: number) {
        console.log(value);
        return value === 1 ? 'Bekliyor' : (value === 2 ? 'Başarılı' : 'Hatalı');
    }
    
    override newTransaction(walletId: number, userId: string, amount: number): Observable<any> {
        return this.api.post(endPoint, { walletId, userId, amount });
    }
}
