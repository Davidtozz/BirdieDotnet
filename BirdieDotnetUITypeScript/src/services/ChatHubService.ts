import { HubConnectionBuilder, LogLevel, HubConnection } from '@microsoft/signalr';
import User  from 'types/User';
import Message  from 'types/Message';

export default class ChatHubService {

    public static _instance: ChatHubService;
    private readonly hubConnection: HubConnection;

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

    private async mapEventHandlers(): Promise<void> {
        this.hubConnection.on("ReceiveMessage", (user: User, message: Message) => {
            console.log(user + " says " + message);
        });
    }
}