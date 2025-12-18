import React, { Component } from 'react';
import ContactCard from './ContactCard';


class Background extends Component {

    constructor(props){
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

    handleInputChange(event) {
      const value = event.target.value;
      const name = event.target.name;
  
      this.setState({
        [name]: value
      });
    }

    onSubmitHandler = (event) => {
      fetch("https://localhost:5000/api/Contacts", {
          method: "post",
          headers: {
              "Content-Type": "application/json"
          },
          body: JSON.stringify({
              email: this.state.email,
              name: this.state.name
          })
      })
      fetch('https://localhost:5000/api/Contacts')
          .then(response => response.json())
          .then(contacts => this.setState({contacts}));
    }

    componentDidMount() {
      try {
        fetch('https://localhost:5000/api/contacts')
          .then(response => response.json())
          .then(contacts => this.setState({contacts}));
      } catch(e) {
        console.error(e);
        this.setState({isLoading: true})
      }
    }
  
    render() {
      return (
        <div>
          <div style={{display: "flex"}}>
            {this.state.isLoading ? "Error" : this.state.contacts.map((contact, i) => <ContactCard contact={contact} key={i} />)}
          </div>
          {this.state.form === false ? 
          <button onClick={this.handleClick} className="myButton">Create</button> : 
          <div>
            <label name="email">Email</label>
            <input type="text" name="email" value={this.state.email} onChange={this.handleInputChange}/>
            <label name="name">Name</label>
            <input type="text" name="name" value={this.state.name} onChange={this.handleInputChange}/>
            <br/>
            <button type="submit" className="myButton" onClick={this.onSubmitHandler}>Create</button>
          </div>}
        </div>
      )
    }
}

export default Background;