import React, { useState } from 'react'
import He from '../../components/locales/He'
import { iCustomer } from '../../interfaces/Ilogin'
import SystemInput from '../../components/ui/systemInput/SystemInput'
import SystemButton from '../../components/ui/systemButton/SystemButton'
import { CustomersList, getVersion, verificationCustomer } from '../../api/api'
import { getLocalVersion } from '../../services/LocalStorage'
import { loadFromLocalStorage, setCustomers } from '../../redux/slices/customerSlice'
import { useDispatch } from 'react-redux'
import { useNavigate } from 'react-router-dom'


const LoginPage = () => {


    const [userValues, setUserValues] = useState<iCustomer>({
        name: "",
        lastName: "",
        email: "",
        password: ""
    })

    const dispatch = useDispatch();
    const navigate = useNavigate();

    const onClickEnter = async () => {
        console.log("userValues", userValues)
        await verifiyCustomer()
        // if (validation())

    }

    const validation = () => {
        return userValues.email.length > 0 && userValues.name.length > 0 && userValues.lastName.length > 0 && userValues.password && userValues.password.length > 0
    }


    const verifiyCustomer = async () => {
        const result = await verificationCustomer(userValues)
        if (result == "access granted") {
            const customerVersionNumber = getLocalVersion();
            const updatedCustomersVersionNumber = await getVersion()
            console.log("customerVersionNumber:", customerVersionNumber);
            console.log("updatedCustomersVersionNumber:", updatedCustomersVersionNumber);
            if (customerVersionNumber == null || customerVersionNumber != updatedCustomersVersionNumber) {
                const NewcustomersList = await CustomersList()
                console.log("NewcustomersList", NewcustomersList);
                localStorage.setItem('data', JSON.stringify(NewcustomersList));
                const updatedData = {
                    version: NewcustomersList.version,
                    customers: NewcustomersList.customers
                };
                console.log("updatedData:", updatedData);

                dispatch(setCustomers(updatedData));
            }
            else {
                console.log('====================================');
                console.log("have the same version");
                console.log('====================================');
                dispatch(loadFromLocalStorage());
            }
            navigate('/home');
        }
        else {
            // filed error popup 
            alert('Access denied');


        }
    }

    return (
        <div>
            <div className='inpputsLoginContainer'>
                <SystemInput placeholder={He.email} textValue={userValues?.email ?? ""} onChangeFunc={(event) => setUserValues(prev => ({ ...prev, email: event.target.value }))} required={true} />
                <SystemInput placeholder={He.first_name} textValue={userValues?.name ?? ""} onChangeFunc={(event) => setUserValues(prev => ({ ...prev, name: event.target.value }))} required={true} />
                <SystemInput placeholder={He.last_name} textValue={userValues?.lastName ?? ""} onChangeFunc={(event) => setUserValues(prev => ({ ...prev, lastName: event.target.value }))} required={true} />
                <SystemInput placeholder={He.password} textValue={userValues?.password ?? ""} onChangeFunc={(event) => setUserValues(prev => ({ ...prev, password: event.target.value }))} required={true} />
            </div>
            <div>
                <SystemButton onClickFunc={onClickEnter} buttonText={He.enter} />
            </div>
        </div>
    )
}

export default LoginPage

function dispatch(arg0: { payload: import("../../interfaces/Ilogin").iCustomerData; type: "customer/setCustomers" }) {
    throw new Error('Function not implemented.')
}
