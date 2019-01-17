using HowManyCountModel;
using OfferKayalarModel.DataModel;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using TalentBaseModel.DataModel;

namespace ConsoleAppTest
{
    public static class CreateDynamicPage
    {
        /// <summary>
        /// Generate Page
        /// </summary>
        public static void CreateHTMLPageFromModel()
        {
            OfferGroupingKeys itemModel = new OfferGroupingKeys();

            StringBuilder sb = new StringBuilder();
            Type t = itemModel.GetType();

            PropertyInfo[] propertyInfos = t.GetProperties();

            sb.AppendLine("<html>");
            sb.AppendLine("<head>");
            sb.AppendLine("<title></title>");
            sb.AppendLine("<meta name=\"viewport\" content=\"width=device-width\" />");
            sb.AppendLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" />");

            sb.AppendLine("<link href=\"../Content/bootstrap.min.css\" rel =\"stylesheet\" />");
            sb.AppendLine("<link href=\"../Content/bootstrap-datetimepicker.min.css\" rel =\"stylesheet\" />");
            sb.AppendLine("<script src=\"../Scripts/jquery-3.2.1.min.js\"></script> ");
            sb.AppendLine("<script src=\"../ Scripts / bootstrap.min.js\"></script>");

            sb.AppendLine("<script src=\"../Scripts/jquery.serialize-object.js\"></script>");
            sb.AppendLine("<script src=\"../Scripts/pascal-common-1.0.0.js\"></script>");
            sb.AppendLine("<script src=\"../Scripts/pascal-1.0.0.js\"></script>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");

            sb.AppendLine("<div id=\"top - menu\"></div>");
            sb.AppendLine("<div class=\"container\">");
            sb.AppendLine("<h2>" + t.Name + " Description</h2>");
            sb.AppendLine("<hr />");

            sb.AppendLine("<form>");
            foreach (var prop in propertyInfos)
            {
                sb.AppendLine("<div class=\"form-group\">");
                sb.AppendLine("<label for=\"" + prop.Name + "\">" + prop.Name + "</label>");

                switch (prop.PropertyType.Name)
                {
                    case "List`1":
                        sb.AppendLine("<select class=\"form-control\"  name=\"" + prop.Name + "\"   id=\"" + prop.Name + "\" class=\"form-control\"></select>");
                        break;
                    case "DateTime":
                        sb.AppendLine("<input type=\"text\" name=\"" + prop.Name + "\"  id=\"" + prop.Name + "\" class=\"form-control\" placeholder=\"" + prop.Name + "\" />");
                        break;
                    case "Boolean":
                        sb.AppendLine("<input type=\"checkbox\" name=\"" + prop.Name + "\"  id=\"" + prop.Name + "\" class=\"form-control\"/>");
                        break;
                    default:
                        sb.AppendLine("<input type=\"text\" name=\"" + prop.Name + "\"  id=\"" + prop.Name + "\" class=\"form-control\" placeholder=\"" + prop.Name + "\"  />");
                        break;
                }
                sb.AppendLine("</div>");
            }

            sb.AppendLine("<div class=\"form-group\">");


            sb.AppendLine("<button type =\"button\" id =\"btnKaydet\" class=\"btn btn-primary\" value =\"Kaydet\" > Kaydet</button>");
            sb.AppendLine("<input type =\"hidden\" id=\"apiPageUri\" value =\"" + t.Name + "\" />");
            sb.AppendLine("<input type =\"hidden\" id =\"pageUriList\" value =\"" + t.Name + "List.html\"/> ");
            sb.AppendLine("</div>");

            sb.AppendLine("</form>");
            sb.AppendLine("</div>");

            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            string result = sb.ToString();

