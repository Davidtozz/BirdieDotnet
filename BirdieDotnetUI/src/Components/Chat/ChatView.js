import MessageBubble from "./MessageBubble";
import Contact from "../Contacts/Contact"
import { useEffect, useState } from 'react'; 
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr';


        

function ChatView(props) {

    const [messages, setMessages] = useState([]);
    const [hubConnection, setHubConnection] = useState(
        new HubConnectionBuilder()
            .withUrl("http://localhost:5069/chathub")
            .configureLogging(LogLevel.Information)
            .withAutomaticReconnect()
            .build()
    );
    
    useEffect(() => {
        

        const startHubConnection = async () => {
            try {
                await hubConnection.start()
                console.log("Connection to ChatHub established")
            } catch (err) {
                console.log(err)
                setTimeout(startHubConnection, 5000)
            }
        } 
        
        startHubConnection();

        

        return () => {
            hubConnection.stop()
        };
    })

    



    const sendMessage = async (e) => {
        if(e.key === 'Enter') {

            console.log(e.target.value);
            
            await setMessages(prevMessages => [...prevMessages, e.target.value])
            e.target.value = "";
        }
    } 

    return <>
        <div className='chat-view'>
        <header className="contact-card chat-header">                      {/* Images are random now, but won't later */} 
                <Contact contactName={props.selectedContact} isHeader={true} picUrl={'https://source.unsplash.com/random/500x500/?pic'}/>
            </header>
            
            <div className="message-list">    
                {
                    messages.map((msg,index) => { // TODO handle logic for ongoing/incoming messages 
                        return <MessageBubble incoming={false} message={msg} key={index}/>;        
                    })
                }
            </div>

            <footer>
                <input 
                onKeyDown={sendMessage} // send message if Enter was pressed
                type="text" 
                placeholder="Type a message..." className="chat-footer" />
            </footer>
        </div>
    </>
}

export default ChatView;