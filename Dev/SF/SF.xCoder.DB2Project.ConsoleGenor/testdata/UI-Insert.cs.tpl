using System;
namespace Com.Vervidian.HireCredit.Web.<#=Name#>
{
    public partial class Insert : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SubmitEvent(object sender, EventArgs e)
        {
			var bean=new <#=Name#>Bean();
			<%  
			 foreach (Column col in Table.Columns)
			 {
				$bean.#col.Name = txt#col.Name .Text;
				$
			 }
			 %>
        }
    }
}
