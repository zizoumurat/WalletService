import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { IWalletService } from "../core/services/IWallet.service";
import { WalletListModel, WalletModel } from "../core/domain/wallet.model";
import { IWalletRepository } from "../core/repositories/IWallet.repository";
import { UserModel } from "../core/domain/user.model";
import { ILocalStorageService } from "../core/services/ILocal.storage.service";
import { USER_KEY } from "../core/constants/storage.keys";

@Injectable({
    providedIn: 'root',
})
export class WalletService extends IWalletService {
    user: UserModel;
    
    constructor(private repo: IWalletRepository,
        private localStorageService: ILocalStorageService) {
        super();

        this.user = this.localStorageService.getData(USER_KEY) as UserModel;
    }

    override getList(): Observable<WalletListModel[]> {
        return this.repo.getList();
    }

    override create(data: any): Observable<any> {
        return this.repo.create(data);
    }
}
