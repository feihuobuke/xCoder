namespace <#=Namespace#>
{
    public class <#=Name#>
    {
         <%  
		 foreach (Column col in Table.Columns)
		 {
			$
			public virtual #col.NetTypeString #col.Name {get;set;}
			$
		 }
		 %>
    }
}