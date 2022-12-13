import React, { useState } from 'react';
import swal from 'sweetalert';
import "./styles.css";
import "bootstrap/dist/css/bootstrap.min.css";


export default function Kontakty() {
    const [kontakty, setKontakty] = useState([]); // store posts in array
    const [kontaktyDetails, setKontaktyDetails] = useState([]);
    const [show, setShow] = useState(false);
    

    const handleDetailsClick = (selectedId) =>{
        getDetails(selectedId);
        setShow(true);
        console.log(show);
    }

    const handleDeleteClick = (Id) =>{
        deleteKontakty(Id);
        console.log("deleted" + Id)
    }
    
    function deleteKontakty(id){
        const url = "https://localhost:7188/api/kontakt/" + id

        fetch(url, {
            method: 'DELETE',
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem('token'),
            }

        })
            .then(response => response.json())
            .then(kontaktyDeleteFromServer => {
                console.log(kontaktyDeleteFromServer);
                
            })
            .then(getKontakty)
            .catch((error) => {
                console.log(error);
            });
    }

    function getDetails(idDetails){
        const url = "https://localhost:7188/api/kontakt/" + idDetails;

        fetch(url, {
            method: 'GET',
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem('token'),
            }

        })
            .then(response => response.json())
            .then(kontaktyDetailsFromServer => {
                console.log(kontaktyDetailsFromServer);
                setKontaktyDetails(kontaktyDetailsFromServer);

                
            })
            .catch((error) => {
                console.log(error);
            });
    }

    const hideModal = () => {
        setShow(false);
      };

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

    

    

    return (
        <div className='container'>
            <div className='row min-vh-100'>
                <div className='col d-flex flex-column justify-content-center align-items-center'>
                    <div>
                        <h1>Kontakty App</h1>

                        <div className='mt-5'>
                            <button onClick={getKontakty} className='btn btn-dark btn-lg w-100'>Get Data</button>
                            
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
                            <tr key={kontakt.id} >
                                <th scope="row">{kontakt.id}</th>
                                <td>{kontakt.imie}</td>
                                <td>{kontakt.nazwisko}</td>
                                <td>
                                <button className='btn btn-dark btn-lg mx-3 my-3' onClick={() => handleDetailsClick(kontakt.id)}>Details</button>
                                    <button className='btn btn-dark btn-lg mx-3 my-3' onClick={() => handleDeleteClick(kontakt.id)}>Delete</button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
                <button onClick={() => setKontakty([])} className='btn btn-dark btn-lg mx-3 my-3'>Schowaj</button>  
                {show && <Modal details={kontaktyDetails} handleClose={hideModal} />}
            </div>
        )
    }
}

const Modal = ({ handleClose, details }) => {
    console.log(details);
    return (
        
      <div className="modal display-block">
        <section className="modal-main">
            <table className="table">
              <thead>
                <tr>
                  <th scope="col">ID</th>
                  <th scope="col">Imie</th>
                  <th scope="col">Nazwisko</th>
                  <th scope="col">Email</th>
                  {details.kategoria && <th scope="col">Kategoria</th>}
                  {/* {details.kategoria.podkategoria && <th scope="col">Podkategoria</th>} */}
                  
                  <th scope="col">Telefon</th>
                  <th scope="col">Haslo</th>
                  <th scope="col">Data Urodzenia</th>
                </tr>
              </thead>
              <tbody>
                <tr>
                  <td>{details?.id}</td>
                  <td>{details?.imie}</td>
                  <td>{details?.nazwisko}</td>
                  <td>{details?.email}</td>
                  {details.kategoria && <td>details?.kategoria.kategoriaId</td>}
                  
                  {/* <td>{details?.kategoria.podkategoria}</td> */}
                  <td>{details?.telefon}</td>
                  <td>{details?.haslo}</td>
                  <td>{details?.dataurodzenia}</td>

                </tr>
              </tbody>
            </table>
          <button onClick={handleClose}>close</button>
        </section>
      </div>
    );
  };
