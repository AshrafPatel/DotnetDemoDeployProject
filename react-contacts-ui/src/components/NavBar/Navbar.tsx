import React from 'react';
import {Link} from 'react-router-dom';
import './NavBar.css';

const Navbar=()=>{
    return (    
        <div className='NavBar'>
            <div className='Contacts'>
                <Link to="/contacts">Contacts</Link>
            </div>
            <div className='NewContact'>
                <Link to="/contacts/new">New Contact</Link>
            </div>
        </div>
    )

}

export default Navbar;