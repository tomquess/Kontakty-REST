import React, { useState } from 'react';
import {
    MDBInput,
    MDBBtn,
    MDBContainer
} from 'mdb-react-ui-kit';
import swal from 'sweetalert';



  async function loginUser(credentials) {
    return fetch('https://localhost:7188/Users/authenticate', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(credentials)
    })
    .then(data => data.json())
   }



function Signin() {
    const [username, setUserName] = useState();
    const [password, setPassword] = useState();

    const handleSubmit = async e => {
        e.preventDefault();

        const response = await loginUser({
            username,
            password
        });

        if ('token' in response) {
            swal("Success", {
                buttons: false,
                timer: 2000,
            })
                .then((value) => {
                    localStorage.setItem('token', response['token']);
                    localStorage.setItem('username', JSON.stringify(response['username']));
                });
        } else {
            swal("Failed");
        }
    }
    

    return (
        <MDBContainer>
            <form noValidate onSubmit={handleSubmit}>
                
                <MDBInput className='mb-4 mt-4' type='username' id='username' placeholder='Username' onChange={e => setUserName(e.target.value)} />
                <MDBInput className='mb-4 mt-4' type='password' id='password' placeholder='Password' onChange={e => setPassword(e.target.value)} />



                <MDBBtn type="submit" block>
                    Sign in
                </MDBBtn>
            </form>
        </MDBContainer>
    );

}

export default Signin;