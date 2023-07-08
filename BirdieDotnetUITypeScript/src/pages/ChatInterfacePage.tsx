import React, { useEffect } from "react";
import ApiService from "services/ApiService";

const ChatInterfacePage = () => {

    const apiService = ApiService.instance;

/* 
    const handleSubmit = async (event: React.FormEvent<HTMLButtonElement>) => {
        event.preventDefault();

        await 
    } */

    const handleGetUsers = async (event: React.FormEvent<HTMLButtonElement>) => {
        event.preventDefault();

        await apiService.getUsers();
    }

    const handleRefreshUsers = async (event: React.FormEvent<HTMLButtonElement>) => {
        event.preventDefault();

        await apiService.refreshToken();
    }

    


    useEffect(() => {

        apiService.getUsers();
    }, []);


    //TODO: Implement chat interface

    return (
    
    
        <div>
            <h1>Chat Interface</h1>
            <button onClick={handleGetUsers}>Get users</button>
            <button onClick={handleRefreshUsers}>Refresh users</button>
        </div>
        )
}

export default ChatInterfacePage;