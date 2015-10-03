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

        /// <summary>
        /// Called when a login completes sucessfully
        /// </summary>
        private void BankOnLoginComplete()
        {
            CreateNextStep("Success", "Logged in", true, _currentStep);
        }

        /// <summary>
        /// Steps in progress or completed
        /// </summary>
        public Dictionary<Guid, StepDefinition> Steps { get; private set; }

        /// <summary>
        /// The current step guid
        /// </summary>
        private Guid _currentStep;

        /// <summary>
        /// The bank we are working with
        /// </summary>
        private readonly Bank _bank;

        /// <summary>
        /// Attempts to login with a set of credentials
        /// </summary>
        /// <param name="creds">The credentials to login with</param>
        /// <returns>A step</returns>
        public StepDefinition Login(Credentials creds)
        {
            var step = new StepDefinition();
            this._currentStep = Guid.NewGuid();
            step.Id = _currentStep;
            Steps.Add(this._currentStep, step);

            _bank.Login(creds);

            return step;
        }

        /// <summary>
        /// Performs a step with a given set of credentials
        /// </summary>
        /// <param name="step">The step to operate on</param>
        /// <param name="creds">The credentials to operate with</param>
        /// <returns>The next step</returns>
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

        /// <summary>
        /// Creates a next step
        /// </summary>
        /// <param name="field">The field that the next step is for</param>
        /// <param name="data">The data required</param>
        /// <param name="success">Has the step been completed</param>
        /// <param name="thisStep">The current step</param>
        /// <returns>A step id</returns>
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

        /// <summary>
        /// Bank requires a user name
        /// </summary>
        private void BankOnRequireUserName()
        {
            CreateNextStep("UserName", "User Name", false, _currentStep);
        }

        /// <summary>
        /// Bank requires a question/answer
        /// </summary>
        /// <param name="s">The question that needs an answer</param>
        private void BankOnRequireQuestion(string s)
        {
            CreateNextStep("Question", s, false, _currentStep);
        }

        /// <summary>
        /// Requires a password
        /// </summary>
        private void BankOnRequirePassword()
        {
            CreateNextStep("Password", "Password", false, _currentStep);
        }

        /// <summary>
        /// Requires a pin
        /// </summary>
        private void BankOnRequirePin()
        {
            CreateNextStep("Pin", "Pin", false, _currentStep);
        }
    }
}