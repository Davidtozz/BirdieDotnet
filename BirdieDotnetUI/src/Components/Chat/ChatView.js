import MessageBubble from "./MessageBubble";
import Contact from "../Contacts/Contact"
import { useState } from 'react'; 

function ChatView(props) {


    const [messages, setMessages] = useState([]);

/*     useEffect(() => {
       console.log(messages);
    },[messages]) */

    const sendMessage = async (e) => {
        if(e.key === 'Enter') {

            console.log(e.target.value);
            
            await setMessages(prevMessages => [...prevMessages, e.target.value])
            e.target.value = "";
        }
    } 

    return <>
        <div className='chat-view'>
            <header className="contact-card chat-header">
                <Contact header={true} picurl={'https://source.unsplash.com/random/500x500/?pic'}/>
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