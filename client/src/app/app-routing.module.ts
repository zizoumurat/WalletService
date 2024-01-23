import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LayoutComponent } from './presentation/shared/layout/layout.component';
import { AuthGuard } from './core/guards/auth.guard';
import { WalletComponent } from './presentation/wallet/wallet.comopnent';
import { TransactionComponent } from './presentation/transaction/transaction.component';


const appRoutes: Routes = [
  {
    path: "",
    component: LayoutComponent,
    canActivate: [AuthGuard],
    children: [
      { path: "wallets", component: WalletComponent },
      { path: "transactions", component: TransactionComponent },
    ]
  },
  {
    path: 'login',
    loadChildren: () => import('./presentation/login/login.module').then(m => m.LoginModule),
  },
  {
    path: '**',
    redirectTo: 'wallets',
    pathMatch: 'full'
  }
];

@NgModule({
  imports: [
    RouterModule.forRoot(appRoutes)
  ],
  exports: [RouterModule],
  providers: []
})
export class AppRoutingModule { }
