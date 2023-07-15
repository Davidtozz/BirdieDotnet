/* import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faSearch } from '@fortawesome/free-solid-svg-icons'; */
import Contact from "./Contact";
import styles from "./ContactView.module.scss";

const ContactView = () => {

  /**
   * This is a dummy array of contacts for testing purposes.
   */
  const contactElements = Array.from({ length: 20 }, (_, index) => (
    <Contact key={index} name={"John Doe"} picture={"none"} />
  ));

  return (
    <div className={styles.contactView}>
      <div className={styles.searchCard}>
        <input type="text" placeholder="Search" className={styles.searchField} />
        {/* <FontAwesomeIcon icon={faSearch} className={styles.searchIcon} /> */}
      </div>

      {/* <hr className={styles.divider} /> */}

      <div className={styles.contactList}>  
        {contactElements}
      </div>
    </div>
  );
};

export default ContactView;
