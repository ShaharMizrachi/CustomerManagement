import React, { useState } from 'react'
import He from '../../components/locales/He'
import { iCustomer } from '../../interfaces/Ilogin'
import SystemInput from '../../components/ui/systemInput/SystemInput'
import SystemButton from '../../components/ui/systemButton/SystemButton'
import { verificationCustomer } from '../../api/api'

const LoginPage = () => {


    const [userValues, setUserValues] = useState<iCustomer>({
        Name: "",
        LastName: "",
        Email: "",
        Password: ""
    })



    const onClickEnter = async () => {
        console.log("userValues", userValues)
        await verifiyCustomer()
        // if (validation())

    }

    const validation = () => {
        return userValues.Email.length > 0 && userValues.Name.length > 0 && userValues.LastName.length > 0 && userValues.Password && userValues.Password.length > 0
    }


    const verifiyCustomer = async () => {
        const result = await verificationCustomer(userValues)
        if (result == "access granted") {
            // contniue to next page 

        }
        else {
            // filed error 

        }
    }

    return (
        <div>
            <div className='inpputsLoginContainer'>
                <SystemInput placeholder={He.email} textValue={userValues?.Email ?? ""} onChangeFunc={(event) => setUserValues(prev => ({ ...prev, Email: event.target.value }))} required={true} />
                <SystemInput placeholder={He.first_name} textValue={userValues?.Name ?? ""} onChangeFunc={(event) => setUserValues(prev => ({ ...prev, Name: event.target.value }))} required={true} />
                <SystemInput placeholder={He.last_name} textValue={userValues?.LastName ?? ""} onChangeFunc={(event) => setUserValues(prev => ({ ...prev, LastName: event.target.value }))} required={true} />
                <SystemInput placeholder={He.password} textValue={userValues?.Password ?? ""} onChangeFunc={(event) => setUserValues(prev => ({ ...prev, Password: event.target.value }))} required={true} />
            </div>
            <div>
                <SystemButton onClickFunc={onClickEnter} buttonText={He.enter} />
            </div>
        </div>
    )
}

export default LoginPage