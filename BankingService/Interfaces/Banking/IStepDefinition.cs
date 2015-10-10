using System;

namespace Interfaces.Banking
{
    public interface IStepDefinition
    {
        /// <summary>
        /// The id of the step
        /// </summary>
        Guid? Id { get; set; }

        /// <summary>
        /// The field to set
        /// </summary>
        string Field { get; set; }

        /// <summary>
        /// The data hint on what the field should contain
        /// </summary>
        string Data { get; set; }

        /// <summary>
        /// Whether or not this step was successful
        /// </summary>
        bool? Successful { get; set; }

        /// <summary>
        /// The id of the next step to perform, if applicabale...
        /// </summary>
        Guid? NextStep { get; set; }

        /// <summary>
        /// The bank id this step was operated against
        /// </summary>
        Guid BankId { get; set; }

        /// <summary>
        /// The user id this step belongs to
        /// </summary>
        Guid UserId { get; set; }
    }
}