#tool "nuget:?package=Fixie"
#addin "nuget:?package=Cake.Watch"
#addin "MagicChunks"

var solution = "Cake.Highlight.sln";
var testProj = @"Cake.Highlight.Tests/Cake.Highlight.Tests.csproj";
var testDll = @"Cake.Highlight.tests/bin/Debug/Cake.Highlight.Tests.dll";

var package = "Cake.Highlight/bin/Debug/JCake.Highlight.vsix";
var assemblyInfo = "Cake.Highlight/Properties/AssemblyInfo.cs";

var user = EnvironmentVariable("ghu");
var pass = EnvironmentVariable("ghp");

Action<string,string> build = (proj, target) => {
    MSBuild(proj, new MSBuildSettings {
        Verbosity = Verbosity.Minimal,
        ToolVersion = MSBuildToolVersion.VS2015,
        Configuration = "Debug",
        ToolPath = @"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe",
        PlatformTarget = PlatformTarget.MSIL
    }.WithTarget(target));
};

Task("Create-Github-Release")
    .IsDependentOn("Build-Debug")
    .Does(() => {
        var asm = ParseAssemblyInfo(assemblyInfo);
        var version = asm.AssemblyVersion;
        var tag = string.Format("v{0}", version);
        var args = string.Format("tag -a {0} -m \"{0}\"", tag);
        var owner = "wk-j";
        var repo = "cake-highlight";

        StartProcess("git", new ProcessSettings {
            Arguments = args
        });

        StartProcess("git", new ProcessSettings {
            Arguments = string.Format("push https://{0}:{1}@github.com/wk-j/{2}.git {3}", user, pass, repo, tag)
        });

        GitReleaseManagerCreate(user, pass, owner , repo, new GitReleaseManagerCreateSettings {
            Name              = tag,
            InputFilePath = "RELEASE.md",
            Prerelease        = false,
            TargetCommitish   = "master",
        });
        GitReleaseManagerAddAssets(user, pass, owner, repo, tag, package);
        GitReleaseManagerPublish(user, pass, owner , repo, tag);
    });

Task("fixie")
    .Does(() => {
        build(testProj, "Build");
        var config = testDll + ".config";
        var className = Argument("className", "");
        TransformConfig(config, new TransformationCollection {
                { "configuration/appSettings/add[@key='fixie']/@value", className }
        });
        Fixie(testDll);
    });


Task("Reset-Experimental-Instance").Does(() => {
    var bin = @"C:\Program Files (x86)\Microsoft Visual Studio 14.0\VSSDK\VisualStudioIntegration\Tools\Bin\CreateExpInstance.exe";
    var args = "/Reset /VSInstance=14.0 /RootSuffix=Exp";
    StartProcess(bin, new ProcessSettings {
        Arguments = args
    });
});

Task("View-Log")
    .Does(() => {
        var path = @"C:\Users\wk\AppData\Roaming\Microsoft\VisualStudio\14.0Exp\ActivityLog.xml";
        StartProcess("notepad", new ProcessSettings { Arguments = path } );
    });

Task("Build-Debug")
    .Does(() => {
        build(solution, "Build");
    });

Task("Rebuild-Debug")
    .Does(() => {
        build(solution, "Rebuild");
    });

var target = Argument("target", "default");
RunTarget(target);