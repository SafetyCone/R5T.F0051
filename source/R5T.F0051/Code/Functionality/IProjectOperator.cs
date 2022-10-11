using System;

using Microsoft.Extensions.Logging;

using R5T.F0020;
using R5T.T0132;


namespace R5T.F0051
{
	[FunctionalityMarker]
	public partial interface IProjectOperator : IFunctionalityMarker
	{
		public string Create_New(
			string solutionFilePath,
			string projectName,
			ProjectType projectType,
			ILogger logger)
		{
			var solutionDirectoryPath = Instances.PathOperator.GetParentDirectoryPath_ForFile(solutionFilePath);

			var projectDirectoryName = Instances.ProjectDirectoryNameOperator.GetProjectDirectoryName_FromProjectName(projectName);
			var projectDirectoryPath = Instances.PathOperator.GetDirectoryPath(
				solutionDirectoryPath,
				projectDirectoryName);

			var projectFileName = Instances.ProjectFileNameOperator.GetProjectFileName_FromProjectName(projectName);
			var projectFilePath = Instances.PathOperator.GetFilePath(
				projectDirectoryPath,
				projectFileName);

			logger.LogInformation($"Creating new project file...{Environment.NewLine}\t{projectFilePath}");

			Instances.ProjectFileOperator.Create_New(
				projectFilePath,
				projectType);

			logger.LogInformation($"Created new project file.{Environment.NewLine}\t{projectFilePath}");

			return projectFilePath;
		}

		public string Get_ProjectDescription_FromLibraryDescription(string libraryDescription)
		{
			// Project description is just the library description.
			var projectDescription = libraryDescription;
			return projectDescription;
		}

		public void SetupProject_Console(
			string projectFilePath,
			string projectDescription,
			string projectName,
			string projectDefaultNamespaceName)
		{
			this.SetupProject_Initial(
				projectFilePath,
				projectDescription,
				projectName,
				projectDefaultNamespaceName);

			// Create the program file.
			var programFilePath = Instances.ProjectPathsOperator.GetProgramFilePath(projectFilePath);

			Instances.CodeFileGenerator.CreateProgramFile(
				programFilePath,
				projectDefaultNamespaceName);
		}

		public void SetupProject_Initial(
			string projectFilePath,
			string projectDescription,
			string projectName,
			string projectDefaultNamespaceName)
		{
			// Create the project plan Markdown file.
			var projectPlanFilePath = Instances.ProjectPathsOperator.GetProjectPlanMarkdownFilePath(projectFilePath);

			Instances.TextFileGenerator.CreateProjectPlanMarkdownFile(
				projectPlanFilePath,
				projectName,
				projectDescription);

			// Create the code directory.
			var codeDirectoryPath = Instances.ProjectPathsOperator.GetCodeDirectoryPath(projectFilePath);

			Instances.FileSystemOperator.CreateDirectory_OkIfAlreadyExists(codeDirectoryPath);

			// Create the Instances file.
			var instanceFilePath = Instances.ProjectPathsOperator.GetInstancesFilePath(projectFilePath);

			Instances.CodeFileGenerator.CreateInstancesFile(
				instanceFilePath,
				projectDefaultNamespaceName);

			// Create the documentation file.
			var documentationFilePath = Instances.ProjectPathsOperator.GetDocumentationFilePath(projectFilePath);

			Instances.CodeFileGenerator.CreateDocumentationFile(
				documentationFilePath,
				projectDefaultNamespaceName,
				projectDescription);
		}

		public void SetupProject_Library(
			string projectFilePath,
			string projectDescription,
			string projectName,
			string projectDefaultNamespaceName)
		{
			this.SetupProject_Initial(
				projectFilePath,
				projectDescription,
				projectName,
				projectDefaultNamespaceName);
		}
	}
}