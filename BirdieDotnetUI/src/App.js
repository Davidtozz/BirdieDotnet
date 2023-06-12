
import SideBar from './Components/Sidebar/SideBar.js';
import ContactsView from './Components/Contacts/ContactsView.js';
import ChatView from "./Components/Chat/ChatView.js";
import PurpleBand from "./Components/PurpleBand.js";
import './App.css';
import { useState } from 'react';
/* import { QueryClient } from '@tanstack/react-query' */

function App() {

	/* const queryClient = new QueryClient(); */
	const [selectedContact, setSelectedContact] = useState(null);

	const handleSelectedContact = (contact) => {
		console.table(contact)
		setSelectedContact(contact);
	}

  return (
    <>
      	<SideBar />
      	<main>
			<PurpleBand />
			<div className='content-wrapper'>
				<ContactsView onContactSelect={handleSelectedContact} />
				<ChatView selectedContact={selectedContact} />
			</div>
		</main>
    </>
  )
}



export default App;