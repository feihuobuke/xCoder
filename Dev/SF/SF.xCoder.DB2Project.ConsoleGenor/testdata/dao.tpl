using System;
namespace <#=Namespace#>
{
    public class <#=Name#>:IDao
    {
         <%  
		 foreach (Column col in Table.Columns)
		 {
			$
		public virtual #col.CSharpType  #col.Name {get;set;}
			$
		 }
		 %>
    }
}