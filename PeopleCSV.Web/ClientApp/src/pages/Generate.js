import React, { useState } from 'react';
import axios from 'axios';


const Generate = () => {
    const [amount, setAmount] = useState(0);
    

    const onGenerateClick = async () => {
        window.location =(`/api/people/generatePeople?amount=${amount}`);
    }

    return (
        <div className="d-flex w-100 justify-content-center align-self-center">
            <div className="row">
                <input type="text" className="form-control-lg" placeholder="Amount" onChange={e => setAmount(e.target.value)}/>
                </div>
            <div className="row">
                <div className="col-md-4"><button className="btn btn-primary btn-lg" onClick={onGenerateClick}>Generate</button>
                </div>
                </div>
            </div>
      
        )
}
export default Generate