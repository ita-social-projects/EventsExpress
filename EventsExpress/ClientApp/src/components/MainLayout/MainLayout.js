import React from 'react';
import Header from './../Header';
import { Footer } from '../footer/footer';
import './main-layout.css'

const MainLayout = ({ children }) => {
    return (
        <div className="page-wrapper">
            <Header />
            {children}
            <Footer />
        </div>
    );
};

export default MainLayout;
