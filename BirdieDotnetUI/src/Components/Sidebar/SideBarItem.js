import "../../styles/Components/SideBarItem.css"

const SideBarItem = ({icon: Icon}) => {
    return <div className='sidebar-item-container'>
        <Icon />
    </div>
}

export  default SideBarItem;