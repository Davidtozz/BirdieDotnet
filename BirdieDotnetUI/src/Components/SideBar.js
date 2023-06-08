import {ReactComponent as ProfileIcon} from '../Profile icon.svg';
import {ReactComponent as SettingsIcon} from '../Settings icon.svg';
import {ReactComponent as BirdieLogoShort} from '../Birdie (short).svg';
import SideBarItem from './SideBarItem.js';

function SideBar(props) {
    return <>
    <nav className='navbar-container'>
    
        <div className="logo-container">
          <BirdieLogoShort alt='logo' id="logo"/>
        </div>
        <div className="sidebar-container">
            <SideBarItem icon={ProfileIcon} /> {/*TODO implement profile*/}
            <SideBarItem icon={SettingsIcon} /> {/*TODO implement settings*/}
        </div>
      </nav>
        </>
}

export default SideBar; 