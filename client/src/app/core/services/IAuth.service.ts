import { Observable } from "rxjs";

export abstract class IAuthService {
    abstract login(username: string, password: string): Observable<any>;
    abstract logout(): void;
    abstract getUserData(): Observable<any>;
}
