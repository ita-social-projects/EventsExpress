import React, { createContext, useState } from 'react';
import { connect } from 'react-redux';
import { getFormValues } from 'redux-form';
import Stepper from '../stepper/Stepper';
import SuccessPage from './steps/success/SuccessPage';
import ConfirmationPage from './steps/confirm/ConfirmationPage';
import CompleteProfileForm from './steps/complete-profile/CompleteProfileForm';
import UserPreferencesForm from './steps/preferences/UserPreferencesForm';
import ChooseActivities from './steps/activities/ChooseActivities';
import registerComplete from '../../actions/register/register-complete-action';
import './RegistrationForm.css';

const stepsArray = [
    'Register',
    'Complete Profile',
    'Choose Activities',
    'Preferences',
    'Confirm',
];

export const RegisterStepContext = createContext();

const RegistrationForm = ({ formValues, registerComplete }) => {
    const [currentStep, setCurrentStep] = useState(2);

    const adjustStep = value => {
        setCurrentStep(currentStep + value);
    };

    const saveProfile = () => {
        registerComplete(formValues);
        setCurrentStep(6);
    };

    const renderStep = () => {
        switch (currentStep) {
            case 2: return <CompleteProfileForm onSubmit={saveProfile} />;
            case 3: return <ChooseActivities onSubmit={saveProfile} />;
            case 4: return <UserPreferencesForm onSubmit={saveProfile} />;
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

const mapStateToProps = state => ({
    formValues: getFormValues('registrationForm')(state),
});

const mapDispatchToProps = dispatch => ({
    registerComplete: data => dispatch(registerComplete(data)),
});

export default connect(mapStateToProps, mapDispatchToProps)(RegistrationForm);
