import {ReactComponent as Pinned} from '../../static/svg/Contact/pinned.svg';
import "../../styles/Components/Contact.css"

// {picurl: url, contactname: name, header: isHeader}
const Contact = (props) => {

    return <>
        <div onClick={props.onClick} className='contact-card'>
            
            <div className="propic-wrapper">
                <img className='propic' src={props.picUrl} alt='pic'/>
            </div>
            
            <div className="contact-details">
            <h2>{props.contactName}</h2>                    {/* TODO fetch contanct name */}
                <h4>Dolor sit amet...</h4>   {/* TODO listen to event */} 
            </div>
            {!props.isHeader ?
            <div className="message-info">
                <div className="last-message-time">6:50</div> {/* TODO fetch last message sent time */}
                <Pinned className="pin-indicator"/> {/* TODO Toggle switch */}
            </div>
            :
            <></>    
        
        }

        </div>
    </>

}

export default Contact;