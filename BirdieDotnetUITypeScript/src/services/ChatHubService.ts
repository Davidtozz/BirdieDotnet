import { HubConnectionBuilder, LogLevel, HubConnection } from '@microsoft/signalr';

export default class ChatHubService {

    public static _instance: ChatHubService;
    private hubConnection: HubConnection;

    private constructor() {
        this.hubConnection = new HubConnectionBuilder()
        .withUrl("http://localhost:5069/chatHub")
        .withAutomaticReconnect()
        .configureLogging(LogLevel.Debug)
        .build();
    }

    public static get instance(): ChatHubService {
        if (!ChatHubService._instance) {
            ChatHubService._instance = new ChatHubService();
        }

        return ChatHubService._instance;
    }

    public get connection(): HubConnection {
        return this.hubConnection;
    }

    public async subscribeToHub() {
        try {
            await this.hubConnection.start();
        } catch (err) {
            console.log(err);
        }
        finally {
            await this.mapEventHandlers();
        }
        console.log("connected");
    }

    private async mapEventHandlers() {
        this.hubConnection.on("ReceiveMessage", (user, message) => {
            console.log(user + " says " + message);
        });
    }
}