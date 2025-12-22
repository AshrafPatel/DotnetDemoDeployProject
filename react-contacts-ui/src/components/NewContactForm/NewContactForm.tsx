import React, { Component } from 'react';
import './NewContactForm.css';
import process from "process";

interface NewContactFormState {
  contacts: any[];
  isLoading: boolean;
  form: boolean;
  name: string;
  email: string;
  [key: string]: any;
}

class NewContactForm extends Component<{}, NewContactFormState> {

    constructor(props:any){
      super(props);
      this.state = {
        contacts: [],
        isLoading: false,
        form: false,
        name: "",
        email: ""
      }
      this.handleClick = this.handleClick.bind(this);
      this.handleInputChange = this.handleInputChange.bind(this);
    }

    handleClick() {
      this.setState({
        form:true
      })
    }

    handleInputChange(event: React.ChangeEvent<HTMLInputElement>) {
      const value = event.target.value;
      const name = event.target.name;
  
      this.setState({
        [name]: value
      });
    }

    onSubmitHandler = (event: React.FormEvent) => {
      fetch(`${process.env.VITE_API_URL}/api/Contacts`, {
          method: "post",
          headers: {
              "Content-Type": "application/json"
          },
          body: JSON.stringify({
              email: this.state.email,
              name: this.state.name
          })
      })
    }

    componentDidMount() {
      try {
        fetch(`${process.env.REACT_APP_API_URL}/api/contacts`)
          .then(response => response.json())
          .then(contacts => this.setState({contacts}));
      } catch(e) {
        console.error(e);
        this.setState({isLoading: true})
      }
    }
  
    render() {
      return (
        <div className='NewContactForm'>
          <h2>New Contact</h2>
          <label htmlFor="email">Email</label>
          <input type="text" name="email" value={this.state.email} onChange={this.handleInputChange}/>
          <label htmlFor="name">Name</label>
          <input type="text" name="name" value={this.state.name} onChange={this.handleInputChange}/>
          <button type="submit" className="myButton" onClick={this.onSubmitHandler}>Create</button>
        </div>
      )
    }
}

export default NewContactForm;