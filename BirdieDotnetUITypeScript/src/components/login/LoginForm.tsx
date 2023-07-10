import { Link, useNavigate } from 'react-router-dom';
import styles from 'components/shared/Form.module.scss';
import { useState } from 'react';
import ApiService from 'services/ApiService';
import { ReactComponent as BirdieLogo } from '../../assets/svg/BirdieLogo.svg';
import FormField from 'components/shared/FormField';
import FormSubmit from 'components/shared/FormSubmit';
import LoginModel from 'models/LoginModel';
import { AxiosError, AxiosResponse } from 'axios';
import Result from 'types/Result';


//TODO auth page related components 
const LoginForm = () => {

    const apiService = ApiService.instance;
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const navigateTo = useNavigate();

    const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
      event.preventDefault();
      
      const response: Result<AxiosResponse, AxiosError> = await apiService.loginUser({
        username,
        password,     
      } as LoginModel)
      
      if (response.success && response.value.status === 200) {
        console.log("User logged successfully");
        navigateTo("/chat");
      } else if(!response.success) {            
        console.table({ status: response.error?.status, reason: response.error?.message });
      }
    }

    return (
        <form className={styles.formWrapper} onSubmit={handleSubmit}>
            <div className={styles.logoWrapper}>
                <BirdieLogo className={styles.logo} />
                <h1 className={styles.logoHeading}>Birdie</h1>
            </div>
            <div className={styles.formContainer}>
                <FormField 
                    name='username' 
                    label="Username" 
                    placeholder="Enter your username" 
                    onChange={(e) => setUsername(e.target.value)}
                    type="text" />

                <FormField 
                    name='password' 
                    label="Password" 
                    placeholder="Enter your password" 
                    onChange={(e) => setPassword(e.target.value)}
                    type="password"/>
            </div>
            <div className={styles.formFooter}>
              <FormSubmit />    
              <p>Don't have an account? <span>
                <Link to="/register" className={styles.loginRedirect}>Register</Link>
                </span>
              </p>
            </div>
        </form>
    )
}

export default LoginForm;