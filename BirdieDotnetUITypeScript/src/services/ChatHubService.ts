import { HubConnectionBuilder, LogLevel, HubConnection } from '@microsoft/signalr';



export default class ChatHubService {

    private static _instance: ChatHubService;

    hubConnection: HubConnection = null!;
    private constructor() {
        this.hubConnection = new HubConnectionBuilder()
        .withUrl("http://localhost:5069/chatHub")
        .withAutomaticReconnect()
        .configureLogging(LogLevel.Debug)
        .build();
    }

    public static getInstance(): ChatHubService {
        if (!ChatHubService._instance) {
            ChatHubService._instance = new ChatHubService();
        }

        return ChatHubService._instance;
    }

    public get connection(): HubConnection {
        return this.hubConnection;
    }

    public async subscribe() {
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


let chatHubService = ChatHubService.getInstance();