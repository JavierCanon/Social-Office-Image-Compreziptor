﻿using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Task = System.Threading.Tasks.Task;

namespace MadsKristensen.ImageOptimizer
{
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", Vsix.Version, IconResourceID = 400)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    //[ProvideAutoLoad(UIContextGuids80.SolutionHasSingleProject)]
    //[ProvideAutoLoad(UIContextGuids80.SolutionHasMultipleProjects)]
    [Guid(PackageGuids.guidImageOptimizerPkgString)]
    [ProvideUIContextRule(PackageGuids.guidAutoloadImagesString,
    name: "Images",
    expression: "Images",
    termNames: new[] { "Images" },
    termValues: new[] { "HierSingleSelectionName:.(png|jpg|jpeg|gif)$" })]
    public sealed class ImageOptimizerPackage : AsyncPackage
    {
        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await JoinableTaskFactory.SwitchToMainThreadAsync();
            await Logger.InitializeAsync(this, Vsix.Name);
            await OptimizeCommand.InitializeAsync(this);
        }
    }
}