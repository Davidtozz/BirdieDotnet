import { HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';

let instance;

export default class ChatHubService{

	hubConnection = null;
	baseUrl = "https://localhost:5069";
	

	constructor() {

		if(instance) {
			throw new Error("ChatHubService is a singleton.")
		}

		this.hubConnection = new HubConnectionBuilder()
			.withUrl(this.baseUrl + "/chatHub")
			.configureLogging(LogLevel.Debug)
			.withAutomaticReconnect()
			.build();

		instance = this;
	}

	async subscribe(setMessages) {
		
		try	{
			await this.hubConnection.start()
			console.log("DEBUG: SignalR connection established")
		} catch(err) {
			console.log("Couldn't start connection to hub:", err)
		}
		finally {
			await this.mapEventHandlers(setMessages)	
		}
		
	}

	async mapEventHandlers(setMessagesCallback) {
		this.hubConnection.on("ReceiveMessage", (id, msg, user) => {

			const incomingMessage = {
				text: msg,
				incoming: true
			}
			
			if(this.hubConnection.connectionId !== id) {
				setMessagesCallback(prevMessages => [...prevMessages, incomingMessage])
				console.log(`Received: ${msg} \nfrom: ${user.name}`)
			}
		
		})
	}
}