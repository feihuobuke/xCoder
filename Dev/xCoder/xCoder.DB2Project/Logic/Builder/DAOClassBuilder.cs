using System.Collections.Generic;

namespace xCoder.Logic.Builder
{
    public class DAOClassBuilder:AbsBuilder
    {
        public DAOClassBuilder(BuilderParameters parameters) : base(parameters)
        {
        }

        public override string[] Build()
        {
            var tmp = new List<string>();
            foreach (var table in DataBase.Tables)
            {

            }
            return tmp.ToArray();
        }
    }
}