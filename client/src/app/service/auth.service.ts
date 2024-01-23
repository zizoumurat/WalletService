import { Injectable } from "@angular/core";
import { IAuthService } from "../core/services/IAuth.service";
import { HttpHeaders, HttpClient } from "@angular/common/http";
import { Observable, catchError, map, of, tap } from "rxjs";
import { ILocalStorageService } from "../core/services/ILocal.storage.service";
import { AUTH_KEY, USER_KEY } from "../core/constants/storage.keys";
import { AuthModel } from "../core/common/auth.model";
import { Router } from "@angular/router";
import { environment } from "src/environments/environment";
import { UserModel } from "../core/domain/user.model";


@Injectable({
    providedIn: 'root',
})
export class AuthService extends IAuthService {

    constructor(private http: HttpClient,
        private localStorageService: ILocalStorageService,
        private router: Router) {
        super();
    }

    override login(username: string, password: string): Observable<any> {

        const headers = new HttpHeaders({
            'Content-Type': 'application/x-www-form-urlencoded',
            'Authorization': 'Basic ' + btoa('webClientUser:mySecret')
        });

        const body = `grant_type=password&username=${username}&password=${password}&client_id=your-client-id&client_secret=your-client-secret`;

        return this.http.post(`${environment.identityUrl}connect/token`, body, { headers })
            .pipe(
                tap((response: any) => {
                    const auth = {
                        token: response.access_token,
                        refreshToken: response.refresh_token,
                        expireIn: response.expires_in
                    };
                    this.localStorageService.saveData(AUTH_KEY, auth);
                }),
                catchError((error) => {
                    console.error('Login Error:', error);
                    throw error;
                })
            )
    }


    override logout(): void {
        this.localStorageService.clearStorage();
        this.router.navigate(["/login"]);
    }

    override getUserData():  Observable<any> {
        const auth = this.localStorageService.getData(AUTH_KEY) as AuthModel;

        if (!auth.token) {
            this.logout();
        }

        const headers = new HttpHeaders({
            'Authorization': `Bearer ${auth.token}`
        });

        return this.http.get<UserModel>(`${environment.identityUrl}api/user`, { headers }).pipe(
            tap((response) => {
                this.localStorageService.saveData(USER_KEY, response);
            })
        )
    }
}
