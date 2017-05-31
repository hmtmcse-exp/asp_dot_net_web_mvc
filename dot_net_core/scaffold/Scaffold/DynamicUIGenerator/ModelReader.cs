using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Reflection;

namespace Scaffold.DynamicUIGenerator
{
    public class ModelReader
    {
        private static string tableHead = "";
        private static string tableBody = "";
        private static string formRow = "";
        private static string detailsRow = "";

        public static void Read(string model)
        {
            var type = Type.GetType(model);
            if (type == null) return;
            foreach (var prop in type.GetProperties())
            {
                Console.WriteLine("{0}", prop.Name);
            }
        }

        public static void ModelListByNamespace(string modelNamespace)
        {
            if (modelNamespace == null)
            {
                Console.WriteLine("Please provide Model Namespace");
            }
            else
            {
                Assembly myAssembly = Assembly.GetEntryAssembly();
                foreach (Type type in myAssembly.GetTypes())
                {
                    var klassName = type.FullName;
                    if (klassName != null && klassName.StartsWith(modelNamespace))
                    {
                        Console.WriteLine(klassName);
                    }
                }
            }
        }

        private static void GenerateFormRow(string type, string name, bool isRequired)
        {
            var rowBody = "<div class=\"form-group\">" + Environment.NewLine;
            switch (type)
            {
                case "text":
                    rowBody += "<label asp-for=\"" + name + "\" class=\"control-label\"></label>" + Environment.NewLine;
                    rowBody += "<input asp-for=\"" + name + "\" class=\"form-control\"/>" + Environment.NewLine;
                    break;
                case "boolean":
                    rowBody += "<div class=\"checkbox\">" + Environment.NewLine;
                    rowBody += "<label class=\"control-label\">" + Environment.NewLine;
                    rowBody += "<input asp-for=\"" + name + "\"/>@Html.DisplayNameFor(model => model." + name + ")" +
                               Environment.NewLine;
                    rowBody += "</label>" + Environment.NewLine + "</div>" + Environment.NewLine;
                    break;
                case "select":
                    rowBody += "<label asp-for=\"" + name + "\" class=\"control-label\"></label>" + Environment.NewLine;
                    rowBody += "<select asp-for=\"" + name + "\" asp-items=\"" + name +
                               "\" class=\"form-control\"></select>" + Environment.NewLine;
                    break;
            }
            rowBody += " <span class=\"has-error\"><span class=\"help-block\" asp-validation-for=\"" + name +
                       "\" ></span></span>" + Environment.NewLine;
            rowBody += "</div>" + Environment.NewLine;
            formRow += rowBody;
        }

        private static void GenerateTableCol(string name)
        {
            tableHead += "<table-header name=\"" + name + "\">" + name + "</table-header>" + Environment.NewLine;
            tableBody += "<td>@Html.DisplayFor(modelItem => item." + name + ")</td>" + Environment.NewLine;
        }

        private static void GenerateDetails(string name)
        {
            var html = "<dt>@Html.DisplayNameFor(model => model." + name + ")</dt>" + Environment.NewLine;
            html += "<dd>@Html.DisplayFor(model => model." + name + ")</dd>" + Environment.NewLine;
            detailsRow += html;
        }

