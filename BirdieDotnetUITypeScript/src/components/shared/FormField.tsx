import FormFieldProps from 'types/FormFieldProps';
import styles from './FormField.module.scss';


const FormField = ({ label, ...otherProps }: FormFieldProps) => {

    return (
        <div className={styles.fieldWrapper}>
            <label className={styles.fieldLabel}>{label}</label>
            <input onChange={otherProps.onChange} className={styles.fieldInput} type={otherProps.type} placeholder={otherProps.placeholder} />
        </div>
    )
}

export default FormField;