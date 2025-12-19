import "./ContactRow.css";
import React, { useEffect, useState } from 'react';
import { State } from "../../enums/State.tsx";

export default function ContactRow(props: any) { 
  const [action, setAction] = useState<string>("");
  const [name, setName] = useState<string>("");
  const [email, setEmail] = useState<string>("");
  const [state, setState] = useState<number>(0);
  const [createdAt, setCreatedAt] = useState<string>("");

  useEffect(() => {
    setName(props.contact?.name);
    setEmail(props.contact?.email);
    setState(props.contact?.state);
    setCreatedAt(props.contact?.createdAt);
  }, [props.contact]);

  const handleDelete = (e: any) => {
    fetch('https://localhost:5000/api/contacts/'+ props.contact.id,
      { 
        method: 'DELETE', 
        headers: {
        "Content-Type": "application/json"
        }
      }
    )
    setAction("get")
  };

  const handleEdit = (e: any) => {
    if (!process.env.NODE_ENV || process.env.NODE_ENV === 'development') {
      fetch('https://localhost:5000/api/contacts/'+ props.contact.id,
      { 
        method: 'PUT', 
        headers: {
        "Content-Type": "application/json"
        },
        body: JSON.stringify({
          email: email,
          name: name,
          state: state,
          createdAt: createdAt
        })
      })
    } else {
      fetch('https://energetic-enthusiasm-production.up.railway.app/api/contacts/'+ props.contact.id,
      {
        method: 'PUT', 
        headers: {
        "Content-Type": "application/json"
        },
        body: JSON.stringify({
          email: email,
          name: name,
          state: state,
          createdAt: createdAt
        })
      })
    }
    setAction("get")
  };

  return (
    <>
      <td>
        <input type="text" name="name" value={name} disabled={action !== "edit"} onChange={e => setName(e.target.value)}/>
      </td>
      <td>
        <input type="text" name="email" value={email} disabled={action !== "edit"} onChange={e => setEmail(e.target.value)}/>
      </td>
      <td>
        <select value={state} onChange={e => setState(Number(e.target.value))} disabled={action !== "edit"}>
          {Object.keys(State)
            .filter(key => !isNaN(Number(key)))
            .map((key) => (
            <option key={key} value={key}>
              {State[key as keyof typeof State]}
            </option>
          ))}
        </select>
      </td>
      <td>
        <p>{props.contact?.createdAt}</p>
      </td>
      <td>
           {action === "edit" ?           
          <button onClick={handleEdit} className="button">Submit</button> :
          <button onClick={() => setAction("edit")} className="button">Edit</button>}
      </td>
      <td>
          {action === "delete" ? 
            <button onClick={handleDelete} className="button deletebtn">Confirm Delete</button> : 
            <button onClick={() => setAction("delete")} className="button deletebtn">Delete</button>}
      </td>
    </>
  )
}