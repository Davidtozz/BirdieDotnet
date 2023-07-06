import styles  from './RegisterForm.module.scss';
import {ReactComponent as BirdieLogo } from '../../assets/svg/BirdieLogo.svg';
import FormField from '../shared/FormField';
import FormSubmit from 'components/shared/FormSubmit';
import ApiService  from 'services/ApiService';
import { useEffect, useState } from 'react';
import RegisterModel from 'models/RegisterModel';


const RegisterForm = () => {

    const apiService = ApiService.instance;
    const [username, setUsername] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

    const handleChange = async (event: React.ChangeEvent<HTMLInputElement>) => {

        switch(event.target.name) {
            case 'username':
                await setUsername(event.target.value);
                break;
            case 'email':
                await setEmail(event.target.value);
                break;
            case 'password':
                await setPassword(event.target.value);
                break;
        }

        console.log(event.target.value);
    }

    //TODO Fix handleSubmit HTTP 415 error
    const handleSubmit = async (event: React.FormEvent) => {
        event.preventDefault();
    
        const userData: RegisterModel = {
          username,
          email,
          password
        };
    
        fetch(`http://localhost:5069/api/user/register`, {
            method: 'POST',
            headers: {
              'Content-Type': 'application/json',
              
            },
            body: JSON.stringify(userData)
          })
          .then(response => response.json())
          .catch(error => console.log("Couldn't register", error));
      };


    return (
        <form className={styles.formWrapper} action="http://localhost:5069/api/user/register" method="POST">
            <div className={styles.logoWrapper}>
                <BirdieLogo className={styles.logo} />
                <h1 className={styles.logoHeading}>Birdie</h1>
            </div>
            <div className={styles.formContainer}>
                <FormField 
                    name='username' 
                    label="Username" 
                    placeholder="Enter your username" 
                    onFieldChange={handleChange}
                    type="text" />

                <FormField 
                    name='email' 
                    label="Email" 
                    placeholder="Enter your email" 
                    onFieldChange={handleChange}
                    type="email"/>

                <FormField 
                    name='password' 
                    label="Password" 
                    placeholder="Enter your password" 
                    onFieldChange={handleChange}
                    type="password"/>
            </div>
            <div>
              <FormSubmit 
              onSubmit={handleSubmit} />    
            </div>
        </form>
    )
}


/* className={styles.formSubmitWrapper} */
export default RegisterForm;