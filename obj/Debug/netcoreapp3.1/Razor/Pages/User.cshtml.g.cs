#pragma checksum "/home/markus/Code/C#/BugTracker/Pages/User.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "99fe2bf4100ad1bf24f8772c4f6b61332621181b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(BugTracker.Pages.Pages_User), @"mvc.1.0.razor-page", @"/Pages/User.cshtml")]
namespace BugTracker.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "/home/markus/Code/C#/BugTracker/Pages/_ViewImports.cshtml"
using BugTracker;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"99fe2bf4100ad1bf24f8772c4f6b61332621181b", @"/Pages/User.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"856bd3c210755756174992806ebba92adeeb5a4e", @"/Pages/_ViewImports.cshtml")]
    public class Pages_User : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "/home/markus/Code/C#/BugTracker/Pages/User.cshtml"
  
    var list = (IList<Models.User>) @ViewData["list"];

#line default
#line hidden
#nullable disable
            WriteLiteral("\n<div>\n    <ul class=\"list-group list-group-flush\">\n");
#nullable restore
#line 9 "/home/markus/Code/C#/BugTracker/Pages/User.cshtml"
         foreach (var user in list)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <li class=\"list-group-item\">\n                ");
#nullable restore
#line 12 "/home/markus/Code/C#/BugTracker/Pages/User.cshtml"
           Write(user.UserId);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                <input class=\"form-control\"");
            BeginWriteAttribute("value", " value=\"", 289, "\"", 307, 1);
#nullable restore
#line 13 "/home/markus/Code/C#/BugTracker/Pages/User.cshtml"
WriteAttributeValue("", 297, user.Name, 297, 10, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("/>\n                <input class=\"form-control\"");
            BeginWriteAttribute("value", " value=\"", 354, "\"", 376, 1);
#nullable restore
#line 14 "/home/markus/Code/C#/BugTracker/Pages/User.cshtml"
WriteAttributeValue("", 362, user.Password, 362, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("/>\n                <input class=\"form-control\"");
            BeginWriteAttribute("value", " value=\"", 423, "\"", 457, 1);
#nullable restore
#line 15 "/home/markus/Code/C#/BugTracker/Pages/User.cshtml"
WriteAttributeValue("", 431, user.ActiveInd.ToString(), 431, 26, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("/>\n                <button class=\"btn alert-danger\">Update</button>\n            </li>\n");
#nullable restore
#line 18 "/home/markus/Code/C#/BugTracker/Pages/User.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </ul>\n</div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<User> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<User> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<User>)PageContext?.ViewData;
        public User Model => ViewData.Model;
    }
}
#pragma warning restore 1591
