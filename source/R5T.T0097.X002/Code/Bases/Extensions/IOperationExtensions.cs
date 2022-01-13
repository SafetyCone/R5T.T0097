using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using R5T.T0097;
using R5T.T0098;


// Use specific (non-System) namespace.
namespace R5T.T0097.X002
{
    public static class IOperationExtensions
    {
        /// <summary>
        /// Loads a file formatted as {Project Name}| {Project File Path} for convenience of human analysis. This requires looking up the project file path among the input projects to get the corresponding identity.
        /// Thus, all file path values must exist in the input projects.
        /// Returns an empty array if the input file path does not exist.
        /// </summary>
        public static async Task<ProjectNameSelection[]> LoadProjectNameSelectionsReturnEmptyIfNotExists(this IOperation _,
            string projectNamesTextFilePath,
            IEnumerable<Project> projects)
        {
            var duplicateValueSelections = Instances.FileSystemOperator.FileExists(projectNamesTextFilePath)
                ? await Instances.DuplicateValuesOperator.LoadDuplicateValueSelections(projectNamesTextFilePath)
                : new Dictionary<string, string>()
                ;

            // Check inputs.
            projects.VerifyDistinctByFilePath();

            var projectsByFilePath = projects.ToDictionaryByFilePath();

            var projectNameSelections = duplicateValueSelections
                .Select(xPair =>
                {
                    // File path may not exist (for example if a project departs, but was selected).
                    var filePathExists = projectsByFilePath.ContainsKey(xPair.Value);
                    if(filePathExists)
                    {
                        var projectForFilePath = projectsByFilePath[xPair.Value];

                        var output = new ProjectNameSelection
                        {
                            ProjectName = xPair.Key,
                            ProjectIdentity = projectForFilePath.Identity
                        };

                        return output;
                    }
                    else
                    {
                        // Set a dummy identity?
                        var output = new ProjectNameSelection
                        {
                            ProjectName = xPair.Key,
                            ProjectIdentity = Instances.GuidOperator.DefaultGuid(),
                        };

                        return output;
                    }
                })
                .ToArray()
                ;

            return projectNameSelections;
        }

        /// <summary>
        /// Chooses <see cref="LoadProjectNameSelectionsReturnEmptyIfNotExists(string, IEnumerable{Project})"/> as the default.
        /// </summary>
        public static async Task<ProjectNameSelection[]> LoadProjectNameSelections(this IOperation _,
            string projectNamesTextFilePath,
            IEnumerable<Project> projects)
        {
            var output = await _.LoadProjectNameSelectionsReturnEmptyIfNotExists(
                projectNamesTextFilePath,
                projects);

            return output;
        }

        /// <summary>
        /// Saves a file formatted as {Project Name}| {Project File Path} for convenience of human analysis. This requires looking up the project identity among the input projects to get the corresponding file path.
        /// Thus, all identity values must exist in the input projects.
        /// </summary>
        public static async Task SaveProjectNameSelections(this IOperation _,
            string projectNameSelectionsTextFilePath,
            IEnumerable<ProjectNameSelection> projectNameSelections,
            IEnumerable<Project> projects)
        {
            // Verify inputs.
            // No verification of project name selections since this code does not require that.
            projects.VerifyDistinctByIdentity();

            // Run.
            var projectsByIdentity = projects.ToDictionary(xProject => xProject.Identity);

            var projectNameValues = projectNameSelections
                .OrderAlphabetically(xProjectNameSelection => xProjectNameSelection.ProjectName)
                .ToDictionary(
                    xProjectNameSelection => xProjectNameSelection.ProjectName,
                    xProjectNameSelection =>
                    {
                        //
                        var projectForIdentity = projectsByIdentity[xProjectNameSelection.ProjectIdentity];

                        var projectFilePath = projectForIdentity.FilePath;
                        return projectFilePath;
                    });

            await Instances.DuplicateValuesOperator.SaveDuplicateValueSelections(
                projectNameSelectionsTextFilePath,
                projectNameValues);
        }
    }
}