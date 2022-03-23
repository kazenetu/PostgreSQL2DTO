using System.Text;

namespace Infrastructure.CSFiles.Templates
{
  public partial class CreateCS
   {
     public string TransformText()
     {
       var template = new StringBuilder();
       template.AppendLine($"namespace {NameSpace}");
       template.AppendLine($"{{");
       template.AppendLine($"  {GetCSComment(classEntity.Comment, "  ", false)}");
       template.AppendLine($"  public class {GetCSName(classEntity.Name)}");
       template.AppendLine($"  {{");
       template.AppendLine($"    public {GetCSName(classEntity.Name)}()");
       template.AppendLine($"    {{");
       template.AppendLine($"    }}");
       foreach(var property in classEntity.Properties)
       {
         template.AppendLine($"    {GetCSComment(property.Comment, "    ")}");
         template.AppendLine($"    public {property.TypeName} {GetCSName(property.Name)}{{set; get;}}");
       }
       template.AppendLine($"  }}");
       template.AppendLine($"}}");
       return template.ToString();
     }
   }
}