            //HTML OLARAK ÇIKTI ÜRETECEK
            string path = @"D:\JsonData\Html\" + t.Name + ".html";
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.Write(result);
            }
        }



        /// <summary>
        ///  Add spinner https://shaack.com/projekte/bootstrap-input-spinner/
        /// </summary>

        public static void CreateHTMLPageFromModelNew<TIn>(this TIn itemModel)
        {
            //https://weblogs.asp.net/grantbarrington/using-reflection-to-determine-whether-an-type-is-nullable-and-get-the-underlying-type
            // https://gist.github.com/afreeland/6796800

            //HowManyCountModel.DataModel.Address itemModel = 
            // new HowManyCountModel.DataModel.Address();

            StringBuilder sb = new StringBuilder();
            Type t = itemModel.GetType();

            PropertyInfo[] propertyInfos = t.GetProperties();

            // typeof(HelpAttribute),
            try
            {
                sb.AppendLine("<div class=\"container\">");
                sb.AppendLine("<h2>" + t.Name + " Description</h2>");
                sb.AppendLine("<hr />");

                sb.AppendLine("@using (Html.BeginForm(\"SaveAndUpdate\", \"" + t.Name + "\", FormMethod.Post))");
                sb.AppendLine("{");
                foreach (var prop in propertyInfos)
                {
                    bool attrResult = false;
                    if (prop.GetCustomAttributes(true).Length != 0)
                    {
                        var att = prop.GetCustomAttributes(true)[0];
                        HelpAttribute ha = (HelpAttribute)att;
                        attrResult = ha.IsDropDownList;
                    }

                    sb.AppendLine("<div class=\"form-group\">");
                    sb.AppendLine("<label for=\"" + prop.Name + "\">" + prop.Name + "</label>");

                    TypeCode typeCode = System.Type.GetTypeCode(prop.PropertyType);
                    // typeof(Decimal).Name
                    //  sb.AppendLine("<select class=\"form-control\"  name=\"" + prop.Name + "\"   id=\"" + prop.Name + "\" class=\"form-control\"></select>");

                    switch (typeCode)
                    {
                        case TypeCode.Object:
                            if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                            {
                                var propertyType = prop.PropertyType.GetGenericArguments()[0];

                                if (prop.PropertyType.Name == "Decimal")
                                {
                                    sb.AppendLine(CreateDecimalInputTag(prop.Name.ToString()));
                                    break;
                                }
                                else if (prop.PropertyType.Name == "DateTime" || prop.Name.Contains("Date"))
                                {
                                    sb.AppendLine(CreateDateTimeInputTag(prop.Name));
                                    break;
                                }
                                else if (propertyType.Name == "Int32" && attrResult)
                                {
                                    sb.AppendLine(CreateDropDownTag(prop.Name));
                                }
                            }
                            sb.AppendLine(CreateInt32InputTag(prop.Name));
                            break;
                        case TypeCode.Int32:
                            sb.AppendLine(CreateInt32InputTag(prop.Name));
                            break;
                        case TypeCode.Decimal:
                            sb.AppendLine(CreateDecimalInputTag(prop.Name));
                            break;
                        case TypeCode.Boolean:
                            sb.AppendLine("@Html.CheckBoxFor(x => x." + prop.Name + ", new { @checked = \"checked\" })");
                            break;
                        case TypeCode.DateTime:
                            sb.AppendLine(CreateDateTimeInputTag(prop.Name));
                            break;
                        default:
                            sb.AppendLine(CreateDefaultInputTag(prop.Name));
                            break;
                    }
                    sb.AppendLine("</div>");
                }

                sb.AppendLine("<div class=\"form-group\">");


                sb.AppendLine("<button type =\"submit\" id =\"btnKaydet\" class=\"btn btn-primary\" value =\"Kaydet\" > Kaydet</button>");
                sb.AppendLine("<input type =\"hidden\" id=\"Id\" name=\"Id\" value=\"@Model.Id\" />");
                //  sb.AppendLine("<input type =\"hidden\" id=\"CreatedUserId\" name=\"CreatedUserId\" value=\"@Model.Id\" />");

                sb.AppendLine("</div>");

                sb.AppendLine("}");
                sb.AppendLine("</div>");

            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
            string result = sb.ToString();

            //HTML OLARAK ÇIKTI ÜRETECEK
            string path = @"D:\" + t.Name + ".html";
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.Write(result);
            }
        }

        // Base bootsrap spinner
        public static void CreateHTMLPageFromModelNew1()
        {
            //https://weblogs.asp.net/grantbarrington/using-reflection-to-determine-whether-an-type-is-nullable-and-get-the-underlying-type
            // https://gist.github.com/afreeland/6796800

            OfferKayalarModel.DataModel.Products itemModel = new OfferKayalarModel.DataModel.Products();

            StringBuilder sb = new StringBuilder();
            Type t = itemModel.GetType();

            PropertyInfo[] propertyInfos = t.GetProperties();


            sb.AppendLine("<div class=\"container\">");
            sb.AppendLine("<h2>" + t.Name + " Description</h2>");
            sb.AppendLine("<hr />");

            sb.AppendLine("@using (Html.BeginForm(\"SaveAndUpdate\", \"" + t.Name + "\", FormMethod.Post))");
            sb.AppendLine("{");
            foreach (var prop in propertyInfos)
            {
                sb.AppendLine("<div class=\"form-group\">");
                sb.AppendLine("<label for=\"" + prop.Name + "\">" + prop.Name + "</label>");

                TypeCode typeCode = System.Type.GetTypeCode(prop.PropertyType);
                // typeof(Decimal).Name
                //  sb.AppendLine("<select class=\"form-control\"  name=\"" + prop.Name + "\"   id=\"" + prop.Name + "\" class=\"form-control\"></select>");

                switch (typeCode)
                {
                    case TypeCode.Object:
                        if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                        {
                            var propertyType = prop.PropertyType.GetGenericArguments()[0];

                            if (prop.PropertyType.Name == "Decimal")
                            {
                                sb.AppendLine("<input type=\"number\" min=\"0\" step=\"any\" name=\"" + prop.Name + "\"  id=\"" + prop.Name + "\" class=\"form-control two-decimals\" placeholder=\"0\" value=\"@Model." + prop.Name + "\" required />");
                                return;
                            }
                        }
                        else
                        {
                            sb.AppendLine("<input type=\"number\" min=\"0\" name=\"" + prop.Name + "\"  id=\"" + prop.Name + "\" class=\"form-control two-decimals\" placeholder=\"0\" value=\"@Model." + prop.Name + "\" required />");
                        }
                        break;
                    case TypeCode.Int32:
                        sb.AppendLine("<input type=\"number\" min=\"0\" name=\"" + prop.Name + "\"  id=\"" + prop.Name + "\" class=\"form-control two-decimals\" placeholder=\"0\" value=\"@Model." + prop.Name + "\" required />");
                        break;
                    case TypeCode.DateTime:
                        sb.AppendLine("<input type=\"text\" name=\"" + prop.Name + "\"  id=\"" + prop.Name + "\" class=\"form-control\" placeholder=\"" + prop.Name + "\" value=\"@Model." + prop.Name + "\" required />");
                        break;
                    case TypeCode.Boolean:
                        sb.AppendLine("@Html.CheckBoxFor(x => x." + prop.Name + ", new { @checked = \"checked\" })");
                        break;
                    default:
                        sb.AppendLine("<input type=\"text\" name=\"" + prop.Name + "\"  id=\"" + prop.Name + "\" class=\"form-control\" placeholder=\"" + prop.Name + "\" value=\"@Model." + prop.Name + "\" required />");
                        break;
                }
                sb.AppendLine("</div>");
            }

            sb.AppendLine("<div class=\"form-group\">");


            sb.AppendLine("<button type =\"submit\" id =\"btnKaydet\" class=\"btn btn-primary\" value =\"Kaydet\" > Kaydet</button>");
            sb.AppendLine("<input type =\"hidden\" id=\"Id\" name=\"Id\" value=\"@Model.Id\" />");
            //  sb.AppendLine("<input type =\"hidden\" id=\"CreatedUserId\" name=\"CreatedUserId\" value=\"@Model.Id\" />");

            sb.AppendLine("</div>");

            sb.AppendLine("}");
            sb.AppendLine("</div>");


            string result = sb.ToString();

            //HTML OLARAK ÇIKTI ÜRETECEK
            string path = @"D:\" + t.Name + ".html";
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.Write(result);
            }
        }

        #region Private Text

        private static string CreateDateTimeInputTag(string propName)
        {
            string strInput = "<input type=\"text\" name=\"" + propName + "\"  id=\"" + propName + "\" class=\"form-control datetimepicker\" placeholder=\""+ propName + "\" value=\"@Model." + propName + "\" required />";
            return strInput;
        }

        private static string CreateDecimalInputTag(string propName)
        {
            string strInput = "<input type=\"number\" min=\"0\" step=\"0.01\" data-decimals=\"2\" name=\"" + propName + "\"  id=\"" + propName + "\" class=\"form-control two-decimals\" placeholder=\"0\" value=\"@Model." + propName + "\" required />";
            return strInput;
        }

        private static string CreateInt32InputTag(string propName)
        {
            string strInput = "<input type=\"number\" min=\"0\" step=\"1\" name=\"" + propName + "\"  id=\"" + propName + "\" class=\"form-control two-decimals\" placeholder=\"0\" value=\"@Model." + propName + "\" required />";
            return strInput;
        }

        private static string CreateDropDownTag(string propName)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<select id=\"" + propName + "\" class=\"form-control\" placeholder=\"" + propName + "\" />");
            sb.AppendLine("<option>Seçiniz</option>");

            sb.AppendLine("@foreach(var item in Model."+ propName + "List)");
            sb.AppendLine(" {");
            sb.AppendLine("<option @(Model.propName == item.Id ? \"selected ='true'\" : \") value = \"@item.Id\">@item.Name</option>");
           
           sb.AppendLine("}");
           sb.AppendLine("</select>");
           sb.AppendLine("</div>");

            return "";
        }

        // String
        private static string CreateDefaultInputTag(string propName)
        {
            string strInput = "<input type=\"text\" name=\"" + propName + "\"  id=\"" + propName + "\" class=\"form-control\" placeholder=\""+ propName + "\" value=\"@Model." + propName + "\" required />";
            return strInput;
        }

        #endregion
    }
}
