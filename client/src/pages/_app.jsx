import 'tailwindcss/tailwind.css';
import Link from "next/link";

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


            <main>
                <div className="container mx-auto">
                    <Component {...pageProps}/>
                </div>
            </main>
        </>
    );
}

export default App