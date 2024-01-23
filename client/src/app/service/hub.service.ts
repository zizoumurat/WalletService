import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class HubService {
  private hubConnection: signalR.HubConnection | undefined;

  startConnection(userId: string): void {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${environment.hubUrl}?userId=${userId}`)
      .build();

    this.hubConnection
      .start()
      .catch((err: any) => console.error('Error while starting SignalR connection: ', err));
  }

  addNotificationListener(callback: (notification: any) => void): void {
    this.hubConnection?.on('ReceiveNotification', (notification: any) => {
      callback(notification);
    });
  }
}