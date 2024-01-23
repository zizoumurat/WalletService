import { Injectable } from "@angular/core";
import { Observable, map } from "rxjs";
import { IWalletRepository } from "src/app/core/repositories/IWallet.repository";
import { ApiService } from "../api/api.service";
import { Currency, WalletListModel, WalletModel } from "src/app/core/domain/wallet.model";

const endPoint = "wallet";

@Injectable()
export class WalletRepository extends IWalletRepository {
    constructor(
        private api: ApiService
    ) {
        super();
    }

    override getList(): Observable<WalletListModel[]> {
        return this.api.get<WalletModel[]>(endPoint).pipe(
            map(wallets => wallets.map(wallet => ({
              ...wallet,
              currencyName: this.getCurrencyName(wallet.walletCurrency)
            })))
          );
    }
    
    override create(data: any): Observable<any> {
        return this.api.post(endPoint, data);
    }

    private getCurrencyName(walletCurrency: number): string {
        return Currency[walletCurrency]
      }
}