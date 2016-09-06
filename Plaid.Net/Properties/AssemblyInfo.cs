using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Plaid.Net")]
[assembly: AssemblyDescription("C# Driver for Plaid: The API for banking data.")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Tyler Jensen")]
[assembly: AssemblyProduct("Plaid.Net")]
[assembly: AssemblyCopyright("Copyright ©  2016")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("1dad3cf9-1b31-4069-853d-1d0c0ca84262")]

// Version information for an assembly consists of the following four values:
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.2.2.0")]
[assembly: AssemblyFileVersion("1.2.2.0")]

// Expose internals to the test project and Moq testing framework
[assembly: InternalsVisibleTo("Plaid.Net.Tests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]