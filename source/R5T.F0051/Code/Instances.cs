using System;

using R5T.F0000;
using R5T.F0002;
using R5T.F0020;
using R5T.F0052;
using R5T.F0053;
using R5T.F0054;


namespace R5T.F0051
{
    public static class Instances
    {
        public static ICodeFileGenerator CodeFileGenerator { get; } = F0053.CodeFileGenerator.Instance;
        public static F0000.IFileSystemOperator FileSystemOperator { get; } = F0000.FileSystemOperator.Instance;
        public static F0002.IPathOperator PathOperator { get; } = F0002.PathOperator.Instance;
        public static IProjectDirectoryNameOperator ProjectDirectoryNameOperator { get; } = F0052.ProjectDirectoryNameOperator.Instance;
        public static IProjectFileNameOperator ProjectFileNameOperator { get; } = F0052.ProjectFileNameOperator.Instance;
        public static IProjectFileOperator ProjectFileOperator { get; } = F0020.ProjectFileOperator.Instance;
        public static IProjectPathsOperator ProjectPathsOperator { get; } = F0052.ProjectPathsOperator.Instance;
        public static ITextFileGenerator TextFileGenerator { get; } = F0054.TextFileGenerator.Instance;
    }
}