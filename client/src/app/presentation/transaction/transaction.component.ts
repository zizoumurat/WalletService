import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { IWalletService } from 'src/app/core/services/IWallet.service';
import { ITransactionService } from 'src/app/core/services/ITransaction.service';
import { ILocalStorageService } from 'src/app/core/services/ILocal.storage.service';
import { USER_KEY } from 'src/app/core/constants/storage.keys';
import { UserModel } from 'src/app/core/domain/user.model';
import { WalletListModel } from 'src/app/core/domain/wallet.model';

@Component({
    selector: 'app-transaction',
    templateUrl: './transaction.component.html',
    styleUrls: ['./transaction.component.css']

})

export class TransactionComponent implements OnInit {
    constructor(private walletService: IWalletService,
        private transactionService: ITransactionService) { }

    transactions: any[];
    wallets: WalletListModel[]
    selectedWallet: WalletListModel;
    displayedColumns: string[] = ['id', 'date', 'type', 'amount', 'status'];

    ngOnInit(): void {
        this.getWalletList();
    }

    getWalletList() {
        this.walletService.getList()
            .subscribe((data) => {
                this.wallets = data;
                if (this.wallets.length > 0) {
                    this.selectedWallet = this.wallets[0];
                    this.getList();
                }
            })
    }

    getList() {
        console.log(this.selectedWallet.id);
        this.transactionService.getList(this.selectedWallet.id)
            .subscribe((res: any[]) => {
                this.transactions = res;
            })
    }

    onWalletChange() {
        this.getList();
    }
}
