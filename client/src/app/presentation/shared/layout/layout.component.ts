import { Component, OnInit, ChangeDetectorRef, OnDestroy, AfterViewInit } from '@angular/core';
import { MediaMatcher } from '@angular/cdk/layout';
import { Subscription } from 'rxjs';

import { SpinnerService } from 'src/app/service/spinner.service';
import { ILocalStorageService } from 'src/app/core/services/ILocal.storage.service';
import { UserModel } from 'src/app/core/domain/user.model';
import { USER_KEY } from 'src/app/core/constants/storage.keys';
import { IAuthService } from 'src/app/core/services/IAuth.service';

@Component({
    selector: 'app-layout',
    templateUrl: './layout.component.html',
    styleUrls: ['./layout.component.css']
})
export class LayoutComponent implements OnInit, OnDestroy, AfterViewInit {

    private _mobileQueryListener: () => void;
    mobileQuery: MediaQueryList;
    showSpinner: boolean = false;
    user: UserModel;

    private autoLogoutSubscription: Subscription = new Subscription;

    constructor(private changeDetectorRef: ChangeDetectorRef,
        private media: MediaMatcher,
        private localService: ILocalStorageService,
        private authService: IAuthService,
        public spinnerService: SpinnerService) {

        this.mobileQuery = this.media.matchMedia('(max-width: 1000px)');
        this._mobileQueryListener = () => changeDetectorRef.detectChanges();
        // tslint:disable-next-line: deprecation
        this.mobileQuery.addListener(this._mobileQueryListener);
    }

    ngOnInit(): void {
        this.user = this.localService.getData(USER_KEY) as UserModel;
    }

    ngOnDestroy(): void {
        // tslint:disable-next-line: deprecation
        this.mobileQuery.removeListener(this._mobileQueryListener);
    }

    ngAfterViewInit(): void {
        this.changeDetectorRef.detectChanges();
    }

    ngAfterContentChecked(): void  {
        this.changeDetectorRef.detectChanges();
    }
    
    logOut() {
        this.authService.logout();
    }
}
