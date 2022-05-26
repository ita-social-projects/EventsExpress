import React from 'react';
import Header from './../Header';
import LoginModal from "../login-modal"
import { Footer } from '../footer/footer';
import AlertContainer from '../../containers/alert';
import './main-layout.css'

const MainLayout = ({ children }) => {
    return (
        <div className="page-wrapper">
            <Header />
            <LoginModal />
            {children}
            <Footer />
            <AlertContainer />
        </div>
    );
};

export default MainLayout;
