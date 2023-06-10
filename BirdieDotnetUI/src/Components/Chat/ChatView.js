import MessageBubble from "./MessageBubble";
import Contact from "../Contacts/Contact";
import { useEffect, useState } from 'react'; 
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr';

function ChatView(props) {
    const [messages, setMessages] = useState([]);
    const hubConnection = new HubConnectionBuilder()
        .withUrl("http://localhost:5069/chathub")
        .configureLogging(LogLevel.Information)
        .withAutomaticReconnect()
        .build();

  useEffect(() => {

    const createHubConnection = async () => {
      try {
        await hubConnection.start();
        //! DEBUG
        console.log("Connection to ChatHub established");
      } catch (err) {
        console.log(err);
        setTimeout(createHubConnection, 5000);
      }
    };

    createHubConnection();

    //? Handler for SendMessage() server-side method 
    hubConnection.on("ReceiveMessage", (id, msg, user) => {

        const incomingMessage = {
            text: msg,
/*          name: user.name,
            userId: id, */
            incoming: true
        }
        
        if(hubConnection.connectionId !== id) {
            setMessages(prevMessages => [...prevMessages, incomingMessage])
            console.log(`Received: ${msg} \nfrom: ${user.name}`)
        }

    })
    
    return () => hubConnection.stop();
  });

  //? Trigger SendMessage server-side method 
  const sendMessage = async (e) => {
      if (e.key === 'Enter') {
        
        //! DEBUG
        console.log(e.target.value);

        hubConnection.invoke("SendMessage", e.target.value, {name: "Jovanotti"});
      
        const outgoingMessage = { //TODO manage logged user 
            text: e.target.value,
            incoming: false
        }
        
        await setMessages(prevMessages => [...prevMessages, outgoingMessage])
        e.target.value = ""
      
    }
  };

  return (
    <>
      <div className='chat-view'>
        <header className="contact-card chat-header">
          {/* Images are random now, but won't later */}
          <Contact contactName={props.selectedContact} isHeader={true} picUrl={'https://source.unsplash.com/random/500x500/?pic'}/>
        </header>
        <div className="message-list">    
          {
            messages.map((msg, index) => {
              // TODO handle logic for ongoing/incoming messages 
              return <MessageBubble 
                incoming={msg.incoming} 
                message={msg.text} 
                key={index}/>;        
            })
          }
        </div>
        <footer>
          <input 
            onKeyDown={sendMessage} // send message if Enter was pressed
            type="text" 
            placeholder="Type a message..." 
            className="chat-footer" 
          />
        </footer>
      </div>
    </>
  );
}

export default ChatView;