        public static void Generate(string modelNamespace)
        {
            if (modelNamespace == null)
            {
                Console.WriteLine("Please provide Model With Namespace");
            }
            else
            {
                
               
                Assembly myAssembly = Assembly.GetEntryAssembly();
                Type type = myAssembly.GetType(modelNamespace);
                if (type == null)
                {
                    Console.WriteLine("Invalid Model.");
                }
                else
                {
                    var sourceCodePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "Scaffold");
                    var controllersPath = Path.Combine(sourceCodePath,"Controllers");
                    var viewPath = Path.Combine(sourceCodePath,"Views");
                    var componentPath = Path.Combine(sourceCodePath,"component");
                    var viewComponentPath = Path.Combine(componentPath, "view");
                    
                    var modelName = type.Name;
                    var viewModel = Path.Combine(viewPath, modelName);
                    Console.WriteLine("{0} {1} {2} ", viewPath, modelName, viewModel);
                    if (Directory.Exists(viewModel))
                    {
                        Directory.Delete(viewModel,true);
                    }
                    Directory.CreateDirectory(viewModel);

                    // Need To Check. boolean, enum, object, Integer, float

                    foreach (var prop in type.GetProperties())
                    {
                        var isRequired = false;
                        var atttrs = prop.GetCustomAttributes(false);
                        foreach (var attr in atttrs)
                        {
                            if (attr.GetType().Name.Equals("RequiredAttribute"))
                            {
                                isRequired = true;
                            }
                        }
                        var propertyName = prop.Name;

                        if (prop.PropertyType.Name.Equals("Boolean"))
                        {
                            GenerateFormRow("boolean", propertyName, isRequired);
                            Console.WriteLine(" Checkbox ");
                        }
                        else if (prop.PropertyType.Name.Equals("String"))
                        {
                            GenerateFormRow("text", propertyName, isRequired);
                            Console.WriteLine(" Textbox ");
                        }
                        else if (prop.PropertyType.Name.Equals("Byte[]"))
                        {
                            if (!prop.Name.Equals("RowVersion"))
                            {
                                Console.WriteLine(" Hidden Row ");
                            }
                        }
                        else if (prop.PropertyType.Name.Equals("Int32"))
                        {
                            if (!prop.Name.Equals("Id"))
                            {
                                GenerateFormRow("text", propertyName, isRequired);
                            }
                        }
                        else
                        {
                            GenerateFormRow("select", propertyName, isRequired);
                        }
                        if (!prop.Name.Equals("Id") && !prop.Name.Equals("RowVersion"))
                        {
                            GenerateTableCol(prop.Name);
                            GenerateDetails(prop.Name);
                        }
                        
                        Console.WriteLine("{0} {1} ", propertyName, prop.PropertyType.Name);
                    }
                    
                    string text = ReadFile(Path.Combine(viewComponentPath, "Create.cshtml"), modelName, modelNamespace);
                    WriteToFile(Path.Combine(viewModel, "Create.cshtml"), text);
                        
                    text = ReadFile(Path.Combine(viewComponentPath, "Details.cshtml"), modelName, modelNamespace);
                    text = text?.Replace("__DETAILS_ROW__", detailsRow);
                    WriteToFile(Path.Combine(viewModel, "Details.cshtml"), text);
                        
                    text = ReadFile(Path.Combine(viewComponentPath, "Edit.cshtml"), modelName, modelNamespace);
                    WriteToFile(Path.Combine(viewModel, "Edit.cshtml"), text);
                        
                    text = ReadFile(Path.Combine(viewComponentPath, "Form.cshtml"), modelName, modelNamespace);
                    text = text?.Replace("__FORM_ROW__", formRow);
                    WriteToFile(Path.Combine(viewModel, "Form.cshtml"), text);
                        
                    text = ReadFile(Path.Combine(viewComponentPath, "Index.cshtml"), modelName, modelNamespace);
                    text = text?.Replace("__TABLE_BODY__", tableBody);
                    text = text?.Replace("__TABLE_HEAD__", tableHead);
                    WriteToFile(Path.Combine(viewModel, "Index.cshtml"), text);
                        
                    text = ReadFile(Path.Combine(componentPath, "Controller.txt"), modelName, modelNamespace);
                    WriteToFile(Path.Combine(controllersPath, modelName + "Controller.cs"), text);
                        
                }
            }
        }


        public static void WriteToFile(string location, string content)
        {
            Console.WriteLine("{0} {1} ", location, content);
            if (File.Exists(location))
            {
                File.Delete(location);
            }
            if (content != null)
            {
                File.WriteAllText(location, content);  
            }
        }
        
        public static string ReadFile(string location, string modelName, string mNamespace)
        {
            string text = null;
            if (!File.Exists(location))
            {
                return null;
            }
            else
            {
                text = File.ReadAllText(location);
                text = text?.Replace("__MODEL_NAME__", modelName);
                text = text?.Replace("__MODEL_WIHT_NAMESPACE__", mNamespace);
            }

            return text;
        }


        public static void ModelList()
        {
            Assembly myAssembly = Assembly.GetEntryAssembly();
            Console.WriteLine(myAssembly.CodeBase);
            Console.WriteLine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName);
            foreach (Type type in myAssembly.GetTypes())
            {
                var klassName = type.FullName;
                if (klassName != null && klassName.StartsWith("Scaffold.Models"))
                {
                    Console.WriteLine(klassName);
                    foreach (var prop in type.GetProperties())
                    {
                        var atttrs = prop.GetCustomAttributes(false);
                        foreach (var attr in atttrs)
                        {
                            if (attr.GetType().Name.Equals("DisplayAttribute"))
                            {
                                Console.WriteLine("Display Name {0}", prop.GetCustomAttribute<DisplayAttribute>().Name);
                            }
                            else if (attr.GetType().Name.Equals("DataTypeAttribute"))
                            {
                                Console.WriteLine("Data Type {0}",
                                    prop.GetCustomAttribute<DataTypeAttribute>().DataType);
                            }
                            Console.WriteLine("{0}", attr.GetType().Name);
                        }
                        Console.WriteLine("{0} {1}", prop.Name, prop.PropertyType);
                    }
                }
            }
        }
    }
}