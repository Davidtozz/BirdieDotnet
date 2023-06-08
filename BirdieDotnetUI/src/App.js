
import SideBar from './Components/SideBar.js';
import ContactsView from './Components/ContactsView.js';
import ChatView from "./Components/ChatView.js";
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
