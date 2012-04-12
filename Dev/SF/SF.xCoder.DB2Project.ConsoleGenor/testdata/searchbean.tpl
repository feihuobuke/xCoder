using System;
using System.Web.UI.WebControls;
using Com.Vervidian.HireCredit.BusinessLogic.Bean;

namespace <#=Namespace#>
{
    public class <#=Name#>SearchBean:ISearchBean<<#=Name#>Bean>
    {
         <%  
		 foreach (Column col in Table.Columns)
		 {
			if (col.CSharpType.Equals("string", StringComparison.OrdinalIgnoreCase) || col.CSharpType.EndsWith("?"))
			{
			$
		public #col.CSharpType  #col.Name {get;set;}
			$
			}
			else
			{
			Output += "public "+col.CSharpType+"? "+col.Name+" {get;set;}\r\n";
			}
		 }
		 %>

		public Sort GetDefaultSortExpression()
        {
            return new Sort { Expression = "Id", Direction = SortDirection.Descending };
        }
    }
}