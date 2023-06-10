import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr';

export const chatHub =  new HubConnectionBuilder()
        .withUrl("https://localhost:5069/chathub")
        .configureLogging(LogLevel.Information)
        .withAutomaticReconnect()
        .build();
        