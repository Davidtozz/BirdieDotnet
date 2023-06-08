import {ReactComponent as ProfileIcon} from '../static/svg/SideBarItems/Profile icon.svg';
import {ReactComponent as SettingsIcon} from '../static/svg/SideBarItems/Settings icon.svg';
import {ReactComponent as BirdieLogoShort} from '../static/svg/Birdie.svg';
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