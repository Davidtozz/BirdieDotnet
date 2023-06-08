
import SideBar from './Components/Sidebar/SideBar.js';
import ContactsView from './Components/Contacts/ContactsView.js';
import ChatView from "./Components/Chat/ChatView.js";
import './App.css';

function App() {
  return (
    <>
      
      <SideBar />
      <main>
         {/* Also contains the Birdie Logo */}
        
       <div className='purple-band-wrapper'>
          <div className='purple-band'>
        </div></div>
         
       
        <div className='content-wrapper'>
          <ContactsView />
          <ChatView />
        </div>{/*  */}
            
        {/*  */}
       
      </main>
  {/*TODO rename chat-view, will display more other than chats */}
    </>
  
  )
}

export default App;
