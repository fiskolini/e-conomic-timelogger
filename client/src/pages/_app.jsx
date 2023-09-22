import 'tailwindcss/tailwind.css';
import Link from "next/link";
import {Toaster} from "react-hot-toast";

const App = ({Component, pageProps}) => {
    return (
        <>
            <header className="bg-gray-900 text-white flex items-center h-12 w-full">
                <div className="container mx-auto">
                    <Link href='/' className='navbar-brand'>
                        Timelogger
                    </Link>
                </div>
            </header>

            <Toaster position="top-right"
                     toastOptions={{
                         duration: 6 * 1000
                     }}
            />


            <main>
                <div className="container mx-auto">
                    <Component {...pageProps}/>
                </div>
            </main>
        </>
    );
}

export default App