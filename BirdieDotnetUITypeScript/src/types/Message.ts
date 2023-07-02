type Message =  {
    id: number;
    sender: string;
    receiver: string;
    content: string;
    timestamp: Date;
    isSentBySelf: boolean;
}

export default Message;