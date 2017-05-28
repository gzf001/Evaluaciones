using System;
using System.Collections.Generic;
using System.Linq;
namespace Evaluaciones
{
	public partial class Empresa
	{
        public static Empresa Get(Guid id)
        {
            return Query.GetEmpresas().SingleOrDefault<Empresa>(x => x.Id == id);
        }

        public static List<Empresa> GetAll()
		{
			return
				(
				from query in Query.GetEmpresas()
				select query
				).ToList<Empresa>();
		}
	}
}