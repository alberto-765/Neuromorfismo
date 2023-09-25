using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMedicina.BackEnd.Model;

namespace WebMedicina.BackEnd.Data {
	public class UsuarioDal {
		private readonly WebmedicinaContext _context;

		public UsuarioDal(WebmedicinaContext context) {
			_context = context;
		}

	}
}
