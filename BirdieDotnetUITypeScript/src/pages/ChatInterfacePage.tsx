import React from "react";
/* import ApiService from "services/ApiService"; */
import styles from "./ChatInterfacePage.module.scss";
import Interface from "components/chat/Interface";

const ChatInterfacePage = () => {

    //TODO: Implement chat interface

    return (    

        <div className={styles.chatInterfaceWrapper}>
            <Interface />
        </div>
    )
}

export default ChatInterfacePage;