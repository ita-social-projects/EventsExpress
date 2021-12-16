import React from "react";
import Header from "./../Header"

const MainLayout = ({children}) => {
    return(<>
    <Header />
    {children}
    </>)
};

export default MainLayout;
