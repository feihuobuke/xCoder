namespace <#=Namespace#>
{
    public class <#=Name#>
    {
         <%  
		 foreach (Column col in Table.Columns)
		 {
			Output += string.Format(@"
			public virtual {0} {1} ",col.NetTypeString,col.Name)+@"{get;set;}
			";
		 }
		 %>
    }
}