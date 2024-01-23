import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { IAuthService } from '../services/IAuth.service';
import { ILocalStorageService } from '../services/ILocal.storage.service';
import { AUTH_KEY } from 'src/app/core/constants/storage.keys';
import { AuthModel } from '../common/auth.model';

@Injectable({ providedIn: 'root' })
export class AuthGuard  {
  constructor(private authService: IAuthService, private localService: ILocalStorageService) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    const auth = (this.localService.getData(AUTH_KEY)) as AuthModel;

    if (auth && auth.token) {
      return true;
    }

    this.authService.logout();
    return false;
  }
}
