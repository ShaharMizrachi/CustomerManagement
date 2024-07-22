import React from 'react'

const SystemButton = ({ onClickFunc, buttonText }: { onClickFunc: () => void, buttonText: string }) => {
    return (
        <div>
            <button onClick={onClickFunc} >{buttonText}</button>
        </div>
    )
}

export default SystemButton