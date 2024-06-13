using System;


namespace R5T.F0051
{
    public static class Instances
    {
        public static F0053.ICodeFileGenerator CodeFileGenerator { get; } = F0053.CodeFileGenerator.Instance;
        public static L0066.IFileSystemOperator FileSystemOperator { get; } = L0066.FileSystemOperator.Instance;
        public static L0066.IPathOperator PathOperator { get; } = L0066.PathOperator.Instance;
        public static F0052.IProjectDirectoryNameOperator ProjectDirectoryNameOperator { get; } = F0052.ProjectDirectoryNameOperator.Instance;
        public static F0052.IProjectFileNameOperator ProjectFileNameOperator { get; } = F0052.ProjectFileNameOperator.Instance;
        public static F0020.IProjectFileOperator ProjectFileOperator { get; } = F0020.ProjectFileOperator.Instance;
        public static F0052.IProjectPathsOperator ProjectPathsOperator { get; } = F0052.ProjectPathsOperator.Instance;
        public static F0020.IProjectSdkStrings ProjectSdkStrings { get; } = F0020.ProjectSdkStrings.Instance;
        public static F0020.IProjectXmlOperator ProjectXmlOperator { get; } = F0020.ProjectXmlOperator.Instance;
        public static F0020.ITargetFrameworkMonikerStrings TargetFrameworkMonikerStrings { get; } = F0020.TargetFrameworkMonikerStrings.Instance;
        public static F0054.ITextFileGenerator TextFileGenerator { get; } = F0054.TextFileGenerator.Instance;
    }
}