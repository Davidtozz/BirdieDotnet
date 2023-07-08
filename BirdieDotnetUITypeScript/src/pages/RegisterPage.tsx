import RegisterForm from 'components/register/RegisterForm';
import styles from "components/shared/FormPage.module.scss";


const RegisterPage = () => {
    
    return (
        <div className={styles.pageWrapper}>
            <RegisterForm />
            <div className={styles.waveContainer}></div>
        </div>
    )
}

export default RegisterPage;