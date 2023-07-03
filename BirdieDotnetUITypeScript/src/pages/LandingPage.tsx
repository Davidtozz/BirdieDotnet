import React from "react";
import { Link } from "react-router-dom";


const LandingPage = () => {

    //TODO: Implement chat interface

    return (<>
        <h1>I'm the landing page!</h1>
        <Link to="/login">Go to Login</Link>
        </>
    )
}

export default LandingPage;