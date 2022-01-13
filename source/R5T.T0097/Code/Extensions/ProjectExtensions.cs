using System;


namespace R5T.T0097
{
    public static class ProjectExtensions
    {
        public static Project Copy(this Project other)
        {
            var output = new Project
            {
                FilePath = other.FilePath,
                Identity = other.Identity,
                Name = other.Name,
            };

            return output;
        }
    }
}
