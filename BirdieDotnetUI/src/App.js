
import SideBar from './Components/Sidebar/SideBar.js';
import ContactsView from './Components/Contacts/ContactsView.js';
import ChatView from "./Components/Chat/ChatView.js";
import PurpleBand from "./Components/PurpleBand.js";
import './App.css';
import { useState } from 'react';

function App() {


	const [selectedContact, setSelectedContact] = useState('');

	const handleSelectedContact = (contact) => {
		
		setSelectedContact(contact);
		console.log(contact)
	}

  return (
    <>
	{/* TODO Wrap elements inside a ChatInterface Page */}
      	<SideBar />
      	<main>
			<PurpleBand />
			<div className='chat-interface'>
				<ContactsView onContactSelect={handleSelectedContact} />
				<ChatView selectedContact={selectedContact} />
			</div>
		</main>
    </>
  )
}



export default App;