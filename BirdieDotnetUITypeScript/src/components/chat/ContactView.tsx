/* import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faSearch } from '@fortawesome/free-solid-svg-icons'; */
import Contact from "./Contact";
import styles from "./ContactView.module.scss";

const ContactView = () => {
  return (
    <div className={styles.contactView}>
      <div className={styles.searchCard}>
        <input type="text" placeholder="Search" className={styles.searchField} />
        {/* <FontAwesomeIcon icon={faSearch} className={styles.searchIcon} /> */}
      </div>

      <hr className={styles.divider} />

      <div className={styles.contactList}>
        <Contact name={"John Doe"} picture={"none"} />
        <Contact name={"John Doe"} picture={"none"} />
        <Contact name={"John Doe"} picture={"none"} />
        <Contact name={"John Doe"} picture={"none"} />
        <Contact name={"John Doe"} picture={"none"} />
        <Contact name={"John Doe"} picture={"none"} />
        <Contact name={"John Doe"} picture={"none"} />
        <Contact name={"John Doe"} picture={"none"} />
        <Contact name={"John Doe"} picture={"none"} />
        <Contact name={"John Doe"} picture={"none"} />
        <Contact name={"John Doe"} picture={"none"} />
        <Contact name={"John Doe"} picture={"none"} />
        <Contact name={"John Doe"} picture={"none"} />
        <Contact name={"John Doe"} picture={"none"} />
        <Contact name={"John Doe"} picture={"none"} />
        <Contact name={"John Doe"} picture={"none"} />
        <Contact name={"John Doe"} picture={"none"} />
        <Contact name={"John Doe"} picture={"none"} />
        <Contact name={"John Doe"} picture={"none"} />
        <Contact name={"John Doe"} picture={"none"} />
        <Contact name={"John Doe"} picture={"none"} />
        <Contact name={"John Doe"} picture={"none"} />
        <Contact name={"John Doe"} picture={"none"} />
        <Contact name={"John Doe"} picture={"none"} />
        <Contact name={"John Doe"} picture={"none"} />
        <Contact name={"John Doe"} picture={"none"} />
        <Contact name={"John Doe"} picture={"none"} />
        <Contact name={"John Doe"} picture={"none"} />
        <Contact name={"John Doe"} picture={"none"} />
        <Contact name={"John Doe"} picture={"none"} />
        <Contact name={"John Doe"} picture={"none"} />
        <Contact name={"John Doe"} picture={"none"} />
        <Contact name={"John Doe"} picture={"none"} />
        <Contact name={"John Doe"} picture={"none"} />
        <Contact name={"John Doe"} picture={"none"} />
        <Contact name={"John Doe"} picture={"none"} />
        <Contact name={"John Doe"} picture={"none"} />
        <Contact name={"John Doe"} picture={"none"} />
        <Contact name={"John Doe"} picture={"none"} />
        <Contact name={"John Doe"} picture={"none"} />
        <Contact name={"John Doe"} picture={"none"} />
        <Contact name={"John Doe"} picture={"none"} />
        <Contact name={"John Doe"} picture={"none"} />
        <Contact name={"John Doe"} picture={"none"} />
        <Contact name={"John Doe"} picture={"none"} />
      </div>
    </div>
  );
};

export default ContactView;
