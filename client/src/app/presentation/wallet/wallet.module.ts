import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { WalletComponent } from './wallet.comopnent';
import { CreateWalletComponent } from './create-walllet/create.wallet.component';
import { DepositComponent } from './deposit/deposit.component';
import { WithdrawComponent } from './withdraw/withdraw.component';
import { SharedModule } from 'src/app/presentation/shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
  ],
  declarations: [WalletComponent, CreateWalletComponent, DepositComponent, WithdrawComponent],
  exports:[]
})

export class WalletModule { }
