namespace xCoder.Test.testdata
{
    public class <#=Name#>
    {
         <%  
		 foreach (Column col in Table.Columns)
		 {
			Output += string.Format("0");
		 }
		 %>
    }
}