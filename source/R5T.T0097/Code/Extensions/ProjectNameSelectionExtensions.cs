using System;


namespace R5T.T0097
{
    public static class ProjectNameSelectionExtensions
    {
        public static ProjectNameSelection Copy(this ProjectNameSelection other)
        {
            var output = new ProjectNameSelection
            {
                ProjectIdentity = other.ProjectIdentity,
                ProjectName = other.ProjectName,
            };

            return output;
        }
    }
}
