import React, {useState} from 'react';
import DatePicker from 'react-date-picker';
import { MDBBtn, MDBContainer, MDBInput, MDBRow, MDBCol, MDBBadge } from 'mdb-react-ui-kit';
import swal from 'sweetalert';

async function Post(item) {
  return fetch('https://localhost:7188/api/kontakt', {
    method: 'post',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + localStorage.getItem('token'), //token do autoryzacji
    },
    body: JSON.stringify(item)
  })
  .then(data => data.json())
 }

export default function PostForm() {
  const [imie, setImie] = useState();
  const [nazwisko, setNazwisko] = useState();
  const [email, setEmail] = useState();
  const [haslo, setHaslo] = useState();
  const [telefon, setTelefon] = useState();
  const [dataurodzenia, setDataurodzenia] = useState(new Date());
  const [NazwaKategorii, setKategoria] = useState();
  const [podkategoria, setPodkategoria] = useState();

  const handlePost = async e => {
    e.preventDefault();
    
    Post({
      "id": 0,
      imie,
      nazwisko,
      email,
      haslo,
      "kategoria": {
        "kategoriaId": 0,
          NazwaKategorii,
         podkategoria
      },
      telefon,
      dataurodzenia
      

  
    })
    .catch(function() {swal("fail", {       //komunikat jeśli post się nie powiedzie
      buttons: false,
      timer: 2000,
  })})
  }
  return (

    <MDBContainer className='mt-4'>
      <hr />
      <form noValidate onSubmit={handlePost}>
      <MDBRow>
        <MDBCol>
          <MDBInput id='Imie' label='Imie'  onChange={e => setImie(e.target.value)}/>
        </MDBCol>
        <MDBCol>
          <MDBInput id='Nazwisko' label='Nazwisko'  onChange={e => setNazwisko(e.target.value)}/>
        </MDBCol>
      </MDBRow>
      <MDBRow>
        <MDBCol>
          <MDBInput id='Email' label='Email'  onChange={e => setEmail(e.target.value)}/>
        </MDBCol>
        <MDBCol>
          <MDBInput id='Haslo' label='Haslo' onChange={e => setHaslo(e.target.value)}/>
        </MDBCol>
      </MDBRow>
      <MDBRow>
        <MDBCol>
          <MDBInput id='Telefon' label='Telefon' onChange={e => setTelefon(e.target.value)}/>
        </MDBCol>
        <MDBCol>
          <DatePicker name='label' id='Dataurodzenia' onChange={setDataurodzenia} value={dataurodzenia}/>
          <MDBBadge className='ms-4'>Data urodzenia</MDBBadge>
        </MDBCol>
      </MDBRow>
      <MDBRow>
        <MDBCol>
          <MDBInput id='Kategoria' label='Kategoria' onChange={e => setKategoria(e.target.value)}/>
        </MDBCol>
        <MDBCol>
          <MDBInput id='Podkategoria' label='Podkategoria' onChange={e => setPodkategoria(e.target.value)}/>
        </MDBCol>
      </MDBRow>
      
      <MDBBtn type="submit" block>
        Dodaj
      </MDBBtn>
      </form>
      <hr />
    </MDBContainer>

  );
}