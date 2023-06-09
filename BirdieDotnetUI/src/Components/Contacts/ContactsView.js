import Contact from './Contact';
import { useState, useEffect } from 'react'
/* import JohnDoe from '../img5 1.png';
 */
function ContactsView (props) {

    const [contacts, setContacts] = useState([]);


    useEffect(() => {

        //? Due to developer mode, Fetch is called twice in useEffect(). 
        //? So we abort the second concurrent request:
        const abortController = new AbortController();

        const url = "http://localhost:5069/api/user";

        const fetchData = async () => {
            try {
                const response = await fetch(url, {method: 'GET', 
                signal: abortController.signal});
                const json = await response.json();
                setContacts(json)
            } catch (error) {
                //console.log("error",error);
            }
        }

        fetchData();


        return () => abortController.abort();
    },[])


    const selectedContact = (contact) => {
        props.onContactSelect(contact);
    };

    return <>
        <div className='list-view'>
            <div className='search-card'>
                <input type='text' placeholder='Search' className='search-field' />
            </div>
            {
                contacts.map((contact, index) => 
                    <Contact 
                    onClick={() => selectedContact(contact.Name)}
                    contactName={contact["Name"]} 
                    header={false} 
                    key={index}
                    picUrl={`https://source.unsplash.com/random/500x500/?profile$${index}`}/>
                )
            }
        </div>
    </>
}

export default ContactsView; 