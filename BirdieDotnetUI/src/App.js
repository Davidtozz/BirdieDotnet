
import SideBar from './Components/Sidebar/SideBar.js';
import ContactsView from './Components/Contacts/ContactsView.js';
import ChatView from "./Components/Chat/ChatView.js";
import './App.css';
import { useState } from 'react';

function App() {

	const [selectedContact, setSelectedContact] = useState(null);

	const handleSelectedContact = (contact) => {
		console.table(contact)
		setSelectedContact(contact);
	}

  return (
    <>
      <SideBar />
      <main>
			<div className='purple-band-wrapper'>
				<div className='purple-band' />
			</div>
			<div className='content-wrapper'>
				<ContactsView onContactSelect={handleSelectedContact} />
				<ChatView selectedContact={selectedContact} />
			</div>
		</main>
    </>
  )
}

export default App;
