import styles from './ChatView.module.scss';

const ChatView = () => {
    return <div className={styles.chatView}>
        <div className={styles.chatHeader}>
            <p>i'm the header!</p>
        </div>
        <div className={styles.chatBody}>
            <p>i'm the body!</p>
        </div>
        <div className={styles.chatFooter}>
            <p>i'm the footer!</p>
        </div>
    </div>;
}

export default ChatView;