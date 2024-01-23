export abstract class ILocalStorageService {
    abstract saveData(key: string, value: any): void;
    abstract getData(key: string): any;
    abstract removeData(key: string): void;
    abstract clearStorage(): void;
}
