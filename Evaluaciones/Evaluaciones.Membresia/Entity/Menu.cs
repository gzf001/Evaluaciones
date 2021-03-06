using System;
using System.Data.Linq;
namespace Evaluaciones.Membresia
{
	[Serializable]
	public partial class Menu : IEquatable<Menu>
	{
		public Menu()
		{
			this.Id = Guid.NewGuid();
		}

		public Guid Id { get; set; }

		public String Nombre { get; set; }

		public String Clave { get; set; }

		public void Attach()
		{
			Context.Instancia.Menus.Attach(this);
		}

		public bool Equals(Menu other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.Id.Equals(Id);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(Menu)) return false;
			return Equals((Menu)obj);
		}

		public override int GetHashCode()
		{
			return this.Id.GetHashCode();
		}
	}
}