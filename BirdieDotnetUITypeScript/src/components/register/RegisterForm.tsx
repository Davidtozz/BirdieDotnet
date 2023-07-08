import styles from "components/shared/Form.module.scss";
import { ReactComponent as BirdieLogo } from "../../assets/svg/BirdieLogo.svg";
import FormField from "../shared/FormField";
import FormSubmit from "components/shared/FormSubmit";
import ApiService from "services/ApiService";
import { useState } from "react";
import RegisterModel from "models/RegisterModel";
import Result from "types/Result";
import { AxiosError, AxiosResponse } from "axios";
import { Link, useNavigate } from "react-router-dom";

const RegisterForm = () => {
  const apiService = ApiService.instance;
  const [username, setUsername] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const navigateTo = useNavigate();

  const handleSubmit = async (event: React.FormEvent) => {
    event.preventDefault();

    const result: Result<AxiosResponse, AxiosError> = await apiService.registerUser({
      username,
      email,
      password,
    } as RegisterModel);
    
    console.log(Object.keys(result));

    if (result.success) {

      console.log("User registered successfully");
      navigateTo("/chat");
    } else {
      console.table({
        status: result.error.status,
        message: result.error.message,
      });
    }
  };

  return (
    <form className={styles.formWrapper} onSubmit={handleSubmit}>
      <div className={styles.logoWrapper}>
        <BirdieLogo className={styles.logo} />
        <h1 className={styles.logoHeading}>Birdie</h1>
      </div>

      <div className={styles.formContainer}>
        <FormField
          name="username"
          label="Username"
          placeholder="Enter your username"
          onChange={(e) => setUsername(e.target.value)}
          type="text"
        />

        <FormField
          name="email"
          label="Email"
          placeholder="Enter your email"
          onChange={(e) => setEmail(e.target.value)}
          type="email"
        />

        <FormField
          name="password"
          label="Password"
          placeholder="Enter your password"
          onChange={(e) => setPassword(e.target.value)}
          type="password"
        />
      </div>

      <div className={styles.formFooter}>
        <FormSubmit />
        <p>
          Already have an account?{" "}
          <span>
            <Link to="/login" className={styles.loginRedirect}>
              Login
            </Link>
          </span>
        </p>
      </div>
    </form>
  );
};

export default RegisterForm;
