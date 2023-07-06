import styles from './FormSubmit.module.scss';

const FormSubmit = (props: React.ButtonHTMLAttributes<HTMLButtonElement> ) => {
    return (
        <button type="submit" className={styles.formSubmit}>Submit</button>
    )
}

export default FormSubmit;