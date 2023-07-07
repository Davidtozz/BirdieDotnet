import { useState } from 'react';
import styles from './FormSubmit.module.scss';



const FormSubmit = (props: React.ButtonHTMLAttributes<HTMLButtonElement> ) => {

    //TODO - show loading spinner during form submit
    const [showSpinner, setShowSpinner] = useState(false);

    return (
        <button 
        onClick={() => setShowSpinner(showSpinner => !showSpinner)}
        type="submit" className={styles.formSubmit}>
            <span>Submit</span>
            {showSpinner && <div className={styles.spinner}></div>}
        </button>
    )
}

export default FormSubmit;