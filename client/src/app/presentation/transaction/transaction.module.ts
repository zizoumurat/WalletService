import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TransactionComponent } from './transaction.component'
import { SharedModule } from 'src/app/presentation/shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
  ],
  declarations: [TransactionComponent],
  exports:[]
})

export class TransactonModule { }
