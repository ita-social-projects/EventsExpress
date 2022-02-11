import React, { createContext, useState } from 'react';
import PropTypes from 'prop-types';
import Stepper from '../stepper/Stepper';
import ConfirmationPage from './steps/confirm/ConfirmationPage';
import CompleteProfileForm from './steps/complete-profile/CompleteProfileForm';
import UserPreferencesForm from './steps/preferences/UserPreferencesForm';
import ChooseActivities from './steps/activities/ChooseActivities';
import SuccessPage from './steps/success/SuccessPage';
import './RegistrationForm.css';

const stepsArray = [
    'Register',
    'Complete Profile',
    'Choose Activities',
    'Preferences',
    'Confirm',
];

export const RegisterStepContext = createContext();

const RegistrationForm = () => {
    const [currentStep, setCurrentStep] = useState(2);

    const adjustStep = value => {
        setCurrentStep(currentStep + value);
    };

    const renderStep = () => {
        switch (currentStep) {
            case 2: return <CompleteProfileForm />;
            case 3: return <ChooseActivities />;
            case 4: return <UserPreferencesForm />;
            case 5: return <ConfirmationPage />;
            case 6: return <SuccessPage />;
            default: return null;
        }
    };

    return (
        <div className="stepper-container-horizontal">
            <Stepper
                currentStepNumber={currentStep - 1}
                steps={stepsArray}
                stepColor="#ff9900"
            />
            <div className="buttons-container">
                <RegisterStepContext.Provider
                    value={{
                        goBack: () => adjustStep(-1),
                        goToNext: () => adjustStep(1),
                    }}
                >
                    {renderStep()}
                </RegisterStepContext.Provider>
            </div>
        </div>
    );
};

RegistrationForm.propTypes = {
    onSubmit: PropTypes.func.isRequired,
};

export default RegistrationForm;
