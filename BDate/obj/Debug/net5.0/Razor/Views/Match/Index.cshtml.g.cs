#pragma checksum "C:\Users\klimi\source\repos\Blind-Dating-Site\BDate\Views\Match\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9b8a8f686c6f1f42a48b35df853c12bdf5b11458"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Match_Index), @"mvc.1.0.view", @"/Views/Match/Index.cshtml")]
namespace AspNetCore
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
#line 1 "C:\Users\klimi\source\repos\Blind-Dating-Site\BDate\Views\_ViewImports.cshtml"
using BDate;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\klimi\source\repos\Blind-Dating-Site\BDate\Views\_ViewImports.cshtml"
using BDate.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9b8a8f686c6f1f42a48b35df853c12bdf5b11458", @"/Views/Match/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c72fddc8e527f5528af97051417dc561c0ed7574", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Match_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\klimi\source\repos\Blind-Dating-Site\BDate\Views\Match\Index.cshtml"
  
    ViewData["Title"] = "Match Page";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"text-center\">\r\n    <h1 class=\"display-4\">Meklējiet partneri</h1>\r\n</div>\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "9b8a8f686c6f1f42a48b35df853c12bdf5b114583542", async() => {
                WriteLiteral(@"
    <div class=""container"">
        <div class=""row"">
            <div class=""form-group col col-sm-12"">
                <label>Vārds, uzvārds</label>
            </div>
        </div>
    </div>
    <div class=""container"">
        <div class=""row"">
            <div class=""form-group col col-sm-12"">
                <label>Dzimums (vīrietis♂, sieviete♀):</label>
            </div>
        </div>
    </div>
    <div class=""container"">
        <div class=""row"">
            <div class=""form-group col col-sm-12"">
                <label>Dzimums (psiholoģiskais):</label>
            </div>
        </div>
    </div>
    <div class=""container"">
        <div class=""row"">
            <div class=""form-group col col-sm-12"">
                <label>Personīgas īpašības:</label>
            </div>
        </div>
    </div>
    <div class=""container"">
        <div class=""row"">
            <div class=""form-group col col-sm-12"">
                <label>Hobiji, Intereses, Vaļasprieki</label>
     ");
                WriteLiteral(@"       </div>
        </div>
    </div>
    <div class=""container"">
        <div class=""row"">
            <div class=""form-group col col-sm-3"">
                <button type=""submit"" class=""btn btn-primary btn-full-space testA"">Like😎</button>
            </div>
            <div class=""form-group col col-sm-3"">
                <button type=""submit"" class=""btn btn-primary btn-full-space testB"">Dislike😒</button>
            </div>
        </div>
    </div>

");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n<style>\r\n    .testA {\r\n        background-color: #4CAF50 !important;\r\n    }\r\n    .testB {\r\n        background-color: #8B0000 !important;\r\n    }\r\n\r\n    .btn-full-space {\r\n        width: 70%;\r\n    }\r\n</style>");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591