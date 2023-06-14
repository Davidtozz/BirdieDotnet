import "../../styles/Components/MessageBubble.css"

const MessageBubble = ({incoming: isInComing, message}) => {


    return <>
    
            {isInComing ? 
            
            <div className="message-bubble-wrapper">
                <div className="triangle"></div>
                <div className="message-bubble">
                    <p> {message} </p>
                </div>
	        </div> 
            
            : 
            
            <div className="message-bubble-wrapper reversed">
                <div className="triangle reversed"></div>
                <div className="message-bubble reversed">
                    <p> {message} </p>
                </div>
            </div>}
    
    
    </>


}


export default MessageBubble;