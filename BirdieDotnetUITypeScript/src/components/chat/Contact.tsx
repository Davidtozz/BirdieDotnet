import styles from './Contact.module.scss';

const Contact = ({name, picture}: ContactProps)  => {

    return (
        <div className={styles.contactCard}>
            
            <div className={styles.propicWrapper}>
                <img className={styles.propic} src={picture} alt='pic'/>
            </div>
            
            <div className={styles.contactDetails}>
                <h2>{name}</h2>                  
                <h4>Dolor sit amet...</h4>    
            </div>
            <div className="message-info">
                <div className="last-message-time">6:50</div> 
            </div>

        </div>
    )
}

type ContactProps = {
    name: string,
    picture: string
} 

export default Contact;