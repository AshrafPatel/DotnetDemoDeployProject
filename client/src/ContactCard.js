import "./Contact.css";
import React, { useState } from 'react';

function ContactCard(props) { 
  const [action, setAction] = useState(0);
  const [name, setName] = useState(0);
  const [email, setEmail] = useState(0);

  const handleDelete = (e) => {
    fetch('https://localhost:5000/api/Contacts/'+ props.contact.id,
      { 
        method: 'DELETE', 
        headers: {
        "Content-Type": "application/json"
        }
      }
    )
    setAction("get")
  };

  const handleEdit = (e) => {
    fetch('https://localhost:5000/api/Contacts/'+ props.contact.id,
      { 
        method: 'PUT', 
        headers: {
        "Content-Type": "application/json"
        },
        body: JSON.stringify({
          email: email,
          name: name
        })
      }
    )
    setAction("get")
  };
 

  return (
    <div className="card">
      <img src={"https://mjsolpac.com/wp-content/uploads/2017/03/person-placeholder.jpg"} alt="Avatar" style={{width:100 + "%"}}/>
      <div className="container">
        <h4><b>{props.contact.name}</b></h4>
        <p>{props.contact.email}</p>
        <div className="buttonContain">
          {action === "edit" ? 
          <div>
            <label name="email">Email</label>
            <input type="text" name="email" value={email} onChange={e => setEmail(e.target.value)}/>
            <label name="name">Name</label>
            <input type="text" name="name" value={name} onChange={e => setName(e.target.value)}/>
            <button onClick={handleEdit} className="button">Submit</button>
          </div> : 
          <button onClick={() => setAction("edit")} className="button">Edit</button>}
          {action === "delete" ? <button onClick={handleDelete} className="button deletebtn">Delete Confirm</button> : <button onClick={() => setAction("delete")} className="button deletebtn">Delete</button>}
        </div>
      </div>
    </div>
  );
}

export default ContactCard;