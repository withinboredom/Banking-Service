using System;

namespace Interfaces
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
    }
}