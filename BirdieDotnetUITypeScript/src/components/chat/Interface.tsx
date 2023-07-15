import styles from "./Interface.module.scss";
import ChatView from "./ChatView";
import ContactView from "./ContactView";

/**
 * The chat interface
 */
const Interface = () => {
  return (
    <main className={styles.interfaceContainer}>
      <section className={styles.interface}>
        <ContactView />
        <ChatView />
      </section>
    </main>
  );
};

export default Interface;
