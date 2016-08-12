#tool "nuget:?package=Fixie"
#addin "nuget:?package=Cake.Watch"

var solution = "Cake.Highlight.sln";

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
        MSBuild(solution, new MSBuildSettings {
            Verbosity = Verbosity.Minimal,
            ToolVersion = MSBuildToolVersion.VS2015,
            Configuration = "Debug",
            ToolPath = @"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe",
            PlatformTarget = PlatformTarget.MSIL
        });
    });

Task("Rebuild-Debug")
    .Does(() => {
        MSBuild(solution, new MSBuildSettings {
            Verbosity = Verbosity.Minimal,
            ToolVersion = MSBuildToolVersion.VS2015,
            Configuration = "Debug",
            ToolPath = @"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe",
            PlatformTarget = PlatformTarget.MSIL
        }.WithTarget("Rebuild"));
    });

var target = Argument("target", "default");
RunTarget(target);