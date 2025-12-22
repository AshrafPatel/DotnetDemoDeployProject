import React, { useEffect, useState } from 'react'
import './ContactsPage.css'
import ContactRow from '../../components/ContactRow/ContactRow.tsx'
import process from "process";

export default function ContactsPage() {
    const [contacts, setContacts] = useState<any[]>([]);
    const [error, setError] = useState<string | null>(null);

    const fetchContacts = async () => {
        try {
            const response = await fetch(`${process.env.REACT_APP_API_URL}/api/contacts`);
            if(!response.ok) {
                throw new Error('Network response was not ok');
            }
            const data = await response.json();
            setContacts(data);
         } catch(error: any) {
            console.error(error);
            setError(error.message);
        }
    };

    useEffect(() => {
        fetchContacts();
    }, []);

    if (!contacts || contacts.length === 0) {
        return <div>No contacts available.</div>;
    }

    const getNewContacts = async (contactId: any) => {
        fetchContacts();
    };

    return (
        <table>
            <thead>
                <tr>
                    <th>Name</th>
                    <th>Email</th>
                    <th>State</th>
                    <th>Created At</th>
                    <th>Edit</th>
                    <th>Delete</th>
                </tr>
            </thead>
            <tbody>
                    {contacts.map((contact:any) => 
                        <tr>
                            <ContactRow onDelete={() =>getNewContacts(contact.id)} key={contact.id} contact={contact} />
                        </tr>
                    )}
            </tbody>
        </table>
  )
}