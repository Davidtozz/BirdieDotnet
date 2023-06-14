
//TODO Use service instead of ChatView's logic

import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr';

export let hubConnection = null;


export const subscribeToHub = async (url, setMessages) => {

	hubConnection = new HubConnectionBuilder()
	.withUrl(url)
	.configureLogging(LogLevel.Information)
	.withAutomaticReconnect()
	.build();

	await hubConnection.start()
	try	{ //! DEBUG
		console.log("DEBUG: SignalR connection established")
	} catch(err) {
		console.log("Couldn't start connection to hub:", err)
	} finally {
		mapEventHandlers(setMessages);
	}
}

async function mapEventHandlers(setMessages) {
	hubConnection.on("ReceiveMessage", (id, msg, user) => {

		const incomingMessage = {
			text: msg,
			incoming: true
		}
		
		if(hubConnection.connectionId !== id) {
			setMessages(prevMessages => [...prevMessages, incomingMessage])
			console.log(`Received: ${msg} \nfrom: ${user.name}`)
		}
	
	})
}

/* export const sendMessage = async (e) => {
	if (e.key === 'Enter') {
	  
	  //! DEBUG
	  console.log(e.target.value);

	  await hubConnection.invoke("SendMessage", e.target.value, {name: "Jovanotti"});
	
	  const outgoingMessage = { //TODO manage logged user 
		  text: e.target.value,
		  incoming: false
	  }
	  
	  await setMessages(prevMessages => [...prevMessages, outgoingMessage])
	  e.target.value = ""
	
  }
}; */