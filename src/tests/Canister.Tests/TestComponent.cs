// <copyright file="TestComponent.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1649:FileHeaderFileNameDocumentationMustMatchTypeName", Scope = "Module", Justification = "Content is valid.")]
[assembly: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Scope = "Module", Justification = "Content is valid.")]

namespace Canister.Tests
{
    public interface IDoStuff
    {
    }

    public interface IDoOtherStuff
    {
    }

    public class DoStuff : IDoStuff
    {
    }

    public class DoOtherStuff : IDoOtherStuff
    {
    }

    public class DoEverything : IDoStuff, IDoOtherStuff
    {
    }
}
