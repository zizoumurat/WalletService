import { Component, Inject, Input, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ITransactionService } from 'src/app/core/services/ITransaction.service';
import { WalletListModel } from 'src/app/core/domain/wallet.model';

@Component({
    selector: 'app-withdraw',
    templateUrl: './withdraw.component.html',
    styleUrls: ['./withdraw.component.css']
})
export class WithdrawComponent implements OnInit {
    public breakpoint: number;
    public form: FormGroup;
    wasFormChanged = false;
    selectedWallet: WalletListModel;
    processing = false;

    constructor(
        private fb: FormBuilder,
        public dialog: MatDialog,
        private transactionService: ITransactionService,
        @Inject(MAT_DIALOG_DATA) public data: WalletListModel
    ) { }

    ngOnInit(): void {
        this.selectedWallet = this.data;
        this.form = this.fb.group({
            amount: [null, [Validators.required]],
        });

        this.breakpoint = window.innerWidth <= 600 ? 1 : 2;
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

        this.processing = true;

        const [walletId, amount] = [this.selectedWallet?.id, this.form.get('amount')?.value];

        this.transactionService.withdraw(walletId, amount)
            .subscribe((_) => {
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