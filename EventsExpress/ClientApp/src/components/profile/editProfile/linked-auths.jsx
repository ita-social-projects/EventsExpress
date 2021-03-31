import React from 'react';
import { externalLoginTypeEnum } from '../../../constants/externalLoginTypeEnum';

const LinkedAuths = props => {
    const { item } = props;
    return (
        <div>
            <div className="btn-group m-1" role="group" disabled>
                <a href="#" class="btn btn-secondary disabled" role="button" aria-disabled="true">
                    {renderType(item.type)}
                </a>
                <a href="#" class="btn btn-outline-secondary disabled" role="button" aria-disabled="true">
                    {item.email}
                </a>
            </div>
        </div>
    );
};

const renderType = type => {
    switch(type){
        case externalLoginTypeEnum.Google:
            return <i class="fab fa-goodreads"></i>;
        case externalLoginTypeEnum.Facebook:
            return <i class="fab fa-facebook-square"></i>
        case externalLoginTypeEnum.Twitter:
            return <i class="fab fa-twitter"></i>
        default :
            return <i class="fas fa-at"></i>
    }
}

export default LinkedAuths;
