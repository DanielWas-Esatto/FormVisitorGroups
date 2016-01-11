using System.Collections.Generic;

namespace FormVisitorGroups.Impl
{
    /// <summary>
    /// Retrieve a list of form submission Ids for the current user
    /// </summary>
    public interface ISubmittedFormsRepository
    {
        /// <summary>
        /// Check if the repository is active and able to save/return form submisson Ids
        /// </summary>
        /// <returns>True if the repo is active</returns>
        bool IsActive();

        /// <summary>
        /// Get a list of all submissions by the current user
        /// </summary>
        /// <returns></returns>
        IList<string> GetFormSubmissionIdsList();

        /// <summary>
        /// Add a form submission for the current user
        /// </summary>
        /// <param name="submissionId">The submission guid of the form being submitted</param>
        void AddFormSubmissionId(string submissionId);
    }
}