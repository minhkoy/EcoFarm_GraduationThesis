import { Link } from 'react-router-dom';
import ReactSVG from '../../assets/react.svg'
import { Home } from 'lucide-react';
import NotificationBell from './NotificationBell';

const Navbar = () => {
    //const navigate = useNavigate();
    return (
        <header className=' sticky top-0 z-50'>
            <nav className="bg-white border-gray-200 dark:bg-gray-900">
                <div className="flex flex-wrap justify-between items-center mx-auto max-w-screen-xl p-4">
                    <a href="/" className="flex items-center space-x-3 rtl:space-x-reverse">
                        <img src={ReactSVG} className="h-8" alt="Flowbite Logo" />
                        <span className="self-center text-green-500 text-2xl font-semibold whitespace-nowrap dark:text-white">Eco Farm</span>
                    </a>
                    <div className="flex items-center space-x-6 rtl:space-x-reverse">
                        <NotificationBell />
                        <Link to="/auth/login" className="text-sm text-blue-600 dark:text-blue-500 hover:underline">Đăng nhập</Link>
                    </div>
                </div>
            </nav>
            <nav className="bg-gray-50 dark:bg-gray-700">
                <div className="max-w-screen-xl px-4 py-3 mx-auto">
                    <div className="flex items-center">
                        <ul className="flex flex-row font-medium mt-0 space-x-8 rtl:space-x-reverse text-sm">
                            <li>
                                <a href="/" className="text-gray-900 dark:text-white hover:underline" aria-current="page">
                                    <Home className='inline' />    
                                    
                                </a>
                            </li>
                            <li>
                                <a href="#" className="text-gray-900 dark:text-white hover:underline">Company</a>
                            </li>
                            <li>
                                <a href="#" className="text-gray-900 dark:text-white hover:underline">Team</a>
                            </li>
                            <li>
                                <a href="#" className="text-gray-900 dark:text-white hover:underline">Features</a>
                            </li>
                        </ul>
                    </div>
                </div>
            </nav>
        </header>
    );
}

export default Navbar;