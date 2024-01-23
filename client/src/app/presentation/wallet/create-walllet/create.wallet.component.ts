import { Component, OnInit, VERSION, ViewChild } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { IWalletService } from 'src/app/core/services/IWallet.service';
import { Currency, WalletModel } from 'src/app/core/domain/wallet.model';
import { ILocalStorageService } from 'src/app/core/services/ILocal.storage.service';
import { USER_KEY } from 'src/app/core/constants/storage.keys';
import { UserModel } from 'src/app/core/domain/user.model';

@Component({
    selector: 'app-create-wallet',
    templateUrl: './create.wallet.component.html',
    styleUrls: ['./create.wallet.component.css']
})
export class CreateWalletComponent implements OnInit {

    public breakpoint: number; 
    public form: FormGroup;
    wasFormChanged = false;
    
    constructor(
        private fb: FormBuilder,
        public dialog: MatDialog,
        private walletService: IWalletService,
        private localService: ILocalStorageService
    ) { }

    currencies = Object.values(Currency).filter((value) => typeof value === 'number');
    CurrencyEnum = Currency;

    ngOnInit(): void {
        this.form = this.fb.group({
            name: ['', [Validators.required]],
            walletCurrency: [null, [Validators.required]],
        });

        this.breakpoint = window.innerWidth <= 600 ? 1 : 2;

        
    }

    getEnumText(value:any) {
        return  Currency[Number(value)]
    }

    public onAddCus(): void {
        this.markAsDirty(this.form);
    }

    closeDialog(): void {
        this.dialog.closeAll();
    }

    save(): void {
        if (this.form.invalid)
            return;

        const wallet = {
            name: this.form.get('name')?.value,
            userId: (this.localService.getData(USER_KEY) as UserModel).id,
            currency: this.form.get('walletCurrency')?.value,
        };

        this.walletService.create(wallet)
            .subscribe((_) => {
                this.closeDialog();
            });
    }

    public onResize(event: any): void {
        this.breakpoint = event.target.innerWidth <= 600 ? 1 : 2;
    }

    private markAsDirty(group: FormGroup): void {
        group.markAsDirty();
        // tslint:disable-next-line:forin
        for (const i in group.controls) {
            group.controls[i].markAsDirty();
        }
    }

    formChanged() {
        this.wasFormChanged = true;
    }
}