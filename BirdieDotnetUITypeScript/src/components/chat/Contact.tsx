import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCircleUser } from '@fortawesome/free-solid-svg-icons';
import styles from './Contact.module.scss';

const Contact = ({name, picture}: ContactProps)  => {

    return (
        <div className={styles.contactCard}>
            
            <div className={styles.propicWrapper}>
            {/*  <img className={styles.propic} src={picture} alt='pic'/>   */}
            <FontAwesomeIcon icon={faCircleUser} className={styles.propic} />
 
             </div>
            
            <div className={styles.contactDetails}>
                <h2>{name}</h2>                  
                <h4>Dolor sit amet...</h4>    
            </div>
            <div className="messageInfo">
                6:50 
            </div>

        </div>
    )
}

type ContactProps = {
    name: string,
    picture: string
} 

export default Contact;