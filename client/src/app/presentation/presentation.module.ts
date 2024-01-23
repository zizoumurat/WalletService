import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { SharedModule } from "./shared/shared.module";
import { WalletModule } from "./wallet/wallet.module";
import { TransactonModule } from "./transaction/transaction.module";

@NgModule({
  declarations: [
  ],
  imports: [
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    SharedModule,
    WalletModule,
    TransactonModule
  ],
})

export class PresentationModule { }