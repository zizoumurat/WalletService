import { Observable } from "rxjs";
import { WalletListModel, WalletModel } from "../domain/wallet.model";

export abstract class IWalletService {
    abstract getList(): Observable<WalletListModel[]>;
    abstract create(data: any): Observable<any>;
}
