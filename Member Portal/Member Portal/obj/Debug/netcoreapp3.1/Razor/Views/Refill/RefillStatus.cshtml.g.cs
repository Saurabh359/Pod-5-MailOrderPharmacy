#pragma checksum "C:\Users\user\Desktop\Mail Order Pharmacy\Member Portal\Member Portal\Views\Refill\RefillStatus.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d6bb58d508d23248fc06e595e583a59ea882db2e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Refill_RefillStatus), @"mvc.1.0.view", @"/Views/Refill/RefillStatus.cshtml")]
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
#line 1 "C:\Users\user\Desktop\Mail Order Pharmacy\Member Portal\Member Portal\Views\_ViewImports.cshtml"
using Member_Portal;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\user\Desktop\Mail Order Pharmacy\Member Portal\Member Portal\Views\_ViewImports.cshtml"
using Member_Portal.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d6bb58d508d23248fc06e595e583a59ea882db2e", @"/Views/Refill/RefillStatus.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9fbd35064beefcedce4573625a5f56bf74c0002f", @"/Views/_ViewImports.cshtml")]
    public class Views_Refill_RefillStatus : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Member_Portal.Models.RefillOrderDetails>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Subscription", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\user\Desktop\Mail Order Pharmacy\Member Portal\Member Portal\Views\Refill\RefillStatus.cshtml"
  
    ViewData["Title"] = "Latest Refill Status";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"justify-content-center\">\r\n    <h2>Latest Refill Status</h2>\r\n</div>\r\n\r\n<div class=\"row\">\r\n    <div class=\"col-8 offset-2\">\r\n        <p><strong>Refill Order Id -- </strong>");
#nullable restore
#line 13 "C:\Users\user\Desktop\Mail Order Pharmacy\Member Portal\Member Portal\Views\Refill\RefillStatus.cshtml"
                                          Write(Model.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n        <p><strong>ComplitionDate -- </strong>");
#nullable restore
#line 14 "C:\Users\user\Desktop\Mail Order Pharmacy\Member Portal\Member Portal\Views\Refill\RefillStatus.cshtml"
                                         Write(Model.RefillDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n        <p><strong>Drug Quantity -- </strong>");
#nullable restore
#line 15 "C:\Users\user\Desktop\Mail Order Pharmacy\Member Portal\Member Portal\Views\Refill\RefillStatus.cshtml"
                                        Write(Model.DrugQuantity);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n        <p>\r\n            <strong>Delivery -- </strong>\r\n");
#nullable restore
#line 18 "C:\Users\user\Desktop\Mail Order Pharmacy\Member Portal\Member Portal\Views\Refill\RefillStatus.cshtml"
             if (Model.RefillDelivered)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <span> Completed </span>\r\n");
#nullable restore
#line 21 "C:\Users\user\Desktop\Mail Order Pharmacy\Member Portal\Member Portal\Views\Refill\RefillStatus.cshtml"
            }
            else
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <span> Processing </span>\r\n");
#nullable restore
#line 25 "C:\Users\user\Desktop\Mail Order Pharmacy\Member Portal\Member Portal\Views\Refill\RefillStatus.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </p>\r\n        <p>\r\n            <strong>Payment -- </strong>\r\n");
#nullable restore
#line 29 "C:\Users\user\Desktop\Mail Order Pharmacy\Member Portal\Member Portal\Views\Refill\RefillStatus.cshtml"
             if (Model.Payment)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <span> Completed </span>\r\n");
#nullable restore
#line 32 "C:\Users\user\Desktop\Mail Order Pharmacy\Member Portal\Member Portal\Views\Refill\RefillStatus.cshtml"
            }
            else
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <span> Pending </span>\r\n");
#nullable restore
#line 36 "C:\Users\user\Desktop\Mail Order Pharmacy\Member Portal\Member Portal\Views\Refill\RefillStatus.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </p>\r\n    </div>\r\n</div>\r\n\r\n<div>\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d6bb58d508d23248fc06e595e583a59ea882db2e7484", async() => {
                WriteLiteral("Go Back");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Member_Portal.Models.RefillOrderDetails> Html { get; private set; }
    }
}
#pragma warning restore 1591
