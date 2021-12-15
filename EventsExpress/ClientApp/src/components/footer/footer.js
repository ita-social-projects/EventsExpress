import React from 'react';
import './footer.css';
import { Link } from 'react-router-dom';

export const Footer = () => {
    const links = [
        { id: 0, name: 'Privacy', path: '/privacy' },
        { id: 1, name: 'Terms', path: '/terms' },
        { id: 2, name: 'About', path: '/about' },
        { id: 3, name: 'Contact us', path: '/contact' }
    ];

    const socialLinks = [
        { id: 0, icon: 'fab fa-facebook-f', link: '' },
        { id: 1, icon: 'fab fa-instagram', link: '' },
        { id: 2, icon: 'fab fa-youtube', link: '' }
    ];

    return (
        <footer className="custom-footer">
            <div className="custom-footer__links-to-pages">
                {links.map(link => (
                    <Link
                        key={link.id}
                        to={link.path}
                        className="nav-link custom-footer__link"
                    >
                        {link.name}
                    </Link>
                ))}
            </div>
            <div className="custom-footer__social-links">
                {socialLinks.map(link => (
                    <Link
                        key={link.id}
                        to={link.path}
                        className="nav-link custom-footer__social-link"
                    >
                        <i className={link.icon}/>
                    </Link>
                ))}
            </div>
        </footer>
    );
};
