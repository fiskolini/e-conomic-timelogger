import 'tailwindcss/tailwind.css'

const App = ({Component, pageProps}) => {
    return (
        <>
            <header className="bg-gray-900 text-white flex items-center h-12 w-full">
                <div className="container mx-auto">
                    <a className="navbar-brand" href="/">
                        Timelogger
                    </a>
                </div>
            </header>

            <main>
                <div className="container mx-auto">
                    <Component/>
                </div>
            </main>
        </>
    );
}

export default App