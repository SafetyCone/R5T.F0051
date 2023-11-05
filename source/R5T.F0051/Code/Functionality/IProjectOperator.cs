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

			ProjectFileGenerator.Instance.CreateNew(
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

		public void PostSetupProject_ProgramAsService(
			string projectFilePath,
			string projectDescription,
			string projectName,
			string projectDefaultNamespaceName)
        {
			// Add Projects.
			Instances.ProjectFileOperator.AddProjectReference_Idempotent_Synchronous(
				projectFilePath,
				@"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.F0035\source\R5T.F0035\R5T.F0035.csproj");

			Instances.ProjectFileOperator.AddProjectReference_Idempotent_Synchronous(
				projectFilePath,
				@"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.F0037\source\R5T.F0037\R5T.F0037.csproj");

			// Recreate the "Program.cs" file.
			var programFilePath = Instances.ProjectPathsOperator.GetProgramFilePath(projectFilePath);

			Instances.CodeFileGenerator.CreateProgramFile_ProgramAsService(
				programFilePath,
				projectDefaultNamespaceName);

			// Create the "Program-Entry Point.cs" file.
			var programEntryPointFilePath = Instances.ProjectPathsOperator.GetProgramEntryPointFilePath(projectFilePath);

			Instances.CodeFileGenerator.CreateProgramFile_ProgramEntryPoint(
				programEntryPointFilePath,
				projectDefaultNamespaceName);

			// Create the "ServicesConfigurer.cs" file.
			var servicesConfigurerFilePath = Instances.ProjectPathsOperator.GetServicesConfigurerFilePath(projectFilePath);

			Instances.CodeFileGenerator.CreateServicesConfigurer(
				servicesConfigurerFilePath,
				projectDefaultNamespaceName);
		}

		public void PostSetupProject_WebApplication(
			string projectFilePath,
			string projectDescription,
			string projectName,
			string projectDefaultNamespaceName)
		{
			// Add Projects.
			var dependencyProjectFilePaths = new[]
			{
				@"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.F0035\source\R5T.F0035\R5T.F0035.csproj",
				@"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.F0036\source\R5T.F0036.F001\R5T.F0036.F001.csproj",
				@"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.F0066\source\R5T.F0066\R5T.F0066.csproj"
			};

            foreach (var dependencyProjectFilePath in dependencyProjectFilePaths)
            {
				Instances.ProjectFileOperator.AddProjectReference_Idempotent_Synchronous(
					projectFilePath,
					dependencyProjectFilePath);
			}

			// Recreate the "Program.cs" file.
			var programFilePath = Instances.ProjectPathsOperator.GetProgramFilePath(projectFilePath);

			Instances.CodeFileGenerator.CreateProgramFile_WebApplication_WithConfigurers(
				programFilePath,
				projectDefaultNamespaceName);

			// Create the "ServicesConfigurer.cs" file.
			var servicesConfigurerFilePath = Instances.ProjectPathsOperator.GetServicesConfigurerFilePath(projectFilePath);

			Instances.CodeFileGenerator.CreateServicesConfigurer(
				servicesConfigurerFilePath,
				projectDefaultNamespaceName);

			// Create the WebApplicationBuilderConfigurer file.
			var webApplicationBuilderConfigurerFilepath = Instances.ProjectPathsOperator.GetWebApplicationBuilderConfigurerFilePath(projectFilePath);

			Instances.CodeFileGenerator.CreateWebApplicationBuilderConfigurer(
				webApplicationBuilderConfigurerFilepath,
				projectDefaultNamespaceName);

			// Create the WebApplicationConfigurer file.
			var webApplicationConfigurerFilepath = Instances.ProjectPathsOperator.GetWebApplicationConfigurerFilePath(projectFilePath);

			Instances.CodeFileGenerator.CreateWebApplicationConfigurer(
				webApplicationConfigurerFilepath,
				projectDefaultNamespaceName);
		}

		public void SetupProject_WebApplication(
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

			// Modify the project.
			Instances.ProjectFileOperator.InModifyProjectFileContext_Synchronous(
				projectFilePath,
				projectElement =>
				{
					Instances.ProjectXmlOperator.SetTargetFramework(
						projectElement,
						Instances.TargetFrameworkMonikerStrings.NET_6);

					Instances.ProjectXmlOperator.SetSdk(
						projectElement,
						Instances.ProjectSdkStrings.Web);
				});

			// Create the program file.
			var programFilePath = Instances.ProjectPathsOperator.GetProgramFilePath(projectFilePath);

			Instances.CodeFileGenerator.CreateProgramFile_WebApplication(
				programFilePath,
				projectDefaultNamespaceName);

			// Create the properties directory.
			var propertiesDirectoryPath = Instances.ProjectPathsOperator.GetPropertiesDirectoryPath(projectFilePath);
			
			Instances.FileSystemOperator.Create_Directory_OkIfAlreadyExists(propertiesDirectoryPath);

			// Create the launch settings JSON file.
			var launchSettingsJsonFilePath = Instances.ProjectPathsOperator.GetLaunchSettingsJsonFilePath(projectFilePath);

			Instances.TextFileGenerator.CreateLaunchSettingsJsonFile(launchSettingsJsonFilePath);

			// Create appsettings.json file.
			var appsettingsJsonFilePath = Instances.ProjectPathsOperator.GetAppSettingsJsonFilePath(projectFilePath);

			Instances.TextFileGenerator.CreateAppSettingsJsonFile(appsettingsJsonFilePath);

			// Create appsettings.Development.json file.
			var appsettingsDevelopmentJsonFilePath = Instances.ProjectPathsOperator.GetAppSettingsDevelopmentJsonFilePath(projectFilePath);

			Instances.TextFileGenerator.CreateAppSettingsDevelopmentJsonFile(appsettingsDevelopmentJsonFilePath);
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

			Instances.FileSystemOperator.Create_Directory_OkIfAlreadyExists(codeDirectoryPath);

			// Create the instances file.
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