import {ReactComponent as Pinned} from '../pinned.svg';

const Contact = ({picurl: url, contactname: name, header: isHeader}) => {

    return <>
        <div className='contact-card'>
            
            <div className="propic-wrapper">
                <img className='propic' src={url} alt='pic'/>
            </div>
            
            <div className="contact-details">
            <h2>{name}</h2>                    {/* TODO fetch contanct name */}
                <h4>Dolor sit amet...</h4>   {/* TODO listen to event */} 
            </div>
            {!isHeader ?
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