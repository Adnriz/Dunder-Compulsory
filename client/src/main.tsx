import ReactDOM from 'react-dom/client'
import App from "./App.tsx";
import './styles.css';
import 'jotai-devtools/styles.css';
import {BrowserRouter} from "react-router-dom";

ReactDOM.createRoot(document.getElementById('root')!).render(
    <BrowserRouter>
        <App/>
    </BrowserRouter>
)