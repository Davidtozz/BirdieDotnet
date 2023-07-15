import styles from './FormField.module.scss';

const FormField = ({ label, ...otherProps }: FormFieldProps) => {

    return (
        <div className={styles.fieldWrapper}>
            <label className={styles.fieldLabel}>{label}</label>
            <input className={styles.fieldInput} {...otherProps} />
        </div>
    )
}

type FormFieldProps = {
    label: string;
} & React.InputHTMLAttributes<HTMLInputElement>;


export default FormField;