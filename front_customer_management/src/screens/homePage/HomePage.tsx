// src/screens/homePage/HomePage.tsx
import React, { useEffect, useState } from 'react';
import { useSelector } from 'react-redux';
import { RootState } from '../../redux/store';
import { iCustomer } from '../../interfaces/Ilogin';
import SystemInput from '../../components/ui/systemInput/SystemInput';

const HomePage = () => {
    const customers = useSelector((state: RootState) => state.customer.customers);
    const [filter, setFilter] = useState('');

    const filteredCustomers = filter
        ? customers.filter(customer =>
            customer.email.toLowerCase().includes(filter.toLowerCase())
        )
        : customers;


    useEffect(() => {
        console.log("customers:", customers);
    }, [customers]);


    return (
        <div>
            <SystemInput
                placeholder="Filter by Email"
                textValue={filter}
                onChangeFunc={(event) => setFilter(event.target.value)} required={false} />
            <table>
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>LastName</th>
                        <th>Email</th>
                        <th>Date</th>
                        <th>Phone</th>
                    </tr>
                </thead>
                <tbody>
                    {filteredCustomers.map((customer, index) => (
                        <tr key={index}>
                            <td>{customer.name}</td>
                            <td>{customer.lastName}</td>
                            <td>{customer.email}</td>
                            <td>{customer.date}</td>
                            <td>{customer.phone}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default HomePage;
