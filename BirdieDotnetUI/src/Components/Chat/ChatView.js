import "../../styles/Components/ChatView.css"
import MessageBubble from "./MessageBubble";
import Contact from "../Contacts/Contact";
import { useEffect, useState } from 'react'; 
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import { ChatHubService } from "../../Services/ChatHubService";

//TODO migrate Hub logic to ChatHubService
//TODO migrate jwt cookie storage elsewhere

const hubConnection = new HubConnectionBuilder()
  .withUrl("http://localhost:5069/chathub")
  .configureLogging(LogLevel.Debug)
  .withAutomaticReconnect()
  .build();

function ChatView(props) {
    
    const [messages, setMessages] = useState([]);

  useEffect(() => {

    const startConnection = async () => {
    if(hubConnection.state !== "Connected" ) {
      await hubConnection.start()
      .then(() => {
        hubConnection.on("ReceiveMessage", onReceiveMessage)      
      })
    }}
    
    startConnection();

    return () => hubConnection.stop();
  },[]);

  const onReceiveMessage = (connectionId, message, user) => {

    const incomingMessage = {
        text: message,
        incoming: true,
        fromUser: user.Username
    }
    
    if(hubConnection.connectionId !== connectionId) {
        setMessages(prevMessages => [...prevMessages, incomingMessage])
        console.log(`Received: ${message} \nfrom: ${user.Userame}`)
    }

}

  //? Trigger SendMessage server-side method 
  const sendMessage = async (e) => {
      if (e.key === 'Enter') {
        //! DEBUG
        
        let message = e.target.value

        /* {Content: e.target.value, SenderId:76} */
        await hubConnection.invoke("SendMessage", message);
        
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
        <footer className="chat-footer-wrapper">
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