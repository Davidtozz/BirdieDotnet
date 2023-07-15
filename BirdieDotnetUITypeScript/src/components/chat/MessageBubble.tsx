import Message from "types/Message";
import styles from "./MessageBubble.module.scss";

const MessageBubble = ({ content, isSentBySelf }: IMessageProps) => {

    //TODO display further info in the message bubble (timestamp, sender, etc.)

    return <>
      {isSentBySelf ?

        <div className={`${styles.messageBubbleWrapper} ${styles.reversed}`}>
            <div className={`${styles.triangle} ${styles.reversed}`} />
            <div className={`${styles.messageBubble} ${styles.reversed}`}>
                <p> {content} </p>
            </div>
        </div> 
        : 
        <div className={styles.messageBubbleWrapper}>
          <div className={styles.triangle}></div>
          <div className={`${styles.messageBubble}`}>
            <p> {content} </p>
          </div>
        </div>
        
        }
    </>
  }

interface IMessageProps {
    content: string;
    isSentBySelf: boolean;
}
  
  
export default MessageBubble;