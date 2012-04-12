using Com.Vervidian.HireCredit.DataAccess.Facade;

namespace <#=Namespace#>
{
    public class <#=Name#>Facade :FacadeBase<<#=Name#>>
    {
         private static readonly <#=Name#>Facade Instance = new <#=Name#>Facade();

        private <#=Name#>Facade()
        {
        }

        public static <#=Name#>Facade GetInstance()
        {
            return Instance;
        }
    }
}