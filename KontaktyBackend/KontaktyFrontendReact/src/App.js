import React, { useState } from 'react';
import Signin from './Signin';

export default function App() {
    const [kontakty, setKontakty] = useState([]); // store posts in array

    function getKontakty() {
        const url = "https://localhost:7188/api/kontakt";

        fetch(url, {
            method: 'GET',
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem('token'),
            }

        })
            .then(response => response.json())
            .then(kontaktyFromServer => {
                console.log(kontaktyFromServer);
                setKontakty(kontaktyFromServer)
            })
            .catch((error) => {
                console.log(error);
            });
    }

    const token = localStorage.getItem('token');

    if (!token) {
        return <Signin />
    }

    return (
        <div className='container'>
            <div className='row min-vh-100'>
                <div className='col d-flex flex-column justify-content-center align-items-center'>
                    <div>
                        <h1>Kontakty App</h1>

                        <div className='mt-5'>
                            <button onClick={getKontakty} className='btn btn-dark btn-lg w-100'>Get Data</button>
                            <button onClick={() => { }} className='btn btn-dark btn-lg w-100'>Post</button>
                        </div>
                    </div>

                    {kontakty.length > 0 && renderKontaktyTable()}
                </div>
            </div>
        </div>
    );

    function renderKontaktyTable() {
        return (
            <div className='table-responsive mt-5'>
                <table className='table table-bordered border-dark'>
                    <thead>
                        <tr>
                            <th scope="col">ID</th>
                            <th scope="col">Imie</th>
                            <th scope="col">Nazwisko</th>
                            <th scope="col"></th>
                        </tr>
                    </thead>
                    <tbody>
                        {kontakty.map((kontakt) => (
                            <tr key={kontakt.id}>
                                <th scope="row">{kontakt.id}</th>
                                <td>{kontakt.imie}</td>
                                <td>{kontakt.nazwisko}</td>
                                <td>
                                    <button className='btn btn-dark btn-lg mx-3 my-3'>Update</button>
                                    <button className='btn btn-dark btn-lg mx-3 my-3'>Delete</button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
                <button onClick={() => setKontakty([])} className='btn btn-dark btn-lg mx-3 my-3'>Usun</button>
            </div>
        )
    }
}
