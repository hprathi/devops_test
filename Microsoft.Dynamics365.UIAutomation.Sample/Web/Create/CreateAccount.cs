﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Dynamics365.UIAutomation.Api;
using Microsoft.Dynamics365.UIAutomation.Browser;
using System;
using System.Security;
using System.Diagnostics;

namespace Microsoft.Dynamics365.UIAutomation.Sample.Web
{
    [TestClass]
    public class CreateAccount
    {

        private readonly SecureString _username = System.Configuration.ConfigurationManager.AppSettings["OnlineUsername"].ToSecureString();
        private readonly SecureString _password = System.Configuration.ConfigurationManager.AppSettings["OnlinePassword"].ToSecureString();
        private readonly Uri _xrmUri = new Uri(System.Configuration.ConfigurationManager.AppSettings["OnlineCrmUrl"].ToString());

        [TestMethod]
        public void WEBTestCreateNewAccount()
        {
            using (var xrmBrowser = new Api.Browser(TestSettings.Options))
            {
                xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);
                xrmBrowser.GuidedHelp.CloseGuidedHelp();
                Trace.TraceInformation("CRM Organization logged in successfully.");

                xrmBrowser.ThinkTime(500);
                xrmBrowser.Navigation.OpenSubArea("Sales", "Accounts");

                Trace.TraceInformation("Navigation Done. Sales >> Accounts");

                xrmBrowser.ThinkTime(2000);
                xrmBrowser.Grid.SwitchView("Active Accounts");

                Trace.TraceInformation("Navigation Done. Active Accounts");

                xrmBrowser.ThinkTime(1000);
                xrmBrowser.CommandBar.ClickCommand("New");

                xrmBrowser.ThinkTime(5000);
                xrmBrowser.Entity.SetValue("name", "HSBC Account" + DateTime.Today.ToLongTimeString());
                xrmBrowser.Entity.SetValue("telephone1", "555-555-5666");
                xrmBrowser.Entity.SetValue(new LookupItem { Name = "hsbc_businesssegmentcountry", Value = "India" });
                xrmBrowser.Entity.SetValue(new OptionSet { Name = "hsbc_clientstatus", Value = "Active" });
                xrmBrowser.Entity.SetValue(new OptionSet { Name = "hsbc_priority", Value = "High" });
                xrmBrowser.Entity.SetValue(new OptionSet { Name = "industrycode", Value = "Accounting" });

                xrmBrowser.Entity.SetValue("websiteurl", "https://wiprolimited04.crm8.dynamics.com");

                xrmBrowser.CommandBar.ClickCommand("Save & Close");
                xrmBrowser.ThinkTime(2000);
            }
        }
    }
}