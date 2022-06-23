﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Init.Sigepro.FrontEnd
{
    /// <summary>
    /// Summary description for Version
    /// </summary>
    public class Version : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var asmNamesList = new List<string>();

            var currentAssembly = Assembly.GetExecutingAssembly();

            context.Response.Write("<html><body>");

            asmNamesList.Add(string.Format("<b>{0} v{1}</b>", currentAssembly.GetName().Name, currentAssembly.GetName().Version));


            foreach (var asm in currentAssembly.GetReferencedAssemblies().Where(x => !x.Name.StartsWith("System") && !x.Name.StartsWith("mscorlib") && !x.Name.StartsWith("Microsoft")))
            {
                asmNamesList.Add(string.Format("{0} v{1}", asm.Name, asm.Version));

            }

            foreach (var name in asmNamesList.OrderBy(x => x).Select(x => x))
                context.Response.Write(String.Format("{0}<br />", name));

            context.Response.Write("<h1>Server variables</h1>");

            foreach (var key in context.Request.ServerVariables.AllKeys)
            {
                context.Response.Write(String.Format("<b>{0}=</b>{1}<br/>", key, context.Request.ServerVariables[key]));
            }

            context.Response.ContentType = "text/html";
            context.Response.Write("</body></html>");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}