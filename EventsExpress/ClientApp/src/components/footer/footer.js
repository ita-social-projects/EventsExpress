import React from 'react';
import './footer.css';
import { Link } from 'react-router-dom';

export const Footer = () => {
    const links = [
        { id: 0, name: 'Privacy', path: '/privacy' },
        { id: 1, name: 'Terms', path: '/terms' },
        { id: 2, name: 'About', path: '/about' },
        { id: 3, name: 'Contact us', path: '/contactAdmin' }
    ];

    const socialLinks = [
        { id: 0, icon: 'fab fa-facebook-f', path: '' },
        { id: 1, icon: 'fab fa-instagram', path: '' },
        { id: 2, icon: 'fab fa-youtube', path: '' }
    ];

    return (
        <footer className="custom-footer">
            <div className="links-to-pages">
                {links.map(link => (
                    <Link
                        key={link.id}
                        to={link.path}
                        className="nav-link link"
                    >
                        <i className="link-circle fas fa-circle"/>
                        {link.name}
                    </Link>
                ))}
            </div>
            <div className="social-links">
                {socialLinks.map(link => (
                    <Link
                        key={link.id}
                        to={link.path}
                        className="nav-link social-link"
                    >
                        <i className={link.icon}/>
                    </Link>
                ))}
            </div>
        </footer>
    );
};
