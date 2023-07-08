import LoginForm from "components/login/LoginForm";
import styles from "../components/shared/FormPage.module.scss";
import React from "react";


const LoginPage = () => {

    return (
        <div className={styles.pageWrapper}>
            <LoginForm />
            <div className={styles.waveContainer}></div>
        </div>
    )
}

export default LoginPage;