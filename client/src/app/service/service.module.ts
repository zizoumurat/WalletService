import { ErrorHandler, NgModule } from "@angular/core";
import { IWalletRepository } from "../core/repositories/IWallet.repository";
import { WalletRepository } from "../data/repository/wallet.repository";
import { IWalletService } from "../core/services/IWallet.service";
import { WalletService } from "./wallet.service";

import { ITransactionRepository } from "../core/repositories/ITransaction.repository";
import { TransactionRepository } from "../data/repository/transaction.repository";
import { ITransactionService } from "../core/services/ITransaction.service";
import { TransactionService } from "./transaction.service";
import { IAuthService } from "../core/services/IAuth.service";
import { AuthService } from "./auth.service";
import { ILocalStorageService } from "../core/services/ILocal.storage.service";
import { LocalStorageService } from "./local.storage.service";


@NgModule({
    providers: [
        { provide: IWalletRepository, useClass: WalletRepository },
        { provide: IWalletService, useClass: WalletService },

        { provide: ITransactionRepository, useClass: TransactionRepository },
        { provide: ITransactionService, useClass: TransactionService },

        { provide: IAuthService, useClass: AuthService },
        { provide: ILocalStorageService, useClass: LocalStorageService },
    ],
})
export class ServiceModule { }
