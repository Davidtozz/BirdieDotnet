import Message from 'types/Message';
import styles from './ChatView.module.scss';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import MessageBubble from './MessageBubble';
import { faBars } from '@fortawesome/free-solid-svg-icons';
import { useState } from 'react';


const ChatView = () => {

    const [messages, setMessages] = useState<Message[]>([]); //TODO: replace with real data from API

/*     const messages: Message[] = [{
        id: 1,
        sender: "me",
        receiver: "you",
        content: "lorem ipsum",
        timestamp: new Date(),
        isSentBySelf: true
    }]; */ 

    const sendMessage = async (e: any) => {
        if (e.key === 'Enter') {
          //! DEBUG
          
          const message = e.target.value
  
          /* {Content: e.target.value, SenderId:76} */

          
          const outgoingMessage: Message = {
            id: 1,
            sender: "me",
            receiver: "you",
            content: message,
            timestamp: new Date(),
            isSentBySelf: true
        }             
          
          await setMessages(prevMessages => [...prevMessages, outgoingMessage])
          e.target.value = ""
        
      }
    };
  


    return <div className={styles.chatView}>

        <header className={styles.chatHeader}>
            <img src='https://picsum.photos/1500' alt='pic' className={styles.propic}/>
            <div className={styles.contactInfo}>
                <h3>John Doe</h3>
                <p>last seen 6:50</p>
            </div>
            <FontAwesomeIcon icon={faBars} className={`${styles.optionsIcon} fa-lg`}/>
        </header>

        <main className={styles.chatBody}>
            {
                messages.map((message: Message) => {
                    return <MessageBubble content={message.content} isSentBySelf={message.isSentBySelf} />;
                })
            }
        </main>

        <footer className={styles.chatFooter}>
            <input placeholder='Send a message...' onKeyDown={sendMessage}/>
        </footer>

    </div>;
}

export default ChatView;