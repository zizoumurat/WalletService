import { Injectable } from "@angular/core";
import { ILocalStorageService } from "../core/services/ILocal.storage.service";

@Injectable({
    providedIn: 'root',
})
export class LocalStorageService extends ILocalStorageService {

    override saveData(key: string, value: any) {
        this.checkKey(key);

        localStorage.setItem(key, JSON.stringify(value));
    }

    override getData(key: string): any {
        this.checkKey(key);
        const item = localStorage.getItem(key);

        if (item && item !== 'undefined') {
            return JSON.parse(item);
        }

        return null;
    }

    override removeData(key: string) {
        this.checkKey(key);
        localStorage.removeItem(key);
    }

    override clearStorage() {
        localStorage.clear();
    }

    private checkKey(key: string): void {
        if (key == null || key === '') {
            throw new ReferenceError('storage key must not be null or empty');
        }
    }
}
