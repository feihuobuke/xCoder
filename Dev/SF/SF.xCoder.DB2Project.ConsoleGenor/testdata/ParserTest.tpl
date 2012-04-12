<@Page xxxxxxxxxxxxxxxx/>
using System;
namespace <#=Namespace#>
{
    public class <#=Name#>Bean:IBean
    {
         <#@ 
		 foreach (Column col in Table.Columns)
		 {
			<$@
		public @(col.CSharpType  @(col.Name) {get;set;}
			@$>
		 }
		 @#>
    }
}