using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BankingService.Models.DataConstructs;

namespace BankingService.Models.Banks
{
    public class BankDriver
    {
        public BankDriver(USAA bank)
        {
            bank.RequirePin += BankOnRequirePin;
            bank.RequirePassword += BankOnRequirePassword;
            bank.RequireQuestion += BankOnRequireQuestion;
            bank.RequireUserName += BankOnRequireUserName;
            bank.LoginComplete += BankOnLoginComplete;
            bank.LoginFailed += BankOnLoginFailed;

            steps = new Dictionary<Guid, StepDefinition>();

            this.bank = bank;
        }

        private void BankOnLoginFailed()
        {
            this.steps[currentStep].Successful = false;
            CreateNextStep("Failed", "Failed to login", false, currentStep);
        }

        private void BankOnLoginComplete()
        {
            CreateNextStep("Success", "Logged in", true, currentStep);
        }

        public Dictionary<Guid, StepDefinition> steps { get; private set; }
        private Guid currentStep;
        private USAA bank;

        public Guid Login(Credentials creds)
        {
            var step = new StepDefinition();
            this.currentStep = Guid.NewGuid();
            step.Id = currentStep;
            steps.Add(this.currentStep, step);

            bank.Login(creds);

            return this.currentStep;
        }

        public StepDefinition DoStep(Guid step, Credentials creds)
        {
            var successful = this.steps[step].Successful;
            if (successful.HasValue && successful.Value)
            {
                throw new Exception("Already performed this step");
            }

            this.steps[step].Successful = true;
            this.currentStep = this.steps[step].NextStep.Value;
            bank.Login(creds);
            return this.steps[this.currentStep];
        }

        private Guid CreateNextStep(string field, string data, bool success, Guid thisStep)
        {
            Guid next;
            steps[thisStep].Id = thisStep;
            steps[thisStep].Field = field;
            steps[thisStep].Data = data;
            steps[thisStep].Successful = success;
            steps[thisStep].NextStep = next = Guid.NewGuid();

            this.steps.Add(next, new StepDefinition() {Data = "", Field = "", Id = next, Successful = false});

            return next;
        }

        private void BankOnRequireUserName()
        {
            CreateNextStep("UserName", "User Name", false, currentStep);
        }

        private void BankOnRequireQuestion(string s)
        {
            CreateNextStep("Question", s, false, currentStep);
        }

        private void BankOnRequirePassword()
        {
            CreateNextStep("Password", "Password", false, currentStep);
        }

        private void BankOnRequirePin()
        {
            CreateNextStep("Pin", "Pin", false, currentStep);
        }
    }
}