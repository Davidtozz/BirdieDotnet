import {ReactComponent as Wave} from '../assets/svg/wave1.svg'; 
import './RegisterPage.scss';

const RegisterPage = () => {

    return (
        <div className='register-wrapper'>
            <h1>I'm the register page!</h1>
            <div className='wave-container'>
                <Wave className='wave-svg' preserveAspectRatio='none'/>
            </div>
            
        </div>
    )
}

export default RegisterPage;