using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Accessibility;
using Microsoft.AspNetCore.Hosting;
using Chromium.AspNetCore.Bridge;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Extensions.DependencyInjection;
using MeetupSample.WebApp;
using CefSharp;
using System.Reflection;
using CefSharp.WinForms;
using Microsoft.Extensions.Hosting;

namespace MeetupSample.Desktop
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private IWebHost _host;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            _host.StopAsync().Wait();
            
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            chromiumWebBrowser1 = new CefSharp.WinForms.ChromiumWebBrowser();

            Cef.Initialize(new CefSettings());

            _ = Task.Run(async () =>
            {
                var builder = new WebHostBuilder();

                builder.ConfigureServices(services =>
                {
                    var server = new OwinServer();
                    server.UseOwin(appFunc =>
                    {
                        var requestContext = Cef.GetGlobalRequestContext();
                        requestContext.RegisterOwinSchemeHandlerFactory("https", "cefsharp.test", appFunc);
                    });

                    services.AddSingleton<IServer>(server);
                });

                string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var webRootPath = Path.Combine(path, "wwwroot");

                _host = builder
                    .UseStartup<Startup>()
                    .UseWebRoot(webRootPath)
                    .Build();

                _host.Services.GetRequiredService<IHostApplicationLifetime>().ApplicationStarted
                    .Register(() => chromiumWebBrowser1.Load("https://cefsharp.test"));

                await _host.RunAsync();
            });

            SuspendLayout();
            // 
            // chromiumWebBrowser1
            // 
            chromiumWebBrowser1.ActivateBrowserOnCreation = false;
            chromiumWebBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            chromiumWebBrowser1.Location = new System.Drawing.Point(0, 0);
            chromiumWebBrowser1.Name = "chromiumWebBrowser1";
            chromiumWebBrowser1.Size = new System.Drawing.Size(800, 450);
            chromiumWebBrowser1.TabIndex = 0;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 450);
            Controls.Add(chromiumWebBrowser1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private CefSharp.WinForms.ChromiumWebBrowser chromiumWebBrowser1;
    }
}