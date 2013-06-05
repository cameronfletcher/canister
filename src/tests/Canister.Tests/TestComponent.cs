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
        public DoStuff(string value = null)
        {
            this.Value = value;
        }

        public string Value { get; set; }
    }

    public class DoOtherStuff : IDoOtherStuff
    {
        public DoOtherStuff(string value = null)
        {
            this.Value = value;
        }

        public string Value { get; set; }
    }

    public class DoEverything : IDoStuff, IDoOtherStuff
    {
        public DoEverything(string value = null)
        {
            this.Value = value;
        }

        public string Value { get; set; }
    }
}
