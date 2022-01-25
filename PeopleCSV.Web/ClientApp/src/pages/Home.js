import React, { useState, useEffect } from 'react';
import axios from 'axios';


const Home = () => {

    const [people, setPeople] = useState([]);
    const getPeople = async () => {
        const { data } = await axios.get('/api/people/getAll');
        setPeople(data);
    }

    const onDeleteClick = async () => {
        await axios.post('api/people/deleteAll');
        getPeople();
    }

    useEffect(() => {
        getPeople();
        
    }, []);

    return (
        <div className="container" style={{ marginTop: 60 }}>
            <button onClick={ onDeleteClick} className=" btn btn-danger btn-block">Delete All</button>
            <div className="row" style={{ marginTop: 20 }}>
                <div className="col-md-12">
                    <table className="table table-header table-bordered table-striped">
                        <thead>
                            <tr>
                                <th>Id</th>
                                <th>First Name</th>
                                <th>Last Name</th>
                                <th>Age</th>
                                <th>Address</th>
                                <th>Email</th>
                            </tr>
                        </thead>
                        <tbody>
                            {people.map(p=>
                                <tr>
                                    <td>{p.id }</td>
                                    <td>{p.firstName }</td>
                                    <td>{p.lastName }</td>
                                    <td>{p.age }</td>
                                    <td>{p.address }</td>
                                    <td>{p.email }</td>
                                </tr>)
                            }
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    )
}
export default Home