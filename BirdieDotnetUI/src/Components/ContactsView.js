import Contact from './Contact';
import { useState, useEffect } from 'react'
/* import JohnDoe from '../img5 1.png';
 */
function ContactsView (props) {

    const [contacts, setContacts] = useState([]);


    useEffect(() => {
        fetchContacts();
    })

    const fetchContacts = () => {
        
        fetch("http://localhost:5069/api/user")
        .then(response => response.json())
        .then(data => setContacts(data))
        .catch(err => console.log("Error fetching contacts", err))
    }

    return <>
        <div className='list-view'>
            <div className='search-card'>
                <input type='text' placeholder='Search' className='search-field' />
            </div>


            {
                contacts.map((contact, _) => 
                    
                    
                     <Contact contactname={contact["Name"]} header={false} picurl={'https://source.unsplash.com/random/500x500/?profile,avatar'}/>
                )
            }






        </div>
    
    </>


}

export default ContactsView; 