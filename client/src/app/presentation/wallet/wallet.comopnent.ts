import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { IWalletService } from 'src/app/core/services/IWallet.service';
import { CreateWalletComponent } from './create-walllet/create.wallet.component';
import { Currency, WalletListModel } from 'src/app/core/domain/wallet.model';
import { DepositComponent } from './deposit/deposit.component';
import { HubService } from 'src/app/service/hub.service';
import { ILocalStorageService } from 'src/app/core/services/ILocal.storage.service';
import { USER_KEY } from 'src/app/core/constants/storage.keys';
import { UserModel } from 'src/app/core/domain/user.model';
import { MatSnackBar } from '@angular/material/snack-bar';
import { WithdrawComponent } from './withdraw/withdraw.component';

@Component({
    selector: 'app-wallet',
    templateUrl: './wallet.component.html',
    styleUrls: ['./wallet.component.css']

})

export class WalletComponent implements OnInit {
    constructor(private walletService: IWalletService,
        private dialog: MatDialog,
        private storageService: ILocalStorageService,
        private snackBar: MatSnackBar,
        private hubService: HubService) { }

    cards: any[];
    cardColors = ['#cc73d7', '#1DCC70', '#FF396F'];

    getCardColor(index: number): string {
        return this.cardColors[index % this.cardColors.length];
    }

    ngOnInit(): void {
        const user = this.storageService.getData(USER_KEY) as UserModel;
        this.hubService.startConnection(user.id);
        this.hubService.addNotificationListener((notification) => {
            if (notification.userId == user.id) {
                if(notification.status)
                    this.getList();

                this.dialog.closeAll();

                this.snackBar.open(notification.message);
            }
        });
        this.getList();
    }

    getList() {
        this.walletService.getList()
            .subscribe((res: any[]) => {
                this.cards = res;
            })
    }

    openCreateWalletDialog(): void {
        const dialogRef = this.dialog.open(CreateWalletComponent, {
            width: '640px', disableClose: true
        });

        dialogRef.afterClosed().subscribe(result => {
            if (!result)
                this.getList();
        });
    }

    openDepositDialog(selectedWallet: WalletListModel): void {
        this.dialog.open(DepositComponent, {
            width: '640px', disableClose: true,
            data: selectedWallet
        });
    }

    openWithdrawDialog(selectedWallet: WalletListModel): void {
       this.dialog.open(WithdrawComponent, {
            width: '640px', disableClose: true,
            data: selectedWallet
        });
    }

    getCurrencyName(value: number) {
        return Currency[value];
    }
}
