import React from 'react'

const SystemInput = ({ placeholder, textValue = "", onChangeFunc, required }:
    { placeholder: string, textValue: string, onChangeFunc: (event: React.ChangeEvent<HTMLInputElement>) => void, required: boolean }) => {
    return (
        <div>
            <input placeholder={placeholder} value={textValue} onChange={onChangeFunc} required={required}></input>
        </div>
    )
}

export default SystemInput