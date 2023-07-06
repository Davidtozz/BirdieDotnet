import RegisterForm from 'components/register/RegisterForm';
import styles from './RegisterPage.module.scss';

const RegisterPage = () => {
    
    return (

            <div className={styles.registerWrapper}>
                <RegisterForm />
            </div>
    )
}

export default RegisterPage;