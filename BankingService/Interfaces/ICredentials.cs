namespace Interfaces
{
    public interface ICredentials
    {
        /// <summary>
        /// A username
        /// </summary>
        string UserName { get; set; }

        /// <summary>
        /// A password
        /// </summary>
        string Password { get; set; }

        /// <summary>
        /// A pin
        /// </summary>
        string Pin { get; set; }

        /// <summary>
        /// An answer to a question
        /// </summary>
        string Question { get; set; }
    }
}