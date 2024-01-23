import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ITransactionService } from "../core/services/ITransaction.service";
import { ITransactionRepository } from "../core/repositories/ITransaction.repository";
import { TransactionListModel } from "../core/domain/transaction.model";
import { ILocalStorageService } from "../core/services/ILocal.storage.service";
import { UserModel } from "../core/domain/user.model";
import { USER_KEY } from "../core/constants/storage.keys";

@Injectable({
    providedIn: 'root',
})
export class TransactionService extends ITransactionService {
    user: UserModel;

    constructor(private repo: ITransactionRepository,
        private localStorageService: ILocalStorageService) {
        super();

        this.user = this.localStorageService.getData(USER_KEY) as UserModel;
    }

    override getList(walletId: number): Observable<TransactionListModel[]> {
        return this.repo.getList(walletId, this.user.id);
    }
    override deposit(walletId: number, amount: number): Observable<any> {
        return this.repo.newTransaction(walletId, this.user.id, amount)
    }
    override withdraw(walletId: number, amount: number): Observable<any> {
        return this.repo.newTransaction(walletId, this.user.id, (amount * -1))
    }
}
