using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Com.Vervidian.HireCredit.BusinessLogic.Bean;
using Com.Vervidian.HireCredit.BusinessLogic.Search;
using Com.Vervidian.HireCredit.DataAccess;

namespace <#=Namespace#>
{
    public partial class <#=Name#>BLL :BLLBase<<#=Name#>Bean, <#=Name#>, <#=Name#>SearchBean>
    {

		 public <#=Name#>BLL()
            : base(<#=Name#>Facade.GetInstance())
        {
        }
         internal override void CopyTo(<#=Name#> dao, ref <#=Name#>Bean bean)
        {
         <%  
		 foreach (Column col in Table.Columns)
		 {
			$
			bean.#col.Name = dao.#col.Name ;
			$
		 }
		 %>
        }

        internal override void CopyTo(<#=Name#>Bean bean, ref <#=Name#> dao)
        {
         <%  
		 foreach (Column col in Table.Columns)
		 {
			$
			dao.#col.Name = bean.#col.Name ;
			$
		 }
		 %>
        }

        protected override List<Expression<Func<<#=Name#>, bool>>> ToExpressions(<#=Name#>SearchBean searchBean)
        {
			var tmp = new List<Expression<Func<<#=Name#>, bool>>>();
        <%  
		 foreach (Column col in Table.Columns)
		 {
			if (col.CSharpType.Equals("string", StringComparison.OrdinalIgnoreCase))
			{
			$
			if (!string.IsNullOrEmpty(searchBean.#col.Name ))
            {
                tmp.Add(t => t.#col.Name .Contains(searchBean.#col.Name ));
            }
			$
			}
			else
			{
			$
			if (searchBean.#col.Name .HasValue)
            {
                tmp.Add(t => t.#col.Name == searchBean.#col.Name .Value);
            }
			$
			}
		 }
		 %>

            return tmp;
        }
    }
}