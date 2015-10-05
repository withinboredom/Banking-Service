using System;
using System.Security.Cryptography;
using Interfaces;
using Microsoft.WindowsAzure.Storage.Table;

namespace DataLibrary.DataConstructs
{
    /// <summary>
    /// What's needed
    /// </summary>
    public class StepDefinition : TableEntity, IStepDefinition
    {
        private Guid _bankId;
        private Guid _userId;

        /// <summary>
        /// The id of the step
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// The field to set
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// The data hint on what the field should contain
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// Whether or not this step was successful
        /// </summary>
        public bool? Successful { get; set; }

        /// <summary>
        /// The id of the next step to perform, if applicabale...
        /// </summary>
        public Guid? NextStep { get; set; }

        /// <summary>
        /// The bank id of this step
        /// </summary>
        public Guid BankId
        {
            get { return _bankId; }
            set
            {
                _bankId = value;
                RowKey = SHA1.Create(value.ToString()).ToString();
            }
        }

        /// <summary>
        /// The user id of this step
        /// </summary>
        public Guid UserId
        {
            get { return _userId; }
            set
            {
                _userId = value;
                PartitionKey = SHA1.Create(value.ToString()).ToString();
            }
        }

        public StepDefinition(IStepDefinition copy)
        {
            Id = copy.Id;
            Field = copy.Field;
            Data = copy.Data;
            Successful = copy.Successful;
            NextStep = copy.NextStep;
            BankId = copy.BankId;
            UserId = copy.UserId;

            PartitionKey = SHA1.Create(UserId.ToString()).ToString();
            RowKey = SHA1.Create(BankId.ToString()).ToString();
        }

        public StepDefinition() { }
    }
}