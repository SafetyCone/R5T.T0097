using System;

using R5T.T0044;
using R5T.T0055;
using R5T.T0057;


namespace R5T.T0097.X002
{
    public static class Instances
    {
        public static IDuplicateValuesOperator DuplicateValuesOperator { get; } = T0057.DuplicateValuesOperator.Instance;
        public static IFileSystemOperator FileSystemOperator { get; } = T0044.FileSystemOperator.Instance;
        public static IGuidOperator GuidOperator { get; } = T0055.GuidOperator.Instance;
    }
}
