import React from 'react';
import Header from './../Header';
import { Footer } from '../footer/footer';
import AlertContainer from '../../containers/alert';
import './main-layout.css'

const MainLayout = ({ children }) => {
    return (
        <div className="page-wrapper">
            <Header />
            {children}
            <Footer />
            <AlertContainer />
        </div>
    );
};

export default MainLayout;
