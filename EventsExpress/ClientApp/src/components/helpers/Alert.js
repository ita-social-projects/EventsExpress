import React from "react";
import { render } from "react-dom";
import { positions, Provider } from "react-alert";
import AlertTemplate from "react-alert-template-basic";
import LoginWrapper from '../../containers/login';
import Register from '../../components/register/register';

const options = {
    timeout: 10000,
    position: positions.BOTTOM_CENTER
};

const App = () => (
    <Provider template={AlertTemplate} {...options}>
        <LoginWrapper />
        <Register/>
    </Provider>
);

render(<App />, document.getElementById("root"));
