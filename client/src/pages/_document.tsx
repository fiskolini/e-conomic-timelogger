import Document, {Html, Head, Main, NextScript, DocumentContext} from "next/document";

class TimeLoggerDocument extends Document {
    static async getInitialProps(ctx: DocumentContext) {
        const initialProps = await Document.getInitialProps(ctx);
        return {...initialProps};
    }

    render() {
        return (
            <Html>
                <Head>
                    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Roboto:300,400,500"/>
                    <link rel="stylesheet" href="https://fonts.googleapis.com/icon?family=Material+Icons"/>
                </Head>

                <body>
                <Main/>
                <NextScript/>
                </body>
            </Html>
        );
    }
}

export default TimeLoggerDocument;