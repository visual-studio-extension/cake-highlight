#tool "nuget:?package=Fixie"
#addin "nuget:?package=Cake.Watch"

var solution = "Cake.Highlight.sln";

Task("Build-Debug").Does(() => {
    DotNetBuild(solution);
});

Task("MS-Build-Debug").Does(() => {
    MSBuild(solution, new MSBuildSettings {
        Verbosity = Verbosity.Minimal,
        ToolVersion = MSBuildToolVersion.VS2015,
        Configuration = "Debug",
        ToolPath = @"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe",
        PlatformTarget = PlatformTarget.MSIL
    });
});

var target = Argument("target", "default");
RunTarget(target);