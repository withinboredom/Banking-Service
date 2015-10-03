using System;
using System.Collections.Generic;
using BankLibrary.DataConstructs;

namespace BankLibrary.Banks
{
    /// <summary>
    /// Drives a bank interface
    /// </summary>
    public class BankDriver
    {
        public BankDriver(Bank bank)
        {
            bank.RequirePin += BankOnRequirePin;
            bank.RequirePassword += BankOnRequirePassword;
            bank.RequireQuestion += BankOnRequireQuestion;
            bank.RequireUserName += BankOnRequireUserName;
            bank.LoginComplete += BankOnLoginComplete;
            bank.LoginFailed += BankOnLoginFailed;

            Steps = new Dictionary<Guid, StepDefinition>();

            this._bank = bank;
        }

        /// <summary>
        /// Called when a login fails
        /// </summary>
        private void BankOnLoginFailed()
        {
            Steps[_currentStep].Successful = false;
            CreateNextStep("Failed", "Failed to login", false, _currentStep);
        }

        private void BankOnLoginComplete()
        {
            CreateNextStep("Success", "Logged in", true, _currentStep);
        }

        public Dictionary<Guid, StepDefinition> Steps { get; private set; }
        private Guid _currentStep;
        private readonly Bank _bank;

        public Guid Login(Credentials creds)
        {
            var step = new StepDefinition();
            this._currentStep = Guid.NewGuid();
            step.Id = _currentStep;
            Steps.Add(this._currentStep, step);

            _bank.Login(creds);

            return this._currentStep;
        }

        public StepDefinition DoStep(Guid step, Credentials creds)
        {
            var successful = Steps[step].Successful;
            if (successful.HasValue && successful.Value)
            {
                throw new Exception("Already performed this step");
            }

            Steps[step].Successful = true;
            var nextStep = Steps[step].NextStep;
            if (nextStep != null) _currentStep = nextStep.Value;
            _bank.Login(creds);
            return this.Steps[_currentStep];
        }

        private Guid CreateNextStep(string field, string data, bool success, Guid thisStep)
        {
            Guid next;
            Steps[thisStep].Id = thisStep;
            Steps[thisStep].Field = field;
            Steps[thisStep].Data = data;
            Steps[thisStep].Successful = success;
            Steps[thisStep].NextStep = next = Guid.NewGuid();

            this.Steps.Add(next, new StepDefinition() {Data = "", Field = "", Id = next, Successful = false});

            return next;
        }

        private void BankOnRequireUserName()
        {
            CreateNextStep("UserName", "User Name", false, _currentStep);
        }

        private void BankOnRequireQuestion(string s)
        {
            CreateNextStep("Question", s, false, _currentStep);
        }

        private void BankOnRequirePassword()
        {
            CreateNextStep("Password", "Password", false, _currentStep);
        }

        private void BankOnRequirePin()
        {
            CreateNextStep("Pin", "Pin", false, _currentStep);
        }
    }
}