import LoginForm from "components/login/LoginForm";
import styles from "./LoginPage.module.scss";
import React from "react";


const LoginPage = () => {

    return (
        <div className={styles.loginWrapper}>
            <LoginForm />
        </div>
    )
}

export default LoginPage;