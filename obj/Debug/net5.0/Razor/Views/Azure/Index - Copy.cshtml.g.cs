#pragma checksum "F:\projects\UploadingLagreFiles_JavaScriptFileSplit\Views\Azure\Index - Copy.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ef1cefb7926bafab8406c1265f87bad07047b7e7"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Azure_Index___Copy), @"mvc.1.0.view", @"/Views/Azure/Index - Copy.cshtml")]
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
#line 1 "F:\projects\UploadingLagreFiles_JavaScriptFileSplit\Views\_ViewImports.cshtml"
using UploadingLagreFiles_JavaScriptFileSplit;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "F:\projects\UploadingLagreFiles_JavaScriptFileSplit\Views\_ViewImports.cshtml"
using UploadingLagreFiles_JavaScriptFileSplit.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ef1cefb7926bafab8406c1265f87bad07047b7e7", @"/Views/Azure/Index - Copy.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7e586889671c3fbf6bca303bda7d815fdd76d6a3", @"/Views/_ViewImports.cshtml")]
    public class Views_Azure_Index___Copy : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<UploadingLagreFiles_JavaScriptFileSplit.Controllers.AzureStorageSASResult>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/azurestoragejs-3.0.100/bundle/azure-storage.blob.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Home", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "UploadSmallFile", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("enctype", new global::Microsoft.AspNetCore.Html.HtmlString("multipart/form-data"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("BlobUploadForm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-label-left"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("role", new global::Microsoft.AspNetCore.Html.HtmlString("form"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "F:\projects\UploadingLagreFiles_JavaScriptFileSplit\Views\Azure\Index - Copy.cshtml"
  
	Layout = null;

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<!DOCTYPE html>\r\n<html lang=\"en\">\r\n\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ef1cefb7926bafab8406c1265f87bad07047b7e76750", async() => {
                WriteLiteral(@"
	<meta charset=""UTF-8"">
	<meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
	<meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
	<link href=""https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css"" rel=""stylesheet"">
	<title>Upload</title>
	<script src=""https://ajax.aspnetcdn.com/ajax/jQuery/jquery-3.6.0.min.js""></script>
	<script src=""https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.bundle.min.js""></script>
	");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ef1cefb7926bafab8406c1265f87bad07047b7e77497", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ef1cefb7926bafab8406c1265f87bad07047b7e79304", async() => {
                WriteLiteral("\r\n\t<div class=\"modal-dialog\">\r\n\t\t<div class=\"modal-content\">\r\n\t\t\t");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ef1cefb7926bafab8406c1265f87bad07047b7e79639", async() => {
                    WriteLiteral(@"
				<div class=""modal-body"">
					<div class=""form-group"">
						<div class=""input-group"">
							<label class=""input-group-btn"">
								<span class=""btn btn-primary"">
									Browse… <input type=""file"" style=""display: none;"" name=""file"" id=""FileInput"">
								</span>
							</label>
							<input type=""text"" class=""form-control""");
                    BeginWriteAttribute("readonly", " readonly=\"", 1390, "\"", 1401, 0);
                    EndWriteAttribute();
                    WriteLiteral(@" id=""BrowseInput"">
						</div>
					</div>
					<div class=""form-group"">
						<div class=""input-group"">
							<button type=""button"" value=""Upload to Blob"" class=""btn btn-success"" id=""UploadBlob"" onclick=""uploadBlob()"">
								Upload to Blob
							</button>
						</div>
					</div>
					<div class=""form-group hidden"" id=""uploadProgressBarContainer"">
						Uploading...
						<div class=""progress"">
							<div class=""progress-bar"" role=""progressbar"" id=""uploadProgressBar"" aria-valuenow=""60""
								 aria-valuemin=""0"" aria-valuemax=""100"" style=""width: 0%;"">
								0%
							</div>
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
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_5.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"
		</div>
	</div>

	<script type=""text/javascript"">
		$(document).on('change', '#FileInput', function () {
			var file = this.files.length > 0 ? this.files[0] : null;
			
			var acceptFileTypes = /(\.|\/)(zip)$/i;
			if (file && !acceptFileTypes.test(file.name)) {
				console.log('Not Allowed type');
				this.value = '';
			}

			var maxSize = 1024 * 1024 * 20;
			if (file && file.size > maxSize) {
				console.log('Not Allowed size');
				this.value = '';
			}

			$('#BrowseInput').val(this.value.replace(/\\/g, '/').replace(/.*\//, ''));
			if (this.value == '') {
				return false;
			}
			else {
				uploadBlob();
				return true;
			}
		});

		function displayProcess(process) {
			document.getElementById(""uploadProgressBar"").style.width = process + '%';
			document.getElementById(""uploadProgressBar"").innerHTML = process + '%';
		}

		function uploadBlob() {
			displayProcess(0);
			document.getElementById(""uploadProgressBarContainer"").classList.remove('hidden');

			// ");
                WriteLiteral("TODO: Call API to get URL, SAS Token, Container name\r\n\t\t\tvar blobUri = \'");
#nullable restore
#line 94 "F:\projects\UploadingLagreFiles_JavaScriptFileSplit\Views\Azure\Index - Copy.cshtml"
                      Write(Html.Raw(Model.AccountUrl));

#line default
#line hidden
#nullable disable
                WriteLiteral("\';\r\n\t\t\tvar containerName = \'");
#nullable restore
#line 95 "F:\projects\UploadingLagreFiles_JavaScriptFileSplit\Views\Azure\Index - Copy.cshtml"
                            Write(Html.Raw(Model.ContainerName));

#line default
#line hidden
#nullable disable
                WriteLiteral("\';\r\n\t\t\tvar blobName = \'");
#nullable restore
#line 96 "F:\projects\UploadingLagreFiles_JavaScriptFileSplit\Views\Azure\Index - Copy.cshtml"
                       Write(Html.Raw(Model.BlobName));

#line default
#line hidden
#nullable disable
                WriteLiteral("\';\r\n\t\t\tvar sasToken = \'");
#nullable restore
#line 97 "F:\projects\UploadingLagreFiles_JavaScriptFileSplit\Views\Azure\Index - Copy.cshtml"
                       Write(Html.Raw(Model.SASToken));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"';

			// https://vschooldev.blob.core.windows.net/lor/test.zip?sv=2019-10-10&st=2021-08-16T14%3A12%3A58Z&se=2021-08-17T14%3A12%3A58Z&sr=b&sp=racwdx&sig=lhgI4eDXzVL%2BdAb8281%2FbTNCYw0CKJ7VCtFV4VaUW9g%3D
			//var sasToken = 'sv=2019-10-10&st=2021-08-16T14%3A12%3A58Z&se=2021-08-17T14%3A12%3A58Z&sr=b&sp=racwdx&sig=lhgI4eDXzVL%2BdAb8281%2FbTNCYw0CKJ7VCtFV4VaUW9g%3D';

			AzureStorage.Blob.Constants.DEFAULT_PARALLEL_OPERATION_THREAD_COUNT = 1

			var blobService = AzureStorage.Blob.createBlobServiceWithSas(blobUri, sasToken).withFilter(cancelUploadFilter);

			var file = $('#FileInput').get(0).files[0];

			var customBlockSize = 1024 * 100;
			blobService.singleBlobPutThresholdInBytes = customBlockSize;
			blobService.parallelOperationThreadCount = 1;

			var uploadOptions = {
				blockSize: customBlockSize,
				parallelOperationThreadCount: 1,
				metadata: {
					isTemp: true
				}
			};

			cancelUploadFilter.cancel = false;
			cancelUploadFilter.onCancelComplete = function () {
				co");
                WriteLiteral(@"nsole.log('Cancelling complete');
			};

			var finishedOrError = false;
			var speedSummary = blobService.createBlockBlobFromBrowserFile(containerName, blobName, file, uploadOptions, function (error, result, response) {
				finishedOrError = true;
				if (error) {
					alert('Error');
				} else {
					displayProcess(100);
				}
			});

			function refreshProgress() {
				setTimeout(function () {
					if (!finishedOrError) {
						var process = speedSummary.getCompletePercent();
						displayProcess(process);
						refreshProgress();
					}
				}, 200);
			}

			refreshProgress();
		}

		// Filter allowing us to cancel an in-progress upload by setting 'cancel' to true.
		// This doesn't cancel file chunks currently being uploaded, so the upload does continue 
		// for a while after this is triggered. If the last chunks have already been sent the 
		// upload might actually complete (??)
		// See http://azure.github.io/azure-storage-node/StorageServiceClient.html#withFilter
		va");
                WriteLiteral(@"r cancelUploadFilter = {
			cancel: false,              // Set to true to cancel an in-progress upload

			loggingOn: false,            // Set to true to see info on console

			onCancelComplete: null,     // Set to a function you want called when a cancelled request is (probably?) complete, 
			// i.e. when returnCounter==nextCounter. Because when you cancel an upload it can be many 
			// seconds before the in-progress chunks complete. 

			// Internal from here down

			nextCounter: 0,             // Increments each time next() is called. a.k.a. 'SentChunks' ? 
			returnCounter: 0,           // Increments each time next()'s callback function is called. a.k.a. 'ReceivedChunks'?
			handle: function (requestOptions, next) {
				var self = this;
				if (self.cancel) {
					self.log('cancelling before chunk sent');
					return;
				}
				if (next) {
					self.nextCounter++;
					next(requestOptions, function (returnObject, finalCallback, nextPostCallback) {
						self.returnCounter++;
			");
                WriteLiteral(@"			if (self.cancel) {
							self.log('cancelling after chunk received');
							if (self.nextCounter == self.returnCounter && self.onCancelComplete) {
								self.onCancelComplete();
							}

							// REALLY ??? Is this the right way to stop the upload?
							return;
						}
						if (nextPostCallback) {
							nextPostCallback(returnObject);
						} else if (finalCallback) {
							finalCallback(returnObject);
						}
					});
				}
			},
			log: function (msg) {
				if (this.loggingOn) {
					console.log('cancelUploadFilter: ' + msg + ' nc: ' + this.nextCounter + ', rc: ' + this.returnCounter);
				}
			},
		};
	</script>
");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</html>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<UploadingLagreFiles_JavaScriptFileSplit.Controllers.AzureStorageSASResult> Html { get; private set; }
    }
}
#pragma warning restore 1591